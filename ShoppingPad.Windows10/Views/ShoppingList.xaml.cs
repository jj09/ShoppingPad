using ShoppingPad.Common.Models;
using ShoppingPad.Common.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShoppingPad.Windows10.Helpers;

namespace ShoppingPad.Windows10
{
    public sealed partial class ShoppingList : Page
    {
        public ShoppingListViewModel ViewModel { get; set; }
        
        public ShoppingList()
        {
            this.InitializeComponent();

            ViewModel = new ShoppingListViewModel(ServiceRegistrar.ShoppingService);
            ShoppingListView.ItemsSource = ViewModel.Items;

            // Developer will want to return to none selection when selected items are zero
            ShoppingListView.SelectionChanged += OnSelectionChanged;

            // With this property we enable that the left edge tap visual indicator shows 
            // when user press the listviewitem left edge 
            // and also the ItemLeftEdgeTapped event will be fired 
            // when user releases the pointer
            ShoppingListView.IsItemLeftEdgeTapEnabled = true;

            // This is event that will be fired when user releases the pointer after
            // pressing on the left edge of the ListViewItem
            ShoppingListView.ItemLeftEdgeTapped += OnEdgeTapped;

            // We set the state of the commands on the appbar
            SetCommandsVisibility(ShoppingListView);

            // This is how devs can handle the back button
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }
        private void OnEdgeTapped(ListView sender, ListViewEdgeTappedEventArgs e)
        {
            // When user releases the pointer after pessing on the left edge of the item,
            // the ListView will switch to Multiple Selection 
            ShoppingListView.SelectionMode = ListViewSelectionMode.Multiple;
            // Also, we want the Left Edge Tap funcionality will be no longer enable. 
            ShoppingListView.IsItemLeftEdgeTapEnabled = false;
            // It's desirable that the Appbar shows the actions available for multiselect
            SetCommandsVisibility(ShoppingListView);
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When there are no selected items, the list returns to None selection mode.
            if (ShoppingListView.SelectedItems.Count == 0)
            {
                ShoppingListView.SelectionMode = ListViewSelectionMode.None;
                ShoppingListView.IsItemLeftEdgeTapEnabled = true;
                SetCommandsVisibility(ShoppingListView);
            }
        }
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            // We want to exit from the multiselect mode when pressing back button
            if (ShoppingListView.SelectionMode == ListViewSelectionMode.Multiple)
            {
                ShoppingListView.SelectedItems.Clear();
                e.Handled = true;
            }
        }
        private void SetCommandsVisibility(ListView listView)
        {
            if (listView.SelectionMode == ListViewSelectionMode.Multiple || listView.SelectedItems.Count > 1)
            {
                SelectAppBarBtn.Visibility = Visibility.Collapsed;
                CancelSelectionAppBarBtn.Visibility = Visibility.Visible;
                RemoveItemAppBarBtn.Visibility = Visibility.Visible;
            }
            else
            {
                SelectAppBarBtn.Visibility = Visibility.Visible;
                CancelSelectionAppBarBtn.Visibility = Visibility.Collapsed;
                RemoveItemAppBarBtn.Visibility = Visibility.Collapsed;
            }
        }
        private void SelectItems(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ShoppingListView.SelectionMode = ListViewSelectionMode.Multiple;
            ShoppingListView.IsItemLeftEdgeTapEnabled = false;
            SetCommandsVisibility(ShoppingListView);
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            var title = this.NewItem.Text;

            if (!string.IsNullOrEmpty(title))
            {
                ViewModel.Add(new Item(title));
            }

            this.NewItem.Text = "";
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            if (ShoppingListView.SelectedIndex != -1)
            {
                // When an item is removed from the underlying collection, the Listview is updated, 
                // hence the this.SelectedItems is updated as well. 
                // It's needed to copy the selected items collection to iterate over other collection that 
                // is not updated.
                var selectedItems = ShoppingListView.SelectedItems.Cast<Item>().ToList();

                foreach (var item in selectedItems)
                {
                    ViewModel.Remove(item);
                }
            }
        }

        private void CancelSelection(object sender, RoutedEventArgs e)
        {
            // If the list is multiple selection mode but there is no items selected, 
            // then the list should return to the initial selection mode.
            if (ShoppingListView.SelectedItems.Count == 0)
            {
                ShoppingListView.SelectionMode = ListViewSelectionMode.None;
                ShoppingListView.IsItemLeftEdgeTapEnabled = true;
                SetCommandsVisibility(ShoppingListView);
            }
            else
            {
                ShoppingListView.SelectedItems.Clear();
            }
        }

        private void ShowSliptView(object sender, RoutedEventArgs e)
        {
            MenuPane.SamplesSplitView.IsPaneOpen = !MenuPane.SamplesSplitView.IsPaneOpen;
        }
    }
}
