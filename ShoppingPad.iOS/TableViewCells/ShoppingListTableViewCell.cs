using Foundation;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.ViewModels;
using System;
using UIKit;

namespace ShoppingPad.iOS
{
    public partial class ShoppingListTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ShoppingListTableViewCell");
        public static readonly UINib Nib;

        public ShoppingListTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public void UpdateCell(Item item, ShoppingListViewModel viewModel, UITableView tableView, NSIndexPath indexPath)
        {
            _itemTitleLabel.Text = item.Title;

            _checkboxButton.SetImage(UIImage.FromFile("Icons/checkbox-unchecked.png"), UIControlState.Normal);

            _checkboxButton.TouchUpInside += (s, e) =>
            {
                _checkboxButton.SetImage(UIImage.FromFile("Icons/checkbox-checked.png"), UIControlState.Normal);
                tableView.ReloadData();
                System.Threading.Thread.Sleep(1500);
                viewModel.Purchase(item);
                tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
            };
        }
    }
}