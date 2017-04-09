using Foundation;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.ViewModels;
using System;
using UIKit;
using CoreImage;
using CoreGraphics;

namespace ShoppingPad.iOS
{
    public partial class ShoppingListTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ShoppingListTableViewCell");
        public static readonly UINib Nib;


        public ShoppingListTableViewCell(IntPtr handle) : base(handle)
        {
        }

        public void UpdateCell(Item item, bool isChecked)
        {
            _itemTitleLabel.Text = item.Title;

            var path = $"Icons/checkbox-{(isChecked ? "" : "un")}checked.png";
            _checkboxImageView.Image = UIImage.FromFile(path);
            _checkboxImageView.Alpha = isChecked ? 1f : 0.4f;
        } 
    }
}