using ShoppingPad.Common.Models;
using ShoppingPad.Common.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShoppingPad.Common.Helpers;

namespace ShoppingPad.Common
{
    public sealed partial class PastPurchases : Page
    {
        public PastPurchasesViewModel ViewModel { get; set; }
        
        public PastPurchases()
        {
            this.InitializeComponent();

            ViewModel = new PastPurchasesViewModel(ServiceRegistrar.ShoppingService);
            PastPurchasesListView.ItemsSource = ViewModel.Items;

            // Developer will want to return to none selection when selected items are zero
            PastPurchasesListView.SelectionChanged += OnSelectionChanged;

            // With this property we enable that the left edge tap visual indicator shows 
            // when user press the listviewitem left edge 
            // and also the ItemLeftEdgeTapped event will be fired 
            // when user releases the pointer
            PastPurchasesListView.IsItemLeftEdgeTapEnabled = true;

            // This is event that will be fired when user releases the pointer after
            // pressing on the left edge of the ListViewItem
            PastPurchasesListView.ItemLeftEdgeTapped += OnEdgeTapped;

            // We set the state of the commands on the appbar
            SetCommandsVisibility(PastPurchasesListView);

            // This is how devs can handle the back button
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }
        private void OnEdgeTapped(ListView sender, ListViewEdgeTappedEventArgs e)
        {
            // When user releases the pointer after pessing on the left edge of the item,
            // the ListView will switch to Multiple Selection 
            PastPurchasesListView.SelectionMode = ListViewSelectionMode.Multiple;
            // Also, we want the Left Edge Tap funcionality will be no longer enable. 
            PastPurchasesListView.IsItemLeftEdgeTapEnabled = false;
            // It's desirable that the Appbar shows the actions available for multiselect
            SetCommandsVisibility(PastPurchasesListView);
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When there are no selected items, the list returns to None selection mode.
            if (PastPurchasesListView.SelectedItems.Count == 0)
            {
                PastPurchasesListView.SelectionMode = ListViewSelectionMode.None;
                PastPurchasesListView.IsItemLeftEdgeTapEnabled = true;
                SetCommandsVisibility(PastPurchasesListView);
            }
        }
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            // We want to exit from the multiselect mode when pressing back button
            if (PastPurchasesListView.SelectionMode == ListViewSelectionMode.Multiple)
            {
                PastPurchasesListView.SelectedItems.Clear();
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
            PastPurchasesListView.SelectionMode = ListViewSelectionMode.Multiple;
            PastPurchasesListView.IsItemLeftEdgeTapEnabled = false;
            SetCommandsVisibility(PastPurchasesListView);
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            if (PastPurchasesListView.SelectedIndex != -1)
            {
                // When an item is removed from the underlying collection, the Listview is updated, 
                // hence the this.SelectedItems is updated as well. 
                // It's needed to copy the selected items collection to iterate over other collection that 
                // is not updated.
                var selectedItems = PastPurchasesListView.SelectedItems.Cast<BoughtItem>().ToList();

                foreach (var item in selectedItems)
                {
                    ViewModel.Items.Remove(item);
                }
            }
        }

        private void CancelSelection(object sender, RoutedEventArgs e)
        {
            // If the list is multiple selection mode but there is no items selected, 
            // then the list should return to the initial selection mode.
            if (PastPurchasesListView.SelectedItems.Count == 0)
            {
                PastPurchasesListView.SelectionMode = ListViewSelectionMode.None;
                PastPurchasesListView.IsItemLeftEdgeTapEnabled = true;
                SetCommandsVisibility(PastPurchasesListView);
            }
            else
            {
                PastPurchasesListView.SelectedItems.Clear();
            }
        }

        private void ShowSliptView(object sender, RoutedEventArgs e)
        {
            MenuPane.SamplesSplitView.IsPaneOpen = !MenuPane.SamplesSplitView.IsPaneOpen;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            var title = ((Button) sender).Tag as string;

            ServiceRegistrar.ShoppingService.TryAddItem(new Item(title));
        }
    }
}
