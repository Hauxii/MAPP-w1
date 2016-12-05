﻿using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.MovieDownload;

namespace MovieSearch.Droid
{
	[Activity (Theme = "@style/MyTheme", Label = "Movie search", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			this.SetContentView (Resource.Layout.Main);

            MovieDbFactory.RegisterSettings(new DBSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;


            // Get our UI controls from the loaded layout
            var movieEditText = this.FindViewById<EditText>(Resource.Id.movieEditText);

			//var movieTextView = this.FindViewById<TextView>(Resource.Id.movieTextView);

			var resultTextView = this.FindViewById<TextView>(Resource.Id.resultTextView);

			var searchButton = this.FindViewById<Button>(Resource.Id.searchButton);

		    var loading = this.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            loading.Visibility = ViewStates.Invisible;

			searchButton.Click += async  (sender, e) =>
			{
			    searchButton.Visibility = ViewStates.Gone;
                loading.Visibility = ViewStates.Visible;

				var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
				manager.HideSoftInputFromWindow(movieEditText.WindowToken, 0);

                var movieInfoResponse = await movieApi.SearchByTitleAsync(movieEditText.Text);
                searchButton.Visibility = ViewStates.Visible;
                loading.Visibility = ViewStates.Gone;
                resultTextView.Text = movieInfoResponse.Results[0].Title;
			};
		}
	}
}


