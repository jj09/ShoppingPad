using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Interfaces;
using ShoppingPad.Common.Services;
using SQLite;

namespace ShoppingPad.Common.Helpers
{
    
    public sealed class ServiceRegistrar
    {
        // singleton implementation from Jon Skeet: http://csharpindepth.com/Articles/General/Singleton.aspx
        //private static readonly Lazy<IShoppingService> _shoppingService = 
        //    new Lazy<IShoppingService>(() => new ShoppingService());

        //public static IShoppingService ShoppingService => _shoppingService.Value;        

        private static IShoppingService _shoppingService;

        public static IShoppingService ShoppingService(SQLiteConnection sqliteConnection=null)
        {
            if (_shoppingService == null)
            {
                _shoppingService = new ShoppingService(sqliteConnection);
            }

            return _shoppingService;
        }

        private ServiceRegistrar()
        {
        }
    }
}
