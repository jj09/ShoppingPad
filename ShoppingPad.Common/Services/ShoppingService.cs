using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Interfaces;
using ShoppingPad.Common.Models;

namespace ShoppingPad.Common.Services
{
    public class ShoppingService : IShoppingService
    {
        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<BoughtItem> BoughtItems { get; set; }

        public ShoppingService()
        {
            Items = new ObservableCollection<Item>();
            BoughtItems = new ObservableCollection<BoughtItem>();
        }

        public void AddItem(Item item)
        {
            this.Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            this.Items.Remove(item);

            this.AddToBoughtItems(item);
        }

        public void AddToBoughtItems(Item item)
        {
            var boughtItem = this.BoughtItems.FirstOrDefault(x => x.Title == item.Title);

            if (boughtItem != null)
            {
                ++boughtItem.BoughtCount;
            }
            else
            {
                this.BoughtItems.Add(new BoughtItem(item.Title));
            }

            this.BoughtItems = new ObservableCollection<BoughtItem>(this.BoughtItems.OrderByDescending(x => x.BoughtCount));
        }
    }
}
