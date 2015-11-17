using ShoppingPad.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingPad.Tests.Models
{
    public class ItemTests
    {
        [Fact]
        public void Set_Title_On_Create()
        {
            // Arrange
            var title = "my item";

            // Act
            var item = new Item(title);

            // Assert
            Assert.Equal(title, item.Title);
        }
    }
}
