using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

//页面基础,使用依赖注入上下文
namespace Toolkit.Core.Contracts.Models
{
    public abstract class PageBase<T> : Page
    {
        public PageBase(IServiceProvider serviceProvider)
        {
            Debug.Assert(
                serviceProvider.GetService(typeof(T)) is not null
                    && typeof(T) is INotifyPropertyChanged
            );
            this.Content = serviceProvider.GetService(typeof(T));
        }
    }
}
