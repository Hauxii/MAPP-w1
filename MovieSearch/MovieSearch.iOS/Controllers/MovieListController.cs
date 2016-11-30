using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieSearch.Model;

using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieListController : UITableViewController
    {
        private List<Movie> _movieList;

        public MovieListController(List<Movie> movieList)
        {
            this._movieList = movieList;
            
        }

        public override void ViewDidLoad()
        {
            this.Title = "Movie list";
            this.View.BackgroundColor = UIColor.White;

            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);
        }

        public void OnSelectedMovie(int row)
        {
            var okAlertController = UIAlertController.Create("Selected movie", _movieList[row].Title, UIAlertControllerStyle.Alert);

            //TODO: CHANGE TO NEW PAGE
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            this.PresentViewController(okAlertController, true, null);
        }

    }
}