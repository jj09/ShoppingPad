using System;
using System.Linq;
using Foundation;
using ShoppingPad.Common.Helpers;
using ShoppingPad.Common.ViewModels;
using UIKit;
using Autofac;

namespace ShoppingPad.iOS
{
    public class PastPurchasesTableSource : UITableViewSource
    {
        PastPurchasesViewModel ViewModel;
        string CellIdentifier = "TableCell";

        public PastPurchasesTableSource()
        {
            ViewModel = ServiceRegistrar.Container.Resolve<PastPurchasesViewModel>();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ViewModel.Items.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            var item = ViewModel.Items.ElementAt(indexPath.Row);

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            cell.TextLabel.Text = $"({item.BoughtCount}) {item.Title}";

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            ViewModel.CopyItemToShoppingList(ViewModel.Items.ElementAt(indexPath.Row));
            tableView.DeselectRow(indexPath, true);
        }
    }
}