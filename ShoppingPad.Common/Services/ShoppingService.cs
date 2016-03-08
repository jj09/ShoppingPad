using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Interfaces;
using ShoppingPad.Common.Models;
using SQLite;
using System.IO;

namespace ShoppingPad.Common.Services
{
    public class ShoppingService : IShoppingService
    {
        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<BoughtItem> BoughtItems { get; set; }

        private SQLiteConnection _sqliteConnection;

        public ShoppingService(SQLiteConnection sqliteConnection)
        {
            // setup db
            //_sqliteConnection = new SQLiteConnection(dbPath);
            _sqliteConnection = sqliteConnection;
            _sqliteConnection.CreateTable<Item>();
            _sqliteConnection.CreateTable<BoughtItem>();

            var items = _sqliteConnection.Table<Item>().ToList();
            var boughtItems = _sqliteConnection.Table<BoughtItem>().ToList();

            Items = new ObservableCollection<Item>(items);
            BoughtItems = new ObservableCollection<BoughtItem>(boughtItems);
        }

        public void AddItem(Item item)
        {
            TryAddItemToShoppingList(item);
        }

        public void RemoveItem(Item item)
        {
            this.Items.Remove(item);
            _sqliteConnection.Delete(item);

            this.AddToBoughtItems(item);
        }

        public void TryAddItemToShoppingList(Item item)
        {
            if (this.Items.All(x => x.Title != item.Title))
            {
                this.Items.Add(item);
                _sqliteConnection.Insert(item);
            }
        }

        public void AddToBoughtItems(Item item)
        {
            var boughtItem = this.BoughtItems.FirstOrDefault(x => x.Title == item.Title);

            if (boughtItem != null)
            {
                ++boughtItem.BoughtCount;
                var bi = _sqliteConnection.Table<BoughtItem>().FirstOrDefault(x => x.Title == item.Title);
                bi.BoughtCount++;
                _sqliteConnection.Update(bi);
            }
            else
            {
                var newBoughtItem = new BoughtItem(item.Title);
                this.BoughtItems.Add(newBoughtItem);
                _sqliteConnection.Insert(newBoughtItem);
            }

            this.BoughtItems = new ObservableCollection<BoughtItem>(this.BoughtItems.OrderByDescending(x => x.BoughtCount));
        }
    }
}
