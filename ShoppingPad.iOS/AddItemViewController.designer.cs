// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ShoppingPad.iOS
{
	[Register ("AddItemViewController")]
	partial class AddItemViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton AddNewItemButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField NewItemTextField { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AddNewItemButton != null) {
				AddNewItemButton.Dispose ();
				AddNewItemButton = null;
			}
			if (NewItemTextField != null) {
				NewItemTextField.Dispose ();
				NewItemTextField = null;
			}
		}
	}
}
