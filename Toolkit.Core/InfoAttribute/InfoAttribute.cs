using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Models;
using Toolkit.Core.Models;

#nullable disable

namespace Toolkit.Core.InfoAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InfoAttribute : Attribute, IInfo
    {
        protected string _uniqueId;
        protected string _title;
        protected string _subtitle;
        protected string _description;
        protected PageInfoDateGroup _group;

        public string UniqueId
        {
            get => _uniqueId;
            init { _uniqueId = value; }
        }

        public string Title
        {
            get => _title;
            set { _title = value; }
        }

        public string Subtitle
        {
            get => _subtitle;
            set { _subtitle = value; }
        }

        public string Description
        {
            get => _description;
            set { _description = value; }
        }

        public PageInfoDateGroup Group
        {
            get => _group;
            set { _group = value; }
        }
    }
}