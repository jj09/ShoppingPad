using ShoppingPad.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPad.Common.ViewModels
{
    public class ShoppingListViewModel
    {
        public ObservableCollection<Item> Items; 

        public ShoppingListViewModel()
        {
            Items = new ObservableCollection<Item>();
        }

        public void Add(Item item)
        {
            this.Items.Add(item);
        }

        public void Remove(Item item)
        {
            this.Items.Remove(item);
        }
    }
}
