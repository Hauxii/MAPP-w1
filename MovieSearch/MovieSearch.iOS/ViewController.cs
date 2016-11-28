using System;
using DM.MovieApi;

using UIKit;
using CoreGraphics;

namespace MovieSearch.iOS
{
	public partial class ViewController : UIViewController
	{
        private const int HorizontalMargin = 20;

        private const int StartY = 80;

        private const int StepY = 50;

        private int _yCoord;

        public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            MovieDbFactory.RegisterSettings(new DBSettings());


            this.View.BackgroundColor = UIColor.White;

            this._yCoord = StartY;

            var prompt = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
                Text = "Enter words in a movie title: "
            };
            this._yCoord += StepY;

            var nameField = new UITextField()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Title"
            };
            this._yCoord += StepY;

            var greetingButton = UIButton.FromType(UIButtonType.RoundedRect);
            greetingButton.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
            greetingButton.SetTitle("Get movie", UIControlState.Normal);
            this._yCoord += StepY;

            var greetingLabel = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50)
            };
            this._yCoord += StepY;

            greetingButton.TouchUpInside += (sender, args) =>
            {
                nameField.ResignFirstResponder();
                //greetingLabel.Text = "Hello " + nameField.Text;
            };

            this.View.AddSubview(prompt);
            this.View.AddSubview(nameField);
            this.View.AddSubview(greetingButton);
            this.View.AddSubview(greetingLabel);


		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

