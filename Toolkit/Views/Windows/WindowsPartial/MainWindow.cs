using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Toolkit.Infrastructure.Common;
using Toolkit.Infrastructure.UI;
using Toolkit.Properties;

namespace Toolkit.Views.Windows;

public partial class MainWindow
{
    protected void OnLoaded(object sender, RoutedEventArgs e)
    {
        // 获取父窗口的窗口句柄。
        HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
        source.AddHook(WndProc);
    }

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

        iNKORE.UI.WPF.Modern.Helpers.DispatcherHelper.RunOnMainThread(() =>
        {
            if (this == Application.Current.MainWindow)
            {
                this.SetPlacement(Settings.Default.MainWindowPlacement);
            }
        });
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
            iNKORE.UI.WPF.Modern.Helpers.DispatcherHelper.RunOnMainThread(() =>
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
