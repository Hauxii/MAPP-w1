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

        public void AddMovie(string title)
        {
            var movie = new Movie(title /*TODO: AND MOAR*/)
            {
                Title = title,
                //TODO: ADD MOAR
            };
            this._list.Add(movie);
        }

        //Getter
        public List<Movie> MovieList => this._list;
    }
}
