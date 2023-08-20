using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Models;

#nullable disable

namespace Toolkit.Core.InfoAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InfoAttribute : Attribute, IInfo
    {
        protected string _id;
        protected string _name;
        protected string _description;

        public string Id
        {
            get => _id;
            init { _id = value; }
        }

        public string Name
        {
            get => _name;
            set { _name = value; }
        }

        public string Description
        {
            get => _description;
            set { _description = value; }
        }
    }
}