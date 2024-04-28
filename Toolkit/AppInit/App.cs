using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

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
using System.Windows.Threading;
using Toolkit.DependencyModel;

namespace Toolkit
{
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
        /// 初始化一个<see cref="App"/>类型的新实例
        /// 全局异常
        /// </summary>
        public App()
        {
            this.Startup += App_Startup;
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



            //Debug.Assert(_options.Value != null &&
            //    _options.Value.Theme != Wpf.Ui.Appearance.ThemeType.Unknown);

            Modularity.ConfigurationModuleCatalog ConfigurationModuleCatalog =
                new Modularity.ConfigurationModuleCatalog();

            InitializeComponent();
        }

        static App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(
                    //c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location));
                    (hostingContext, configuration) =>
                    {
                        configuration.Sources.Clear();
                        IHostEnvironment env = hostingContext.HostingEnvironment;

                        configuration
                            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath) //Directory.GetCurrentDirectory()
                            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile(
                                $"appsettings.{env.EnvironmentName}.json",
                                optional: true,
                                reloadOnChange: true
                            )
                            .AddJsonFile($"modules.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();
                        // .AddCommandLine(_args);
                        ConfigurationBuilder = configuration;
                    }
                )
                .ConfigureServices(
                    (hostingContext, services) =>
                    {
                        // App Host
                        //services.AddHostedService<ApplicationHostService>();

                        // Page resolver service
                        services.AddSingleton<IPageService, TypeService>();
                        services.AddSingleton<IService, TypeService>();

                        // Theme manipulation
                        //services.AddSingleton<IThemeService, ThemeService>();

                        // Service containing navigation, same as INavigationWindow... but without window
                        //services.AddSingleton<INavigationService, NavigationService>();

                        services.AddSingleton<IConfigService, JsonConfigService>();

                        // Main window with navigation
                        /*services.AddScoped<INavigationWindow, MainWindow>();
                        services.AddScoped<ViewModels.MainWindowViewModel>();*/

                        // Views and ViewModels
                        /*services.AddScoped<Views.Pages.DashboardPage>();
                        services.AddScoped<ViewModels.DashboardViewModel>();
                        services.AddScoped<Views.Pages.DataPage>();
                        services.AddScoped<ViewModels.DataViewModel>();
                        services.AddScoped<Views.Pages.SettingsPage>();
                        services.AddScoped<ViewModels.SettingsViewModel>();*/

                        services.AddScoped<ILoginService, LoginService>();
                        services.AddScoped<ICryptService, CryptService>();
                        services.AddScoped<IGetCryptCodeService, GetJsonCryptCodeService>();

                        // All other pages and view models
                        services.AddTransientFromNamespace(
                            "Wpf.Ui.Gallery.Views",
                            ToolkitAssembly.Asssembly
                        );
                        services.AddTransientFromNamespace(
                            "Wpf.Ui.Gallery.ViewModels",
                            ToolkitAssembly.Asssembly
                        );
                        //model  每次请求都是新的实例
                        //services.AddTransient<INavigationViewDto, NavigationViewModel>();

                        //全局外置配置接口
                        //services.AddSingleton<IAppConfigModel, AppConfig>();
                        // services.AddScoped(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>));

                        //services.AddScoped<IAppConfigModel, AppConfig>();

                        services.AddScoped(
                            typeof(IOptionsSnapshot<IAppConfigModel>),
                            typeof(OptionsManager<AppConfig>)
                        );

                        // Configuration
                        services.Configure<AppConfig>(
                            hostingContext.Configuration.GetSection(nameof(AppConfig))
                        );
                    }
                )
                .ConfigureLogging(logging =>
                {
                    //logging.AddConsole();
                })
                .Build();
        }

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetRequiredService<T>()
            where T : class
        {
            Debug.Assert((_host.Services.GetService(typeof(T)) as T) != null);
            return _host.Services.GetService(typeof(T)) as T;
        }

        public static T GetRequiredService<T>(Type serviceType)
            where T : class
        {
            Debug.Assert((_host.Services.GetService(typeof(T)) as T) != null);
            return (T)_host.Services.GetService(serviceType);
        }

        public static R GetRequiredService<T, R>()
            where R : class, T
        {
            Debug.Assert((_host.Services.GetService(typeof(T)) as R) != null);
            return (R)_host.Services.GetService<T>();
        }

        /// <inheritdoc />
        public static FrameworkElement GetRequiredPage<T>()
            where T : class
        {
            Debug.Assert((FrameworkElement?)_host.Services.GetService(typeof(T)) != null);
            if (!typeof(FrameworkElement).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException("The page should be a WPF control.");

            return (FrameworkElement?)_host.Services.GetService(typeof(T));
        }

        /// <inheritdoc />
        public FrameworkElement? GetRequiredPage(Type pageType)
        {
            Debug.Assert(_host.Services.GetService(pageType) as FrameworkElement != null);
            if (!typeof(FrameworkElement).IsAssignableFrom(pageType))
                throw new InvalidOperationException("The page should be a WPF control.");

            return _host.Services.GetService(pageType) as FrameworkElement;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host!.StartAsync();

            //var a = App.GetService<IOptionsSnapshot<IAppConfigModel>>();

            //_options = App.GetService<IOptionsSnapshot<IAppConfigModel>>();

            if (Options.Value.Theme != Toolkit.Helper.ThemeHelper.RootTheme)
            {
                ThemeHelper.RootTheme = Options.Value.Theme;
            }

            //程序启动参数
            var AppArgs = ConfigurationBuilder.AddCommandLine(e.Args).Build();

            string username = AppArgs["username"];
            string password = AppArgs["password"];

            if (!string.IsNullOrEmpty(username))
            {
                //取得密码
                //string cryptCode = await GetService<IGetCryptCodeService>().GetCryptCodeAsync("GlobalCrypt");
                string cryptCode = await GetRequiredService<IGetCryptCodeService>()
                    .GetCryptCodeAsync("GlobalCrypt");

                //解密密码
                string DeCryptPassword = GetRequiredService<ICryptService>()
                    .Decrypt(password, cryptCode);
                //登陆验证
                if (
                    !(
                        await GetRequiredService<ILoginService>()
                            .LoginAsync(username, DeCryptPassword)
                    )
                )
                {
                    Current.Shutdown();
                }
                ;
            }

            CreateShell()?.Show();

            base.OnStartup(e);
        }

        private Window CreateShell()
        {
            Window StartWindows = _host.Services.GetRequiredService<MainWindow>();
            if (InitializeShell(StartWindows))
            {
                return StartWindows;
            }
            Application.Current.Shutdown();
            return null;
        }

        private bool InitializeShell(Window window)
        {
            return true;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            // _options = App.GetService<IOptionsSnapshot<IAppConfigModel>>();
        }

        public static bool IsMultiThreaded { get; } = false;

        public static TEnum GetEnum<TEnum>(string text)
            where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }
    }
}
