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
        private ShoppingService _shoppingService;
        private string _dbPath;

        public ShoppingListViewModelTests()
        {
            _dbPath = Guid.NewGuid().ToString();
            _shoppingService = new ShoppingService(_dbPath);
        }

        public void Dispose()
        {
            File.Delete(_dbPath);
        }

        [Fact]
        void Should_Be_Able_To_Add_Item()
        {
            // Arrange
            var vm = new ShoppingListViewModel(_shoppingService);
            var item = new Item("some bought item");

            // Act
            vm.Add(item);

            // Assert
            Assert.Contains<Item>(vm.Items, x => x == item);
            Assert.Contains<Item>(_shoppingService.Items, x => x == item);
        }

        [Fact]
        void Should_Be_Able_To_Remove_Item()
        {
            // Arrange
            var vm = new ShoppingListViewModel(_shoppingService);
            var item = new Item("some bought item");
            vm.Add(item);

            // Act
            vm.Remove(item);

            // Assert
            Assert.DoesNotContain<Item>(vm.Items, x => x == item);
            Assert.DoesNotContain<Item>(_shoppingService.Items, x => x == item);
        }
    }
}
