using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "Movie list")]
	//ListActivity is similar to TableViewController in ios
    public class MovieListActivity : ListActivity
    {
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			//ask the intent object to extract the list that the previous activity sentt
			var movieList = this.Intent.Extras.GetStringArrayList("titleList") ?? new string[0];

			//A list activity needs an adapter (data source))
			this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, movieList); 
        }
    }
}