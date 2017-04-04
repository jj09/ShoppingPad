using System;

using Foundation;
using UIKit;
using ShoppingPad.Common.Helpers;
using System.Linq;
using ShoppingPad.Common.ViewModels;
using Autofac;

namespace ShoppingPad.iOS
{
    public class ShoppingListTableSource : UITableViewSource 
	{
        const string CellIdentifier = "TableCell";

        private ShoppingListViewModel _viewModel;
		
		public ShoppingListTableSource ()
		{
			_viewModel = ServiceRegistrar.Container.Resolve<ShoppingListViewModel>();
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _viewModel.Items.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			var item = _viewModel.Items.ElementAt(indexPath.Row);

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
			_viewModel.Purchase(_viewModel.Items.ElementAt(indexPath.Row));
			tableView.ReloadData();
		}

        // deleting from the list: https://developer.xamarin.com/guides/ios/user_interface/tables/part_4_-_editing/#Swipe_to_Delete
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    _viewModel.Remove(_viewModel.Items.ElementAt(indexPath.Row));
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
                case UITableViewCellEditingStyle.None:
                    break;
            }
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you wish to disable editing for a specific indexPath or for all rows
        }
    }
}
