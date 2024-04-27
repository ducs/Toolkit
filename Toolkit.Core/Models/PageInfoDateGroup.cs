using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//页组运行时间信息
namespace Toolkit.Core.Models
{
    public class PageInfoDateGroup : PageInfoDate
    {
        public bool IsExpanded { get; set; } = false;
        public ObservableCollection<PageInfoDate> Items { get; set; }

        public PageInfoDateGroup(
            string uniqueId,
            string title,
            string subtitle,
            string description,
            PageInfoDateGroup group,
            IconElement icon,
            bool isExpanded
        )
        {
            UniqueId = uniqueId;
            Title = title;
            Subtitle = subtitle;
            Description = description;
            Group = group;
            Icon = icon;
            IsExpanded = isExpanded;
            Items = new ObservableCollection<PageInfoDate>();
        }
    }
}