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
    public class ShoppingListFragment : Fragment
    {
        public ShoppingListViewModel ViewModel { get; set; }

        private ListView _shoppingListView;

        private LayoutInflater _inflater;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this._inflater = inflater;
            View view = inflater.Inflate(Resource.Layout.ic_tab_shopping_list, null);
            
            ViewModel = new ShoppingListViewModel(ServiceRegistrar.ShoppingService);

            _shoppingListView = view.FindViewById<ListView>(Resource.Id.ShoppingListView);

            var newItemEditText = view.FindViewById<EditText>(Resource.Id.NewItemEditText);

            Button addItemButton = view.FindViewById<Button>(Resource.Id.AddButton);

            _shoppingListView.Adapter = ViewModel.Items.GetAdapter(GetItemView);

            addItemButton.Click += delegate
            {
                var title = newItemEditText.Text;

                if (!string.IsNullOrEmpty(title))
                {
                    ViewModel.Add(new Item(title));
                }

                newItemEditText.Text = "";
            };

            _shoppingListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                ViewModel.Remove(this.ViewModel.Items.ElementAt(e.Position));
            };

            return view;
        }

        private View GetItemView(int position, Item item, View convertView)
        {
            var view = convertView ?? this._inflater.Inflate(Resource.Layout.RowItem, null);

            var title = view.FindViewById<TextView>(Resource.Id.Title);
            title.Text = item.Title;

            return view;
        }
    }
}