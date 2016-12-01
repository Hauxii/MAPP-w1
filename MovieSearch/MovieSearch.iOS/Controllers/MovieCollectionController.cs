using System;
using System.Collections.Generic;
using System.Text;
using MovieSearch.Model;
using MovieSearch.iOS.Views;


using UIKit;

namespace MovieSearch.iOS.Controllers
{
	public class MovieCollectionController : UICollectionViewController
    {
		private List<Movie> _movieList;

		public MovieCollectionController(UICollectionViewFlowLayout layout, List<Movie> movieList) : base(layout)
		{
			this._movieList = movieList;
			this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Favorites, 0);
		}	

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.Title = "Collection";

			this.CollectionView.BackgroundColor = UIColor.White;
			this.CollectionView.ContentSize = this.View.Frame.Size;
			this.CollectionView.ContentInset = new UIEdgeInsets(10, 10, 10, 10);

			this.CollectionView.RegisterClassForCell(typeof(CustomCollectionCell), MovieCollectionSource.MovieCollectionCellId);
			//this.CollectionView.DataSource = new MovieCollectionSource();
		}
    }
}