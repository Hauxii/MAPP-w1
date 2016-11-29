using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListController : UITableViewController
    {
        private List<string> _movieList;

        public MovieListController(List<string> movieList)
        {
            this._movieList = movieList;
            
        }

        public override void ViewDidLoad()
        {
            this.Title = "Movie list";
            this.View.BackgroundColor = UIColor.White;

            this.TableView.Source = new MovieListSource(this._movieList);
        }


    }
}