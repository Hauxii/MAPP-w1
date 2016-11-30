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
            this._imageView = new UIImageView();

            this._titleLabel = new UILabel()
            {
                Font = UIFont.FromName("Marion", 22f),
                TextColor = UIColor.FromRGB(60, 60, 60),
            };

            this._yearLabel = new UILabel()
            {
                Font = UIFont.FromName("Marion-Italic", 15f), //TODO: ERLA FIX FONT PLS
                TextColor = UIColor.FromRGB(130, 130, 130), //TODO: ERLA FIX THIS COLOR FIASKO
            };
            
            this.ContentView.AddSubviews(new UIView[] {this._imageView, this._titleLabel, this._yearLabel});
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._imageView.Frame = new CGRect(this.ContentView.Bounds.Width - 60, 5, 33, 33);
            this._titleLabel.Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 60, 25);
            this._yearLabel.Frame = new CGRect(5, 27, this.ContentView.Bounds.Width - 60, 20);
        }

        public void UpdateCell(string title, string year, string image)
        {
            this._imageView.Image = UIImage.FromFile(string.Empty); //TODO: GET IMAGE FROM API
            this._titleLabel.Text = title;
            this._yearLabel.Text = year;

			this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }
    }
}
