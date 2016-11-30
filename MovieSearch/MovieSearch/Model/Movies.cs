using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch.Model
{
    public class Movies
    {
        private List<Movie> _list;

        public Movies()
        {
            this._list = new List<Movie>();
        }

        public void AddMovie(Movie mov)
        {
            this._list.Add(mov);
        }

        //Getter
        public List<Movie> MovieList => this._list;
    }
}
