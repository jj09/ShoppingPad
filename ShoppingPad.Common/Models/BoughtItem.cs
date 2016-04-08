using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPad.Common.Models
{
    public class BoughtItem : Item
    {
        // crashes in UWP as duplicated prop
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

        public int BoughtCount { get; set; }

        public BoughtItem()
        {

        }

        public BoughtItem(string title) : base(title)
        {
            this.BoughtCount = 1;
        }
    }
}
