using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Models;
using Xunit;

namespace ShoppingPad.Tests.Models
{
    public class BougthItemTests
    {
        [Fact]
        public void Should_Init_Item_With_Count_1()
        {
            // Arrange
            var title = "some bought item";

            // Act
            var boughtItem = new BoughtItem(title);

            // Assert
            Assert.Equal(1, boughtItem.BoughtCount);
        }
    }
}
