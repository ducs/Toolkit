using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Toolkit.Infrastructure.Common;
using Toolkit.Infrastructure.UI;
using Toolkit.Properties;
using Toolkit.Services.Contracts;
using Toolkit.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Toolkit.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IWindow
    {

        /*  public MainWindow(ViewModels.MainWindowViewModel viewModel,
         INavigationService navigationService,
         IServiceProvider serviceProvider,
         ISnackbarService snackbarService,
        IContentDialogService contentDialogService)
         {
             Wpf.Ui.Appearance.Watcher.Watch(this);

             ViewModel = viewModel;
             DataContext = this;

             InitializeComponent();
             snackbarService.SetSnackbarPresenter(SnackbarPresenter);
             navigationService.SetNavigationControl(NavigationView);
             contentDialogService.SetContentPresenter(RootContentDialog);

             NavigationView.SetServiceProvider(serviceProvider);
             NavigationView.Loaded += (_, _) => NavigationView.Navigate(typeof(DashboardPage));

         } */

        public ViewModels.MainWindowViewModel ViewModel
        {
            get;
        }
        /*
        #region INavigationWindow methods

        public Frame GetFrame()
            => RootFrame;

        public INavigation GetNavigation()
            => RootNavigation;

        public bool Navigate(Type pageType)
            => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService)
            => RootNavigation.PageService = pageService;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();

        #endregion INavigationWindow methods
        */
        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Make sure that closing this window will begin the process of closing the application.
            Application.Current.Shutdown();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            DispatcherHelper.RunOnMainThread(() =>
            {
                if (this == Application.Current.MainWindow)
                {
                    this.SetPlacement(Settings.Default.MainWindowPlacement);
                }
            });

            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == NativeMethods.WM_SHOWME)
            {
                if (!this.IsVisible)
                {
                    this.Show();
                }

                if (this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Normal;
                }
                Activate();
                this.Topmost = true;
                this.Topmost = false;
                this.Focus();
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// 保存最后窗口关闭的位置
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!e.Cancel)
            {
                DispatcherHelper.RunOnMainThread(() =>
                {
                    if (this == Application.Current.MainWindow)
                    {
                        Settings.Default.MainWindowPlacement = this.GetPlacement();
                        Settings.Default.Save();
                    }
                });
            }
        }
    }
}