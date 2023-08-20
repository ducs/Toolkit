using System.Reflection;
using Toolkit.Core.Contracts.Models;

namespace Toolkit.Core.InfoAttribute
{
#nullable disable

    public class ViewInfoAttribute : InfoAttribute
    {
        protected string _displayNamme;
        protected string _icon;
        protected Assembly _assembly;
        protected bool _isAvailable;

        public string DisplayName
        {
            get => _displayNamme;
            set { _displayNamme = value; }
        }

        public string Icon
        {
            get => _icon;
            set { _icon = value; }
        }

        //public Assembly Assembly
        //{
        //    get => _assembly;
        //    set { _assembly = value; }
        //}

        public bool IsAvailable
        {
            get => _isAvailable;
            set { _isAvailable = value; }
        }
    }
}