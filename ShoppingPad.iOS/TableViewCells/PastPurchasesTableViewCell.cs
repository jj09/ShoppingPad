using Foundation;
using System;
using UIKit;
using ShoppingPad.Common.Models;

namespace ShoppingPad.iOS
{
    public partial class PastPurchasesTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("PastPurchasesTableViewCell");
        public static readonly UINib Nib;

        public PastPurchasesTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public void UpdateCell(BoughtItem item, bool isOnShoppingList)
        {
            _countLabel.Text = item.BoughtCount.ToString();

            if (isOnShoppingList)
            {
                _titleLabel.Text = item.Title + " (added to shopping list)";
            }
            else
            {
                _titleLabel.Text = item.Title;

            }

            //var filterHeaderLabel = new NSMutableAttributedString($"{item.Title}{(string.IsNullOrEmpty(filterValue) ? "" : $": {filterValue}")}");
            //filterHeaderLabel.SetAttributes(new UIStringAttributes
            //{
            //    ForegroundColor = UIColor.Black,
            //    Font = UIFont.SystemFontOfSize(SizeHelpers.H1SystemFontSize)
            //}, new NSRange(0, filterName.Length));
        }
    }
}