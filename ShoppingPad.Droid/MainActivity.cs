using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ShoppingPad.Common.ViewModels;
using ShoppingPad.Common.Helpers;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Helpers;
using ShoppingPad.Common.Models;

//using Validation;

namespace ShoppingPad.Droid
{
    [Activity(Label = "ShoppingPad", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public ShoppingListViewModel ViewModel { get; set; }

        private ListView _shoppingListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ViewModel = new ShoppingListViewModel(ServiceRegistrar.ShoppingService);

            _shoppingListView = FindViewById<ListView>(Resource.Id.ShoppingListView);

            var newItemEditText = FindViewById<EditText>(Resource.Id.NewItemEditText);

            // Get our button from the layout resource,
            // and attach an event to it
            Button addItemButton = FindViewById<Button>(Resource.Id.AddButton);

            _shoppingListView.Adapter = ViewModel.Items.GetAdapter(GetItemView);

            addItemButton.Click += delegate
            {
                var title = newItemEditText.Text;
                //ViewModel.Items.Add(new Item(title));
                if (!string.IsNullOrEmpty(title))
                {
                    ServiceRegistrar.ShoppingService.TryAddItem(new Item(title));
                }
                newItemEditText.Text = "";
            };

            _shoppingListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                //Get our item from the list adapter
                //var item = this._shoppingListView.Adapter.GetItem(e.Position);

                ViewModel.Remove(this.ViewModel.Items.ElementAt(e.Position));
            };
        }

        private View GetItemView(int position, Item item, View convertView)
        {
            var view = convertView ?? LayoutInflater.Inflate(Resource.Layout.RowItem, null);

            var title = view.FindViewById<TextView>(Resource.Id.Title);

            title.Text = item.Title;

            return view;
        }
    }
}


 

