using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Core.Contracts.Entity
{
    public class PageInfoDateItemEntity
    {
        public string UniqueId { get; init; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string GroupID { get; set; }
        public IconElement Icon { get; set; }
        public string PageType { get; set; }
    }
}