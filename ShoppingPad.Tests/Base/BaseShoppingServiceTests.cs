using ShoppingPad.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPad.Tests.Base
{
    public abstract class BaseShoppingServiceTests : IDisposable
    {
        protected ShoppingService _shoppingService;
        protected readonly string _dbPath;

        public BaseShoppingServiceTests()
        {
            _dbPath = Guid.NewGuid().ToString();
            _shoppingService = new ShoppingService(_dbPath);
        }

        public void Dispose()
        {
            File.Delete(_dbPath);
        }
    }
}
