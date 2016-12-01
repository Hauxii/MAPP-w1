using System;
using DM.MovieApi;

using UIKit;
using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using MovieSearch.Model;
using System.Collections.Generic;
using System.Threading;
using MovieSearch.MovieDownload;
using System.Threading.Tasks;

namespace MovieSearch.iOS.Controllers

{
	public class MovieController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 50;

		private int _yCoord;

		private Movies _movies;

		private MovieResourceProvider _imgDl;

		public MovieController(Movies movies)
		{ 
			this._movies = movies;

			this._imgDl = new MovieResourceProvider();

			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.Title = "Search";

			this.View.BackgroundColor = UIColor.White;

			this._yCoord = StartY;

			var prompt = CreatePrompt();

			this._yCoord += StepY;

			var movieField = CreateMovieField();

			this._yCoord += StepY;

			var searchButton = CreateButton("Get movie");
			this._yCoord += StepY;

			searchButton.TouchUpInside += async (sender, args) =>
			{
				if (movieField.Text.Length == 0)
				{
					var okAlertController = UIAlertController.Create("Invalid input", "Please enter a valid input", UIAlertControllerStyle.Alert);
					okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
					this.PresentViewController(okAlertController, true, null);
				}
				else {
					searchButton.Enabled = false;
					var loading = CreateLoadingSpinner();
					this.View.AddSubview(loading);
					loading.StartAnimating();

					MovieResourceProvider resourceProvider = new MovieResourceProvider();

					await resourceProvider.GetMoviesByTitle(this._movies, movieField.Text);

					NavigationController.PushViewController(new MovieListController(this._movies.MovieList), true);
					loading.StopAnimating();
					searchButton.Enabled = true;
					loading.StopAnimating();
				}


			};


			this.View.AddSubview(prompt);
			this.View.AddSubview(movieField);
			this.View.AddSubview(searchButton);
		}

		private UIButton CreateButton(string title)
		{
			var navButton = UIButton.FromType(UIButtonType.RoundedRect);
			navButton.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
			navButton.SetTitle(title, UIControlState.Normal);
			return navButton;
		}

		private UITextField CreateMovieField()
		{
			var movieField = new UITextField()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
				BorderStyle = UITextBorderStyle.RoundedRect,
				Placeholder = "Title"
			};
			return movieField;
		}

		private UILabel CreatePrompt()
		{
			var prompt = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
				Text = "Enter words in a movie title: "
			};
			return prompt;
		}

		private UIActivityIndicatorView CreateLoadingSpinner()
		{
			UIActivityIndicatorView loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			loading.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
			loading.HidesWhenStopped = true;
			return loading;
		}
    }
}

