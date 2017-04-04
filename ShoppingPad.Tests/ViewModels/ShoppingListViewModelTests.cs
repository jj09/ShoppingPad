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
using ShoppingPad.Tests.Base;

namespace ShoppingPad.Tests.ViewModels
{
    public class ShoppingListViewModelTests : BaseShoppingServiceTests
    {
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
        void Should_Be_Able_To_Purchase_Item()
        {
            // Arrange
            var vm = new ShoppingListViewModel(_shoppingService);
            var item = new Item("some bought item");
            vm.Add(item);

            // Act
            vm.Purchase(item);

            // Assert
            Assert.DoesNotContain<Item>(vm.Items, x => x == item);
            Assert.DoesNotContain<Item>(_shoppingService.Items, x => x == item);
            Assert.DoesNotContain<Item>(_shoppingService.BoughtItems, x => x == item);
        }

        [Fact]
        void Should_Be_Able_To_Remove_Item()
        {
            // Arrange
            var vm = new ShoppingListViewModel(_shoppingService);
            var item = new Item("some item to remove");
            vm.Add(item);

            // Act
            vm.Remove(item);

            // Assert
            Assert.DoesNotContain<Item>(vm.Items, x => x == item);
            Assert.DoesNotContain<Item>(_shoppingService.Items, x => x == item);
            Assert.DoesNotContain<Item>(_shoppingService.BoughtItems, x => x == item);
        }
    }
}
