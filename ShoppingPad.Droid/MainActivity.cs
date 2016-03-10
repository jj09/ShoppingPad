using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;
using ShoppingPad.Common.Helpers;

namespace ShoppingPad.Droid
{
    [Activity(Label = "ShoppingPad", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Fragment[] _fragments;

        public static SQLiteConnection SqliteConnection;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // init db
            var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, ServiceRegistrar.DbFileName);
            SqliteConnection = new SQLiteConnection(path);

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
            var tab = ActionBar.NewTab().SetText(labelResourceId);
            tab.TabSelected += TabOnTabSelected;
            ActionBar.AddTab(tab);
        }

        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            var tab = (ActionBar.Tab)sender;

            Fragment frag = _fragments[tab.Position];
            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SqliteConnection.Commit();
            SqliteConnection.Close();
        }

        protected override void OnPause()
        {
            base.OnPause();
            SqliteConnection.Commit();
        }
    }
}