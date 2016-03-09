using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using ShoppingPad.Common.Helpers;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.ViewModels;

namespace ShoppingPad.Droid
{
    public class PastPurchasesFragment : Fragment
    {
        public PastPurchasesViewModel ViewModel { get; set; }

        private ListView _pastPurchasesListView;

        private LayoutInflater _inflater;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this._inflater = inflater;

            View view = inflater.Inflate(Resource.Layout.ic_tab_past_purchases, null);
            
            ViewModel = new PastPurchasesViewModel(ServiceRegistrar.ShoppingService(MainActivity.SqlLiteConnection));

            _pastPurchasesListView = view.FindViewById<ListView>(Resource.Id.PastPurchasesListView);

            _pastPurchasesListView.Adapter = ViewModel.Items.GetAdapter(GetItemView);

            _pastPurchasesListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                ViewModel.CopyItemToShoppingList(this.ViewModel.Items.ElementAt(e.Position));
            };

            return view;
        }

        private View GetItemView(int position, BoughtItem item, View convertView)
        {
            var view = convertView ?? this._inflater.Inflate(Resource.Layout.PurchasedItem, null);

            var count = view.FindViewById<TextView>(Resource.Id.Count);
            count.Text = item.BoughtCount.ToString();

            var title = view.FindViewById<TextView>(Resource.Id.Title);
            title.Text = item.Title;

            return view;
        }
    }
}