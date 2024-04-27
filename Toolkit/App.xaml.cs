using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Toolkit.Core.Contracts.Models;
using Toolkit.Core.Contracts.Services.Crypt;
using Toolkit.Core.Contracts.Services;
using Toolkit.Views.Windows;
using System.Windows.Navigation;
using Toolkit.Models;
using iNKORE.UI.WPF.Modern;
using Toolkit.Helper;
using Toolkit.Services;

namespace Toolkit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            App app = new Toolkit.App();
            //app.InitializeComponent();
            app.Run();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();

            base.OnExit(e);
        }

        private void CurrentDomain_UnhandledException(
            object sender,
            UnhandledExceptionEventArgs args
        )
        {
            const string msg = "主线程异常";
            try
            {
                if (args.ExceptionObject is Exception && Dispatcher != null)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Exception ex = (Exception)args.ExceptionObject;
                        HandleException(msg, ex);
                    });
                }
            }
            catch (Exception ex)
            {
                HandleException(msg, ex);
            }
        }

        private void App_DispatcherUnhandledException(
            object sender,
            DispatcherUnhandledExceptionEventArgs args
        )
        {
            const string msg = "子线程异常";
            try
            {
                HandleException(msg, args.Exception);
                args.Handled = true;
            }
            catch (Exception ex)
            {
                HandleException(msg, ex);
            }
        }

        private void TaskScheduler_UnobservedTaskException(
            object? sender,
            UnobservedTaskExceptionEventArgs args
        )
        {
            const string msg = "异步异常";
            try
            {
                HandleException(msg, args.Exception);
                args.SetObserved();
            }
            catch (Exception ex)
            {
                HandleException(msg, ex);
            }
        }

        private void HandleException(string msg, Exception ex)
        {
            //MessageBox messageBox = new MessageBox()
            //messageBox.Show(msg, ex.ToString());
            //_logger.Error(msg, ex);
            Debug.WriteLine(msg, ex);
        }
    }
}
