using System.ComponentModel.Design;

namespace Toolkit.Modularity;

public interface IModule
{
    //
    // 摘要:
    //     Used to register types with the container that will be used by your application.
    //void RegisterTypes(IContainerRegistry containerRegistry);
    void RegisterTypes(IServiceContainer container);

    //
    // 摘要:
    //     Notifies the module that it has been initialized.
    //void OnInitialized(IContainerProvider containerProvider);
    void OnInitialized(IServiceProvider serviceProvider);

    void onUnload(IServiceContainer container);
}