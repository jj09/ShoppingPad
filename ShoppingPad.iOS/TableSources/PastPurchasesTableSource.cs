using System;
using System.Linq;
using Foundation;
using ShoppingPad.Common.Helpers;
using ShoppingPad.Common.ViewModels;
using UIKit;
using Autofac;
using ShoppingPad.Common.Interfaces;

namespace ShoppingPad.iOS
{
    public class PastPurchasesTableSource : UITableViewSource
    {
        private PastPurchasesViewModel _viewModel;

        public PastPurchasesTableSource()
        {
            _viewModel = ServiceRegistrar.Container.Resolve<PastPurchasesViewModel>();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _viewModel.Items.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (PastPurchasesTableViewCell)tableView.DequeueReusableCell(PastPurchasesTableViewCell.Key);

            var item = _viewModel.Items.ElementAt(indexPath.Row);
            var isOnShoppingList = ServiceRegistrar.Container.Resolve<IShoppingService>().Items.FirstOrDefault(x => x.Title == item.Title) != null;

            cell.UpdateCell(item, isOnShoppingList);

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            _viewModel.CopyItemToShoppingList(_viewModel.Items.ElementAt(indexPath.Row));
            tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
            tableView.DeselectRow(indexPath, true);
        }
    }
}