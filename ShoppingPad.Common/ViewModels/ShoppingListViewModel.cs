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
    public class ShoppingListViewModel
    {
        public ObservableCollection<Item> Items => this._shoppingService.Items;

        private readonly IShoppingService _shoppingService;

        public ShoppingListViewModel(IShoppingService shoppingService)
        {
            this._shoppingService = shoppingService;
        }

        public void Add(Item item)
        {
            this._shoppingService.AddItem(item);
        }

        public void Remove(Item item)
        {
            this._shoppingService.RemoveItem(item);
        }
    }
}
