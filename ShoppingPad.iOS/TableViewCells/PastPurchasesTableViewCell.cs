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

            var addedToShoppingListString = " (added to shopping list)";

            var titleLabel = new NSMutableAttributedString($"{item.Title}{(isOnShoppingList ? addedToShoppingListString : "")}");
            titleLabel.SetAttributes(new UIStringAttributes
            {
                ForegroundColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(18)
            }, new NSRange(0, item.Title.Length));

            if (isOnShoppingList)
            {
                titleLabel.SetAttributes(new UIStringAttributes
                {
                    ForegroundColor = UIColor.Gray,
                    Font = UIFont.SystemFontOfSize(13)
                }, new NSRange(item.Title.Length, addedToShoppingListString.Length));
            }

            _titleLabel.AttributedText = titleLabel;
        }
    }
}