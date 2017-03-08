using System;
using System.Linq;
using Foundation;
using ShoppingPad.Common.Helpers;
using ShoppingPad.Common.ViewModels;
using UIKit;

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
            
            // Perform any additional setup after loading the view, typically from a nib.
            var table = new UITableView(); // defaults to Plain style
            table.Source = new PastPurchasesTableSource();
            TableView = table;
            TableView.ContentInset = new UIEdgeInsets(50, 0, 0, 0);            
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TableView.ReloadData();
        }
    }

    public class PastPurchasesTableSource : UITableViewSource
    {
        PastPurchasesViewModel ViewModel;
        string CellIdentifier = "TableCell";

        public PastPurchasesTableSource()
        {
            ViewModel = new PastPurchasesViewModel(ServiceRegistrar.ShoppingService(Application.SqliteConnection));
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