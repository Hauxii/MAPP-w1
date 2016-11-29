using System;
using DM.MovieApi;

using UIKit;
using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;

namespace MovieSearch.iOS
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

            this.Title = "TITLE";

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

            var searchResult = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50)
            };
            this._yCoord += StepY;

            var navButton = this.CreateButton("See movie list");

            searchButton.TouchUpInside += async (sender, args) =>
            {
                movieField.ResignFirstResponder();
                ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(movieField.Text);

                searchResult.Text = response.Results[0].Title;
                //add to list?
                foreach (var r in response.Results)
                {
                    _movies.Movies.Add(r.Title);
                }
                //TODO: ADD LOADING BAR
            };

            navButton.TouchUpInside += (sender, args) =>
            {
                movieField.ResignFirstResponder();
                this.NavigationController.PushViewController(new MovieListController(this._movies.Movies), true);
            };

            this.View.AddSubview(prompt);
            this.View.AddSubview(movieField);
            this.View.AddSubview(searchButton);
            this.View.AddSubview(searchResult);


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
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2*HorizontalMargin, 50),
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
    }
}

