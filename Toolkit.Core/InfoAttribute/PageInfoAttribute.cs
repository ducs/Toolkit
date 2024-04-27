using iNKORE.UI.WPF.Modern.Controls;
using System.Reflection;
using Toolkit.Core.Contracts.Models;

namespace Toolkit.Core.InfoAttribute
{
#nullable disable

    public class PageInfoAttribute : InfoAttribute
    {
        protected string _displayNamme;
        protected IconElement _icon;
        protected bool _isAvailable;

        public string DisplayName
        {
            get => _displayNamme;
            set { _displayNamme = value; }
        }

        public IconElement Icon
        {
            get => _icon;
            set { _icon = value; }
        }

        public bool IsAvailable
        {
            get => _isAvailable;
            set { _isAvailable = value; }
        }
    }
}
