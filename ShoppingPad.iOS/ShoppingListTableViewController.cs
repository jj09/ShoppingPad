using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.Helpers;
using System.Linq;
using ShoppingPad.Common.ViewModels;

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
			// Perform any additional setup after loading the view, typically from a nib.
			base.ViewDidLoad ();
			var table = new UITableView(View.Bounds); // defaults to Plain style
			table.Source = new TableSource();
			Add(table);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	public class TableSource : UITableViewSource 
	{
		ShoppingListViewModel ViewModel;
		string CellIdentifier = "TableCell";

		public TableSource ()
		{
			ViewModel = new ShoppingListViewModel(ServiceRegistrar.ShoppingService);
			ViewModel.Items.Add (new Item ("item1"));
			ViewModel.Items.Add (new Item ("item2"));
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
			{ cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier); }

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
