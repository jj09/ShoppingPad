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
using System.Collections.Specialized;

namespace ShoppingPad.Common.Services
{
    public class ShoppingService : IShoppingService
    {
        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<BoughtItem> BoughtItems { get; set; }

        private SQLiteConnection _sqliteConnection; // https://github.com/praeclarum/sqlite-net

        private object _locker = new object(); // SQLite Db locker

        public ShoppingService(SQLiteConnection sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;            
            _sqliteConnection.CreateTable<Item>();
            _sqliteConnection.CreateTable<BoughtItem>();

            var items = _sqliteConnection.Table<Item>().ToList();
            var boughtItems = _sqliteConnection.Table<BoughtItem>().ToList().OrderByDescending(x => x.BoughtCount);

            Items = new ObservableCollection<Item>(items);
            BoughtItems = new ObservableCollection<BoughtItem>(boughtItems);

            Items.CollectionChanged += Items_CollectionChanged;
            BoughtItems.CollectionChanged += BoughtItems_CollectionChanged;
        }

        private void BoughtItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var boughtItem = e.NewItems[0] as BoughtItem;

                lock (_locker)
                {
                    _sqliteConnection.Insert(boughtItem, typeof(BoughtItem));
                    _sqliteConnection.Commit();
                }
            }

            // implement INotifyPropertyChange to support this
            // http://stackoverflow.com/questions/2246777/raise-an-event-whenever-a-propertys-value-changed
            // http://stackoverflow.com/questions/1427471/observablecollection-not-noticing-when-item-in-it-changes-even-with-inotifyprop
            //if (e.Action == NotifyCollectionChangedAction.Replace)
            //{
            //    var boughtItem = e.NewItems[0] as BoughtItem;

            //    lock (_locker)
            //    {
            //        _sqliteConnection.Delete<BoughtItem>(boughtItem.Id);
            //        _sqliteConnection.Commit();
            //    }
            //}

            // not supported yet
            //if (e.Action == NotifyCollectionChangedAction.Remove)
            //{
            //    var boughtItem = e.OldItems[0] as BoughtItem;

            //    lock (_locker)
            //    {
            //        _sqliteConnection.Delete<BoughtItem>(boughtItem.Id);
            //        _sqliteConnection.Commit();
            //    }
            //}
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var item = e.NewItems[0] as Item;

                lock(_locker)
                {
                    _sqliteConnection.Insert(item);
                    _sqliteConnection.Commit();
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var item = e.OldItems[0] as Item;

                lock (_locker)
                {
                    _sqliteConnection.Delete<Item>(item.Id);
                    _sqliteConnection.Commit();
                }
            }
        }

        public void AddItem(Item item)
        {
            TryAddItemToShoppingList(item);
        }

        public void RemoveItem(Item item)
        {
            this.Items.Remove(item);
            this.AddToBoughtItems(item);
        }

        public void TryAddItemToShoppingList(Item item)
        {
            if (this.Items.All(x => x.Title != item.Title))
            {
                this.Items.Add(item);
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
                lock(_locker)
                {
                    _sqliteConnection.Update(bi);
                    _sqliteConnection.Commit();
                }
            }
            else
            {
                var newBoughtItem = new BoughtItem()
                {
                    Title = item.Title,
                    BoughtCount = 1
                };
                this.BoughtItems.Add(newBoughtItem);
            }

            this.BoughtItems = new ObservableCollection<BoughtItem>(this.BoughtItems.OrderByDescending(x => x.BoughtCount));
            BoughtItems.CollectionChanged += BoughtItems_CollectionChanged;
        }
    }
}
