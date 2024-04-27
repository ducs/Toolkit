using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Core.Contracts.Models
{
    public interface IInfoExtenion : IInfo
    {
        public string Version { get; set; }
        public string Author { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly LastModifiedBy { get; set; }
    }
}