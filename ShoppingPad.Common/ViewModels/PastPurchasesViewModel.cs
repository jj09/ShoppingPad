using ShoppingPad.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPad.Common.ViewModels
{
    public class PastPurchasesViewModel
    {
        public ObservableCollection<BoughtItem> Items;

        public PastPurchasesViewModel()
        {
            Items = new ObservableCollection<BoughtItem>();
        }

        public void Add(BoughtItem boughtItem)
        {
            Items.Add(boughtItem);
        }
    }
}
