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
        static readonly string Tag = "ActionBarTabsSupport";

        private Fragment[] _fragments;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.Main);

            _fragments = new Fragment[]
                         {
                             new ShoppingListFragment(),
                             new PastPurchasesFragment(),
                         };

            AddTabToActionBar(Resource.String.shopping_list_tab_label);
            AddTabToActionBar(Resource.String.past_purchases_tab_label);
        }

        void AddTabToActionBar(int labelResourceId)
        {
            var tab = ActionBar.NewTab()
                                         .SetText(labelResourceId);
            tab.TabSelected += TabOnTabSelected;
            ActionBar.AddTab(tab);
        }

        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            var tab = (ActionBar.Tab)sender;

            Fragment frag = _fragments[tab.Position];
            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }
    }
}


 

