using System;
using System.Collections.Generic;
using System.Text;
using MovieSearch.Model;
using MovieSearch.iOS.Views;

using UIKit;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;

namespace MovieSearch.iOS.Controllers
{
	public class MovieTopRatedController : UIViewController
    {
		private Movies _movies;

		public MovieTopRatedController(Movies movies)
		{
			this._movies = movies;

			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
		}	

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Top rated movies";
			this.View.BackgroundColor = UIColor.White;

			var indicator = CreateLoadingSpinner();
			indicator.StartAnimating();
			this.View.AddSubview(indicator);

			//GET TOP RATED
			//PUSH MOVIELIST

			//NavigationController.PushViewController(new MovieListController(this._movies.MovieList), true);
			indicator.StopAnimating();

			//indicator.StopAnimating();
		}

		private UIActivityIndicatorView CreateLoadingSpinner()
		{
			UIActivityIndicatorView loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			loading.Frame = new CGRect((this.View.Bounds.Width /2) - 25, this.View.Bounds.Height / 2, 50, 50);
			loading.HidesWhenStopped = true;
			return loading;
		}
    }
}