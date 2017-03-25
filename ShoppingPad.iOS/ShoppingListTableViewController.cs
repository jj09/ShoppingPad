using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.Helpers;
using System.Linq;
using ShoppingPad.Common.ViewModels;
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

	public class ShoppingListTableSource : UITableViewSource 
	{
		ShoppingListViewModel ViewModel;
		string CellIdentifier = "TableCell";

		public ShoppingListTableSource ()
		{
			ViewModel = ServiceRegistrar.Container.Resolve<ShoppingListViewModel>();
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return ViewModel.Items.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			var item = ViewModel.Items.ElementAt(indexPath.Row);

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{
				cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier);
			}

			cell.TextLabel.Text = item.Title;

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			ViewModel.Remove(ViewModel.Items.ElementAt(indexPath.Row));
			tableView.ReloadData();
		}
	}
}
