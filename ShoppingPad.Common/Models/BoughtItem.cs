using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPad.Common.Models
{
    public class BoughtItem : Item
    {
        public int BoughtCount { get; set; }

        public BoughtItem(string title) : base(title)
        {
            this.BoughtCount = 1;
        }
    }
}
