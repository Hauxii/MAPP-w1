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
            this._list = new List<string>
            {
                "item1",
                "item2"
            };
        }

        //Getter
        public List<string> Movies => this._list;
    }
}
