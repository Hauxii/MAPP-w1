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

namespace MovieSearch.iOS.Controllers

{
	public class MovieController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 50;

		private int _yCoord;

		private List<Model.Movie> _movieList;

		public MovieController(List<Model.Movie> movieList)
		{
			this._movieList = movieList;

			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
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
			    ImageDownloader getImage = new ImageDownloader(new StorageClient());
                

				//loading
				var loading = CreateLoadingSpinner();
				this.View.AddSubview(loading);
				loading.StartAnimating();

				movieField.ResignFirstResponder();
				ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(movieField.Text);

				this._movieList.Clear();

				var movieInfoList = response.Results;

				foreach (var r in movieInfoList)
				{
					ApiQueryResponse<MovieCredit> resp = await movieApi.GetCreditsAsync(r.Id);

					var localFilePath = getImage.LocalPathForFilename(r.PosterPath);
					var image = getImage.DownloadImage(r.PosterPath, localFilePath, CancellationToken.None);

					var castList = new List<string>();
					var genreList = new List<string>();

					//Getting genres
					foreach (var g in r.Genres)
					{
						genreList.Add(g.Name);
					}

					var castMembers = resp.Item.CastMembers;

					//Getting 3 cast members
					int k;
					for (k = 0; k < 3 && k < castMembers.Count; k++)
					{
						castList.Add(castMembers[k].Name);
					}

					Model.Movie movie = new Model.Movie()
					{
						ID = r.Id,
						Title = r.Title,
						Year = r.ReleaseDate.Year.ToString(),
						Overview = r.Overview,
						Poster = localFilePath,
						Cast = castList,
						Genre = genreList
					};

					populateMovieList(movie);
				}

				NavigationController.PushViewController(new MovieListController(this._movieList), true);
                loading.StopAnimating();
				searchButton.Enabled = true;
				loading.StopAnimating();
			};

			this.View.AddSubview(prompt);
			this.View.AddSubview(movieField);
			this.View.AddSubview(searchButton);
		}

		private void populateMovieList(Model.Movie movie)
		{
			this._movieList.Add(movie);
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

