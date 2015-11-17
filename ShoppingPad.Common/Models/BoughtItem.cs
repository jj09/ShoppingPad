using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPad.Common.Models
{
    public class BoughtItem
    {
        public string Title { get; set; }
        public int BoughtCount { get; set; }

        public BoughtItem(string title)
        {
            this.Title = title;
            this.BoughtCount = 1;
        }
    }
}
