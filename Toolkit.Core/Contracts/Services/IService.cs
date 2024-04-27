using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Toolkit.Core.Contracts.Services
{
    public interface IService
    {
        T? GetRequiredService<T>();

        T? GetRequiredService<T>(Type pageType);
    }
}
