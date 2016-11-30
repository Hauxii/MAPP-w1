using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieSearch.Model;

using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieDetailController : UITableViewController
    {
		private Movie movie;
        public MovieDetailController(Movie movie)
        {
			this.movie = movie;
        }

        public override void ViewDidLoad()
        {
            this.Title = "Movie list";
            this.View.BackgroundColor = UIColor.White;
		}
    }
}