using System;
using UIKit;
using System.Collections.Generic;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.Helpers;
using ShoppingPad.Common.Interfaces;
using Autofac;

namespace ShoppingPad.iOS
{
    public partial class ShoppingListTableViewController : UITableViewController
	{
		public ShoppingListTableViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            TableView = new UITableView(View.Bounds);
            TableView.Source = new ShoppingListTableSource();

            _addButton.Clicked += (s, e) =>
            {
                var addPopup = UIAlertController.Create("Enter name:", "", UIAlertControllerStyle.Alert);

                addPopup.AddTextField(x => { });

                addPopup.AddAction(UIAlertAction.Create("Add", UIAlertActionStyle.Default, x =>
                {
                    var title = addPopup.TextFields[0]?.Text;
                    ServiceRegistrar.Container.Resolve<IShoppingService>().TryAddItemToShoppingList(new Item(title));
                    TableView.ReloadData();
                }));

                addPopup.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, x =>
                {
                    // noop
                }));

                PresentViewController(addPopup, true, null);
            };
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			TableView.ReloadData();
		}
	}
}
