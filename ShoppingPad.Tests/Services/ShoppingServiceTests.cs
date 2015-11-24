using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.Services;
using Xunit;

namespace ShoppingPad.Tests.Services
{
    public class ShoppingServiceTests
    {
        [Fact]
        public void Add_Item_To_ShoppingList()
        {
            // Arrange
            var item = new Item("item");
            var shoppingService = new ShoppingService();

            // Act
            shoppingService.AddItem(item);

            // Assert
            Assert.Contains<Item>(shoppingService.Items, x => x == item);
        }

        [Fact]
        public void Remove_Item_From_ShoppingList()
        {
            // Arrange
            var item = new Item("item");
            var shoppingService = new ShoppingService();
            shoppingService.AddItem(item);

            // Act
            shoppingService.RemoveItem(item);

            // Assert
            Assert.DoesNotContain<Item>(shoppingService.Items, x => x == item);
            Assert.Contains<BoughtItem>(shoppingService.BoughtItems, x => x.Title == item.Title);
            Assert.Contains<BoughtItem>(shoppingService.BoughtItems, x => x.Title == item.Title && x.BoughtCount == 1);
        }

        [Fact]
        public void Remove_Item_From_ShoppingList_That_Is_Present_On_BoughtList()
        {
            // Arrange
            var itemTitle = "item";
            var item = new Item(itemTitle);
            var shoppingService = new ShoppingService();
            shoppingService.AddItem(item);
            shoppingService.BoughtItems.Add(new BoughtItem(itemTitle));

            // Act
            shoppingService.RemoveItem(item);

            // Assert
            Assert.DoesNotContain<Item>(shoppingService.Items, x => x == item);
            Assert.Contains<BoughtItem>(shoppingService.BoughtItems, x => x.Title == item.Title && x.BoughtCount == 2);
        }

        [Fact]
        public void BoughtItems_Are_Always_Sorted_By_BoughtCount()
        {
            // Arrange
            var item1 = "item 1";
            var item2 = "item 2";
            var item3 = "item 3";
            var shoppingService = new ShoppingService();

            // Act
            shoppingService.AddToBoughtItems(new Item(item1));
            shoppingService.AddToBoughtItems(new Item(item1));
            shoppingService.AddToBoughtItems(new Item(item2));
            shoppingService.AddToBoughtItems(new Item(item3));
            shoppingService.AddToBoughtItems(new Item(item3));
            shoppingService.AddToBoughtItems(new Item(item3));

            // Assert
            var expected = shoppingService.BoughtItems.OrderByDescending(x => x.BoughtCount);
            Assert.Equal(expected, shoppingService.BoughtItems);
        }

        [Fact]
        public void TryAdd_Adds_Item_Only_If_Not_Present_On_List()
        {
            // Arrange
            var item1 = "item 1";
            var shoppingService = new ShoppingService();
            shoppingService.AddItem(new Item(item1));

            // Act
            shoppingService.TryAddItem(new Item(item1));

            // Assert
            Assert.Equal(1, shoppingService.Items.Count);
        }

        [Fact]
        public void TryAdd_Adds_Item_Like_AddItem_When_Item_Not_Present()
        {
            // Arrange
            var item1 = "item 1";
            var shoppingService = new ShoppingService();

            // Act
            shoppingService.TryAddItem(new Item(item1));

            // Assert
            Assert.Equal(1, shoppingService.Items.Count);
        }

    }
}
