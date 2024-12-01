using iNKORE.UI.WPF.Modern;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Toolkit.Core.Helper;

namespace Toolkit.Core.Services
{
    public static class Ioc
    {
        public static bool IsInitialize { get; private set; }

        public static IHost Host { get; private set; }

        public static void Initialize(Action<IServiceCollection> configureDelegate)
        {
            if (!IsInitialize)
            {

                Host = Microsoft.Extensions.Hosting.Host.
                    CreateDefaultBuilder().
                    ConfigureServices(configureDelegate).
                    Build();

                IsInitialize = true;
            }
        }

        public static T GetService<T>() where T : class
        {
            if (Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }

            return service;
        }



        
    }
}
