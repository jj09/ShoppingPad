using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPad.Common.Models
{
    public class Item
    {
        public Item()
        {

        }

        public Item(string title)
        {
            this.Title = title;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title;
    }
}
