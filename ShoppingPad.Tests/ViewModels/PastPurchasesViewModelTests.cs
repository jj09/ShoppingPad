using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.Services;
using ShoppingPad.Common.ViewModels;
using Xunit;
using SQLite;
using System.IO;

namespace ShoppingPad.Tests.ViewModels
{
    public class PastPurchasesViewModelTests
    {
        SQLiteConnection _sqliteConnection;

        public PastPurchasesViewModelTests()
        {
            _sqliteConnection = new SQLiteConnection(Guid.NewGuid().ToString());
        }

        public void Dispose()
        {
            _sqliteConnection.Close();
            File.Delete(_sqliteConnection.DatabasePath);
        }

        [Fact]
        public void Add_Item()
        {
            // Arrange
            var shoppingService = new ShoppingService(_sqliteConnection);
            var vm = new PastPurchasesViewModel(shoppingService);
            var item = new BoughtItem("item 1");

            // Act
            vm.Add(item);

            // Assert
            Assert.Contains<BoughtItem>(vm.Items, x => x == item);
        }

        [Fact]
        public void Add_Item_Already_Added()
        {
            // Arrange
            var shoppingService = new ShoppingService(_sqliteConnection);
            var vm = new PastPurchasesViewModel(shoppingService);
            var item = new BoughtItem("item 1");
            vm.Add(item);
            var item2 = new BoughtItem("item 1");

            // Act
            vm.Add(item);

            // Assert
            Assert.Equal(1, vm.Items.Count);
            Assert.Equal(2, vm.Items.First().BoughtCount);
        }

        [Fact]
        public void Copy_Item_To_Shopping_List()
        {
            // Arrange
            var shoppingService = new ShoppingService(_sqliteConnection);
            var vm = new PastPurchasesViewModel(shoppingService);
            var itemTitle = "item1";
            var item = new BoughtItem(itemTitle);
            vm.Add(item);

            // Act
            vm.CopyItemToShoppingList(item);

            // Assert
            Assert.Equal(1, shoppingService.Items.Count);
            Assert.Contains<Item>(shoppingService.Items, x => x.Title == itemTitle);
        }
    }
}
