using System;
using System.Collections.Generic;
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
    public class ShoppingListViewModelTests
    {
        SQLiteConnection _sqliteConnection;

        public ShoppingListViewModelTests()
        {
            _sqliteConnection = new SQLiteConnection(Guid.NewGuid().ToString());
        }

        public void Dispose()
        {
            _sqliteConnection.Close();
            File.Delete(_sqliteConnection.DatabasePath);
        }

        [Fact]
        void Should_Be_Able_To_Add_Item()
        {
            // Arrange
            var shoppingService = new ShoppingService(_sqliteConnection);
            var vm = new ShoppingListViewModel(shoppingService);
            var item = new Item("some bought item");

            // Act
            vm.Add(item);

            // Assert
            Assert.Contains<Item>(vm.Items, x => x == item);
            Assert.Contains<Item>(shoppingService.Items, x => x == item);
        }

        [Fact]
        void Should_Be_Able_To_Remove_Item()
        {
            // Arrange
            var shoppingService = new ShoppingService(_sqliteConnection);
            var vm = new ShoppingListViewModel(shoppingService);
            var item = new Item("some bought item");
            vm.Add(item);

            // Act
            vm.Remove(item);

            // Assert
            Assert.DoesNotContain<Item>(vm.Items, x => x == item);
            Assert.DoesNotContain<Item>(shoppingService.Items, x => x == item);
        }
    }
}
