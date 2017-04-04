using System;
using UIKit;
using ShoppingPad.Common.Interfaces;

namespace ShoppingPad.iOS
{
    public partial class PastPurchasesTableViewController : UITableViewController
    {
        public PastPurchasesTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView = new UITableView();
            TableView.Source = new PastPurchasesTableSource();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TableView.ReloadData();
        }
    }
}