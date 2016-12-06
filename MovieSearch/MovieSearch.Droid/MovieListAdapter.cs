using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using MovieSearch.Model;

namespace MovieSearch.Droid
{
	public class MovieListAdapter : BaseAdapter<Movie>
	{
		private Activity _context;

		private List<Model.Movie> _movieList;

		public MovieListAdapter(Activity context, List<Movie> movieList)
		{
			this._context = context;
			this._movieList = movieList;
		}

		public override Movie this[int position]
		{
			get { return this._movieList[position]; }
		}

		public override int Count
		{
			get
			{
				return this._movieList.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;

			if (view == null)
			{
				view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
			}
			var movie = this._movieList[position];
		    view.FindViewById<TextView>(Resource.Id.title).Text = movie.Title;
		    string castLabel = "";
            for (int i = 0; i < movie.Cast.Count; i++)
            {
                castLabel += movie.Cast[i];
                if (i + 1 != movie.Cast.Count)
                {
                    castLabel += ", ";
                }
            }
            view.FindViewById<TextView>(Resource.Id.cast).Text = castLabel;

			var resourceId = this._context.Resources.GetIdentifier(movie.Poster, "drawable", this._context.PackageName);
			view.FindViewById<ImageView>(Resource.Id.poster).SetBackgroundResource(resourceId);
			return view;
		}
	}
}
