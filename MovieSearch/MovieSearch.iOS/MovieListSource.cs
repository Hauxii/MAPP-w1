using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListSource : UITableViewSource
    {
        private List<string> _movieList;

        public readonly NSString MovieListCellId = new NSString("MovieListCell");

        public MovieListSource(List<string> _movieList)
        {
            this._movieList = _movieList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(MovieListCellId);
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, this.MovieListCellId);
            }

            int row = indexPath.Row;
            cell.TextLabel.Text = this._movieList[row];
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movieList.Count;
        }
    }
}