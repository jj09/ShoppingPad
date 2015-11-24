using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Interfaces;
using ShoppingPad.Common.Services;

namespace ShoppingPad.Windows10.Helpers
{
    // singleton implementation from Jon Skeet: http://csharpindepth.com/Articles/General/Singleton.aspx
    public sealed class ServiceRegistrar
    {
        private static readonly Lazy<IShoppingService> _shoppingService = 
            new Lazy<IShoppingService>(() => new ShoppingService());

        public static IShoppingService ShoppingService => _shoppingService.Value;

        private ServiceRegistrar()
        {
        }
    }
}
