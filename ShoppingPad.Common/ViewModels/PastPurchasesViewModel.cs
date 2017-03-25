using ShoppingPad.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Interfaces;

namespace ShoppingPad.Common.ViewModels
{
    public class PastPurchasesViewModel
    {
        public ObservableCollection<BoughtItem> Items => this._shoppingService.BoughtItems;

        private readonly IShoppingService _shoppingService;

        public PastPurchasesViewModel(IShoppingService shoppingService)
        {
            this._shoppingService = shoppingService;
        }

        public void Add(BoughtItem item)
        {
            var boughtItem = Items.FirstOrDefault(x => x.Title == item.Title);
            if (boughtItem == null)
            {
                this.Items.Add(item);
            }
            else
            {
                boughtItem.BoughtCount++;
            }
        }


        public void CopyItemToShoppingList(Item item)
        {
            this._shoppingService.TryAddItemToShoppingList(new Item(item.Title));
        }
    }
}
