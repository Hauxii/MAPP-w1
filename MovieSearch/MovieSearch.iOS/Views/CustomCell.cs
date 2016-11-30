using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MovieSearch.iOS.Views
{
    public class CustomCell : UITableViewCell
    {
        private UILabel _titleLabel, _yearLabel;
        private UIImageView _imageView;

        public CustomCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this._imageView = new UIImageView(); //TODO: GET IMAGE FROM API

            this._titleLabel = new UILabel()
            {
                Font = UIFont.FromName("Marion-Italic", 22f),
                TextColor = UIColor.FromRGB(255, 0, 0),
                BackgroundColor = UIColor.Clear //MAYBE SKIP?
            };
            
            this.ContentView.AddSubviews(new UIView[] {this._imageView, this._titleLabel});
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._imageView.Frame = new CGRect(this.ContentView.Bounds.Width - 60, 5, 33, 33);
            this._titleLabel.Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 60, 25);
        }

        public void UpdateCell(string title, string year, string image)
        {
            this._imageView.Image = UIImage.FromFile(image); //TODO: GET IMAGE FROM API
            this._titleLabel.Text = title;
            this._yearLabel.Text = year;
        }


    }
}
