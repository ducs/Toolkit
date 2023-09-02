using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Toolkit.Core.Contracts.Dto;
using Toolkit.Core.Contracts.Models;
using Toolkit.Core.Contracts.Services;
using Toolkit.Core.Contracts.Services.Crypt;
using Toolkit.Models;
using Toolkit.Services;
using Toolkit.Services.Contracts;
using Toolkit.ViewModels;
using Toolkit.Views.Windows;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Markup;
using WToolkit.Services;
using MessageBox = Wpf.Ui.Controls.MessageBox;

#nullable disable

namespace Toolkit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static IConfigurationBuilder ConfigurationBuilder { get; private set; }
        protected static string[] _args;

        private static IOptionsSnapshot<IAppConfigModel> _options;

        public static IOptionsSnapshot<IAppConfigModel> Options
        {
            get => _options;
        }

        //IOptionsSnapshot<AppConfig> _options;
        // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host;

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T? GetRequiredService<T>() 
            where T : class
        {
            Debug.Assert((_host.Services.GetRequiredService(typeof(T)) as T) != null);
            return _host.Services.GetRequiredService(typeof(T)) as T;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host!.StartAsync();

            //var a = App.GetService<IOptionsSnapshot<IAppConfigModel>>();

            //_options = App.GetService<IOptionsSnapshot<IAppConfigModel>>();

            if (Options.Value.Theme != ThemeType.Unknown
                && Options.Value.Theme != Wpf.Ui.Appearance.Theme.GetAppTheme())
            {
                //Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
                //List<ResourceDictionary>
                /*var themedict = (from dict in Current.Resources.MergedDictionaries
                              where dict is ThemesDictionary
                            select dict).FirstOrDefault<ResourceDictionary>();*/
                ResourceDictionary themedict = Current.Resources.MergedDictionaries.Where(
                dict => { return (dict is ThemesDictionary); }
                ).FirstOrDefault();

                if (themedict is ThemesDictionary themesDictionary)
                {
                    themesDictionary.Theme = Options.Value.Theme;//Wpf.Ui.Appearance.ThemeType.Dark;
                    Theme.Apply(Options.Value.Theme);// Wpf.Ui.Appearance.ThemeType.Dark);
                }
            }

            //程序启动参数
            var AppArgs = ConfigurationBuilder.AddCommandLine(e.Args).Build();

            string username = AppArgs["username"];
            string password = AppArgs["password"];

            if (!string.IsNullOrEmpty(username))
            {
                //取得密码
                //string cryptCode = await GetService<IGetCryptCodeService>().GetCryptCodeAsync("GlobalCrypt");
                string cryptCode = await GetRequiredService<IGetCryptCodeService>().GetCryptCodeAsync("GlobalCrypt");

                //解密密码
                string DeCryptPassword = GetRequiredService<ICryptService>().Decrypt(password, cryptCode);
                //登陆验证
                if (!(await GetRequiredService<ILoginService>().LoginAsync(username, DeCryptPassword)))
                {
                    Current.Shutdown();
                };
            }

            var startUp = _host.Services.GetRequiredService<MainWindow>();

            startUp.Show();

            base.OnStartup(e);
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

        static App()
        {
            _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration
            (
            //c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location));
            (hostingContext, configuration) =>
            {
                configuration.Sources.Clear();
                

                IHostEnvironment env = hostingContext.HostingEnvironment;

                //configuration.SetBasePath(AppContext.BaseDirectory);
                configuration
                    .SetBasePath(AppContext.BaseDirectory)//hostingContext.HostingEnvironment.ContentRootPath//Directory.GetCurrentDirectory()
                    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"modules.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
                // .AddCommandLine(_args);
                ConfigurationBuilder = configuration;
            })
            .ConfigureServices((hostingContext, services) =>
            {
                // App Host
                services.AddHostedService<ApplicationHostService>();

                // Page resolver service
                //services.AddSingleton<IPageService, PageService>();

                // Theme manipulation
                //services.AddSingleton<IThemeService, ThemeService>();
                // TaskBar manipulation
                //services.AddSingleton<ITaskBarService, TaskBarService>();
                // Service containing navigation, same as INavigationWindow... but without window
                //services.AddSingleton<INavigationService, NavigationService>();

                services.AddSingleton<IConfigService, JsonConfigService>();

                // Main window container with navigation
                services.AddSingleton<IWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<WindowsProviderService>();

                // Views and ViewModels
                services.AddScoped<Views.Pages.DashboardPage>();
                services.AddScoped<ViewModels.DashboardViewModel>();
                services.AddScoped<Views.Pages.DataPage>();
                services.AddScoped<ViewModels.DataViewModel>();
                services.AddScoped<Views.Pages.SettingsPage>();
                services.AddScoped<ViewModels.SettingsViewModel>();

                services.AddScoped<ILoginService, LoginService>();
                services.AddScoped<ICryptService, CryptService>();
                services.AddScoped<IGetCryptCodeService, GetJsonCryptCodeService>();

                //model  每次请求都是新的实例
                services.AddTransient<INavigationViewDto, NavigationViewModel>();

                //全局外置配置接口
                //services.AddSingleton<IAppConfigModel, AppConfig>();
                // services.AddScoped(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>));

                //services.AddScoped<IAppConfigModel, AppConfig>();

                services.AddScoped(typeof(IOptionsSnapshot<IAppConfigModel>), typeof(OptionsManager<AppConfig>));

                // Configuration
                services.Configure<AppConfig>(hostingContext.Configuration.GetSection(nameof(AppConfig)));
            })
            .ConfigureLogging(logging =>
            {
                //logging.AddConsole();
            }
            ).Build();
        }

        /// <summary>
        /// 初始化一个<see cref="App"/>类型的新实例
        /// 全局异常
        /// </summary>
        public App()
        {
            //注册全局事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            DispatcherUnhandledException += App_DispatcherUnhandledException;

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            /*
                        IConfigService jsonConfigService = GetService<IConfigService>();

                        var config = GetService<IConfiguration>();

                        var a = jsonConfigService.GetValue("AppSettings1:Theme12");

            */
            //Type iAppConfigModel = App.GetService<IAppConfigModel>().GetType();
            _options = App.GetRequiredService<IOptionsSnapshot<IAppConfigModel>>();

            Debug.Assert(_options.Value != null &&
                _options.Value.Theme != ApplicationTheme.Unknown);

            Modularity.ConfigurationModuleCatalog ConfigurationModuleCatalog =
                new Modularity.ConfigurationModuleCatalog();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
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

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
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

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs args)
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
            MessageBox messageBox = new MessageBox();
            messageBox.Show(msg, ex.ToString());
            //_logger.Error(msg, ex);
        }
    }
}