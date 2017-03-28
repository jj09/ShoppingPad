using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.Services;
using Xunit;
using SQLite;
using System.IO;

namespace ShoppingPad.Tests.Services
{
    public class ShoppingServiceTests : IDisposable
    {
        private ShoppingService _shoppingService;
        private string _dbPath;

        public ShoppingServiceTests()
        {
            _dbPath = Guid.NewGuid().ToString();
            _shoppingService = new ShoppingService(_dbPath);
        }

        public void Dispose()
        {            
            File.Delete(_dbPath);
        }

        [Fact]
        public void Add_Item_To_ShoppingList()
        {
            // Arrange
            var item = new Item("item");

            // Act
            _shoppingService.AddItem(item);

            // Assert
            Assert.Contains<Item>(_shoppingService.Items, x => x == item);
        }

        [Fact]
        public void Remove_Item_From_ShoppingList()
        {
            // Arrange
            var item = new Item("item");
            _shoppingService.AddItem(item);

            // Act
            _shoppingService.RemoveItem(item);

            // Assert
            Assert.DoesNotContain<Item>(_shoppingService.Items, x => x == item);
            Assert.Contains<BoughtItem>(_shoppingService.BoughtItems, x => x.Title == item.Title);
            Assert.Contains<BoughtItem>(_shoppingService.BoughtItems, x => x.Title == item.Title && x.BoughtCount == 1);
        }

        [Fact]
        public void Remove_Item_From_ShoppingList_That_Is_Present_On_BoughtList()
        {
            // Arrange
            var itemTitle = "item";
            var item = new Item(itemTitle);
            _shoppingService.AddItem(item);
            _shoppingService.AddToBoughtItems(item);

            // Act
            _shoppingService.RemoveItem(item);

            // Assert
            Assert.DoesNotContain<Item>(_shoppingService.Items, x => x == item);
            Assert.Contains<BoughtItem>(_shoppingService.BoughtItems, x => x.Title == item.Title && x.BoughtCount == 2);
        }

        [Fact]
        public void BoughtItems_Are_Always_Sorted_By_BoughtCount()
        {
            // Arrange
            var item1 = "item 1";
            var item2 = "item 2";
            var item3 = "item 3";

            // Act
            _shoppingService.AddToBoughtItems(new Item(item1));
            _shoppingService.AddToBoughtItems(new Item(item1));
            _shoppingService.AddToBoughtItems(new Item(item2));
            _shoppingService.AddToBoughtItems(new Item(item3));
            _shoppingService.AddToBoughtItems(new Item(item3));
            _shoppingService.AddToBoughtItems(new Item(item3));

            // Assert
            var expected = _shoppingService.BoughtItems.OrderByDescending(x => x.BoughtCount);
            Assert.Equal(expected, _shoppingService.BoughtItems);
        }

        [Fact]
        public void TryAdd_Adds_Item_Only_If_Not_Present_On_List()
        {
            // Arrange
            var item1 = "item 1";
            _shoppingService.AddItem(new Item(item1));

            // Act
            _shoppingService.TryAddItemToShoppingList(new Item(item1));

            // Assert
            Assert.Equal(1, _shoppingService.Items.Count);
        }

        [Fact]
        public void TryAdd_Adds_Item_Like_AddItem_When_Item_Not_Present()
        {
            // Arrange
            var item1 = "item 1";

            // Act
            _shoppingService.TryAddItemToShoppingList(new Item(item1));

            // Assert
            Assert.Equal(1, _shoppingService.Items.Count);
        }

        [Fact]
        public void PastPurchases_RestoringFromDb_Sorted()
        {
            // Arrange
            var item1 = "item 1";
            var item2 = "item 2";
            _shoppingService.AddToBoughtItems(new Item(item1));
            _shoppingService.AddToBoughtItems(new Item(item2));
            _shoppingService.AddToBoughtItems(new Item(item2));

            // Act
            _shoppingService = new ShoppingService(_dbPath);

            // Assert
            Assert.Equal(item2, _shoppingService.BoughtItems.FirstOrDefault()?.Title);
            Assert.Equal(2, _shoppingService.BoughtItems.FirstOrDefault()?.BoughtCount);
            Assert.Equal(item1, _shoppingService.BoughtItems.LastOrDefault()?.Title);
            Assert.Equal(1, _shoppingService.BoughtItems.LastOrDefault()?.BoughtCount);
        }
    }
}
