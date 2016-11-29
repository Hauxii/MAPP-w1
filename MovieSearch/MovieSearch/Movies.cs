using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public class Movies
    {
        private List<string> _list;

        public Movies()
        {
            this._list = new List<string>();
        }

        //Getter
        public List<string> MovieList => this._list;
    }
}
