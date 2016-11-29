using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListController : UIViewController
    {
        private List<string> _movieList;

        public MovieListController(List<string> movieList)
        {
            this._movieList = movieList;
        }


    }
}