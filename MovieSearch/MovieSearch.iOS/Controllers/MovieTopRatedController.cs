using System;
using System.Collections.Generic;
using System.Text;
using MovieSearch.Model;
using MovieSearch.iOS.Views;

using UIKit;

namespace MovieSearch.iOS.Controllers
{
	public class MovieTopRatedController : UITableViewController
    {
		private List<Movie> _movieList;

		public MovieTopRatedController(List<Movie> movieList)
		{
			this._movieList = movieList;
			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
		}	

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Top rated movies";
			this.View.BackgroundColor = UIColor.White;
		}
    }
}