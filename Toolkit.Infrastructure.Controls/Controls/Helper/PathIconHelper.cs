using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Toolkit.Infrastructure.Controls.Helper
{
    public static class PathIconHelper
    {
        public static readonly DependencyProperty KindProperty =
            DependencyProperty.RegisterAttached(
                "Kind",
                typeof(double),
                typeof(PathIcon),
                new FrameworkPropertyMetadata(18.0)
            );
        public static readonly DependencyProperty KindDBSizeProperty =
            DependencyProperty.RegisterAttached(
                "KindDB",
                typeof(double),
                typeof(PathIcon),
                new FrameworkPropertyMetadata(18.0)
            );
    }
}
