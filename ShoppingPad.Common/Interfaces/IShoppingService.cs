using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Models;

namespace ShoppingPad.Common.Interfaces
{
    public interface IShoppingService
    {
        ObservableCollection<Item> Items { get; set; }

        ObservableCollection<BoughtItem> BoughtItems { get; set; }

        void AddItem(Item item);

        void Purchase(Item item);

        void Remove(Item item);

        void TryAddItemToShoppingList(Item item);
    }
}
