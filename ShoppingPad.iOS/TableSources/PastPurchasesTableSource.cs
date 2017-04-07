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
            var cell = (PastPurchasesTableViewCell)tableView.DequeueReusableCell(PastPurchasesTableViewCell.Key);

            var item = ViewModel.Items.ElementAt(indexPath.Row);

            cell.UpdateCell(item, true);

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            ViewModel.CopyItemToShoppingList(ViewModel.Items.ElementAt(indexPath.Row));
            tableView.DeselectRow(indexPath, true);
        }
    }
}