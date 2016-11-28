using System;
using DM.MovieApi;

using UIKit;
using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;

namespace MovieSearch.iOS
{
	public class ViewController : UIViewController
	{
        private const int HorizontalMargin = 20;

        private const int StartY = 80;

        private const int StepY = 50;

        private int _yCoord;

        public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            MovieDbFactory.RegisterSettings(new DBSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            this.View.BackgroundColor = UIColor.White;

            this._yCoord = StartY;

            var prompt = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
                Text = "Enter words in a movie title: "
            };
            this._yCoord += StepY;

            var movieField = new UITextField()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Title"
            };
            this._yCoord += StepY;

            var searchButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchButton.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
            searchButton.SetTitle("Get movie", UIControlState.Normal);
            this._yCoord += StepY;

            var searchResult = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50)
            };
            this._yCoord += StepY;

            searchButton.TouchUpInside += async (sender, args) =>
            {
                movieField.ResignFirstResponder();
                ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(movieField.Text);

                searchResult.Text = response.Results[0].Title;
            };

            this.View.AddSubview(prompt);
            this.View.AddSubview(movieField);
            this.View.AddSubview(searchButton);
            this.View.AddSubview(searchResult);


		}
	}
}

