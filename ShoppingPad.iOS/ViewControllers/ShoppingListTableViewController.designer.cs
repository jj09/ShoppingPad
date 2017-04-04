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
    [Register ("ShoppingListTableViewController")]
    partial class ShoppingListTableViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem _addButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_addButton != null) {
                _addButton.Dispose ();
                _addButton = null;
            }
        }
    }
}