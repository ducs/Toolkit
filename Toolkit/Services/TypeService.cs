using System;
using System.Windows;
using Toolkit.Core.Contracts.Services;

namespace Toolkit.Services
{
    /// <summary>
    /// Service that provides pages for navigation.
    /// </summary>
    public class TypeService : IPageService, IService
    {
        /// <summary>
        /// Service which provides the instances of pages.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Creates new instance and attaches the <see cref="IServiceProvider"/>.
        /// </summary>
        public TypeService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public T GetRequiredPage<T>()
            where T : class
        {
            if (!typeof(FrameworkElement).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException("The page should be a WPF control.");

            return (T?)_serviceProvider.GetService(typeof(T));
        }

        /// <inheritdoc />
        public FrameworkElement? GetRequiredPage(Type pageType)
        {
            if (!typeof(FrameworkElement).IsAssignableFrom(pageType))
                throw new InvalidOperationException("The page should be a WPF control.");

            return _serviceProvider.GetService(pageType) as FrameworkElement;
        }

        public T? GetRequiredService<T>(Type serviceType)
        {
            return (T?)_serviceProvider.GetService(serviceType);
        }

        public T GetRequiredService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
