using System;
using DM.MovieApi;

using UIKit;
using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using MovieSearch.Model;
using System.Collections.Generic;

namespace MovieSearch.iOS.Controllers

{
	public class MovieController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 50;

		private int _yCoord;

		private Movies _movies;
		public MovieController()
		{
			this._movies = new Movies();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.Title = "Search";

			MovieDbFactory.RegisterSettings(new DBSettings());
			var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

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
				searchButton.Enabled = false;

				//loading
				var loading = CreateLoadingSpinner();
				this.View.AddSubview(loading);
				loading.StartAnimating();

				movieField.ResignFirstResponder();
				ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(movieField.Text);

                _movies.MovieList.Clear();

                foreach (var r in response.Results)
				{
                    ApiQueryResponse<MovieCredit> resp = await movieApi.GetCreditsAsync(r.Id);

                    var movie = new Model.Movie()
                    {
                        Title = r.Title,
                        Year = r.ReleaseDate.Year.ToString(),
                        Overview = r.Overview,
                        Poster = r.PosterPath
                        //TODO: Cast
                    };

					foreach (var g in r.Genres)
					{
						movie.Genre.Add(g.Name);
					}

                    for (int i = 0; i < resp.Item.CastMembers.Count || i < 3; i++)
                    {
                        movie.Cast.Add(resp.Item.CastMembers[i].Name);
                    }
                    
					_movies.MovieList.Add(movie);
				}

				this.NavigationController.PushViewController(new MovieListController(this._movies.MovieList), true);
                loading.StopAnimating();
				searchButton.Enabled = true;
				loading.StopAnimating();
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

