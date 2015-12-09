using System;
using Foundation;
using UIKit;
using ShoppingPad.Common.Helpers;
using ShoppingPad.Common.Models;

namespace ShoppingPad.iOS
{
	public partial class AddItemViewController : UIViewController
	{
		public AddItemViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			AddNewItemButton.TouchUpInside += (sender, e) => {
				var title = NewItemTextField.Text;
				ServiceRegistrar.ShoppingService.TryAddItem (new Item (title));
                NavigationController.PopToRootViewController(true);
			};
		}
    }
}


