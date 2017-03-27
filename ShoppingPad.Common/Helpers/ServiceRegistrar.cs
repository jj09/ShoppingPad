using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPad.Common.Interfaces;
using ShoppingPad.Common.Services;
using SQLite;
using Autofac;
using ShoppingPad.Common.ViewModels;

namespace ShoppingPad.Common.Helpers
{
    // installation workaround: Install-package System.Runtime.InteropServices.RuntimeInformation first (https://github.com/dotnet/corefx/issues/10445)
    public static class ServiceRegistrar
    {
        public static string DbFileName = "ShoppingPad_v_1_1";

        public static IContainer Container { get; set; }

        public static void Initialize(string path)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new ShoppingService(new SQLiteConnection(path))).As<IShoppingService>();
            builder.RegisterType<ShoppingListViewModel>();
            builder.RegisterType<PastPurchasesViewModel>();

            Container = builder.Build();
        }
    }
}
