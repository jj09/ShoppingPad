using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Models;
using ShoppingPad.Common.ViewModels;
using Xunit;

namespace ShoppingPad.Tests.ViewModels
{
    public class PastPurchasesViewModelTests
    {
        [Fact]
        void Should_Be_Able_To_Add_Item()
        {
            // Arrange
            var vm = new PastPurchasesViewModel();
            var boughtItem = new BoughtItem("some bought item");

            // Act
            vm.Add(boughtItem);

            // Assert
            Assert.Contains<BoughtItem>(vm.Items, x => x == boughtItem);
        }
    }
}
