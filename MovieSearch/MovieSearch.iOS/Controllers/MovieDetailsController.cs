using MovieSearch.Model;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using DM.MovieApi;
using CoreGraphics;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
	public class MovieDetailController : UIViewController
	{
		private const int HorizontalMargin = 20;

		private const int StartY = 80;

		private const int StepY = 20;

		private int _yCoord;

		private MovieSearch.Model.Movie movie;

		public MovieDetailController(MovieSearch.Model.Movie movie)
		{
			this.movie = movie;
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			MovieDbFactory.RegisterSettings(new DBSettings());
			var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
			var movieDetails = await movieApi.FindByIdAsync(movie.ID);

			this.movie.Runtime = movieDetails.Item.Runtime.ToString();

			this.Title = "Movie details";
			this.View.BackgroundColor = UIColor.White;

			this._yCoord = StartY;

			var title = CreateTitle();

			this._yCoord += StepY;

			var poster = CreatePoster();

			var runtime = CreateRunningTimeAndGenre();
			this._yCoord += StepY;

			var overview = CreateOverview();

			this.View.AddSubview(title);
			this.View.AddSubview(overview);
			this.View.AddSubview(runtime);


		}

		private UILabel CreateTitle()
		{
			string titleyear = movie.Title + " (" + movie.Year + ")";
			var title = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - HorizontalMargin * 2, 20),
				Font = UIFont.FromName("Marion", 22f),
				Text = titleyear,
			};
			return title;
		}

		private UILabel CreateOverview()
		{
			var overview = new UILabel()
			{
				Frame = new CGRect(100, this._yCoord, this.View.Bounds.Width - HorizontalMargin * 2, 100),
				Font = UIFont.FromName("Arial", 12f),
				Text = movie.Overview
			};


			return overview;
		}

		private UILabel CreateRunningTimeAndGenre()
		{
			var subtitle = new UILabel()
			{
				Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - HorizontalMargin * 2, 20),
				Font = UIFont.FromName("Arial", 15f),
				Text = this.movie.Runtime + " min | "
			};
			for (int i = 0; i < movie.Genre.Count; i++)
			{
				subtitle.Text += movie.Genre[i];
				if (i+1 != movie.Genre.Count)
				{
					subtitle.Text += ", ";
				}
			}
			return subtitle;
		}

		private UIImage CreatePoster()
		{
			return null; //movie.Poster;
		}
	}
}