using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPad.Common.Models
{
    public class Item
    {
        public Item(string title)
        {
            this.Title = title;
        }

        public string Title;
    }
}
