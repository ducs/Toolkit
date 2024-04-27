using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using Toolkit.Resources;

namespace Toolkit.Infrastructure.Controls;

//[TemplatePart(Name = nameof(PART_Path), Type = typeof(Path))]
public class PackIcon : Control
{
    //private Path? PART_Path { get; set; }
    //使用图标示例
    private static readonly Lazy<IDictionary<PackIconKind, string>> _dataIndex
        = new Lazy<IDictionary<PackIconKind, string>>(PackIconDataFactory.Create);

    static PackIcon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIcon), new FrameworkPropertyMetadata(typeof(PackIcon)));
    }

    public static readonly DependencyProperty KindProperty
        = DependencyProperty.Register(nameof(Kind), typeof(PackIconKind), typeof(PackIcon), new PropertyMetadata(default(PackIconKind), KindPropertyChangedCallback));

    private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        => ((PackIcon)dependencyObject).UpdateData();

    /// <summary>
    /// Gets or sets the icon to display.
    /// </summary>
    public PackIconKind Kind
    {
        get => (PackIconKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    private static readonly DependencyPropertyKey DataPropertyKey
        = DependencyProperty.RegisterReadOnly(nameof(Data), typeof(string), typeof(PackIcon), new PropertyMetadata(""));

    // ReSharper disable once StaticMemberInGenericType
    public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

    /// <summary>
    /// Gets the icon path data for the current <see cref="Kind"/>.
    /// </summary>
    [TypeConverter(typeof(GeometryConverter))]
    public string? Data
    {
        get => (string?)GetValue(DataProperty);
        private set => SetValue(DataPropertyKey, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        UpdateData();
    }

    private void UpdateData()
    {
        string? data = null;
        //使用图标示例
        //_dataIndex.Value?.TryGetValue(Kind, out data);
        //Data = data;

        //使用自定义图标库
        Data = MaterialIconDataProvider.GetData(Kind);
    }
}
