using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;

#nullable disable

namespace Toolkit.Core.Interface.Mvvm.ViewModels;

public class ViewModelBase : ObservableObject
{

}

public class MainPageViewModelBase : ViewModelBase
{
    public string NavHeader { get; set; }

    public string IconKey { get; set; }

    public bool ShowsInFooter { get; set; }
}

public class PageBaseViewModel : ViewModelBase
{
    public MainPageViewModelBase Parent { get; set; }

    public string Header { get; set; }

    public string Description { get; set; }

    public string IconResourceKey { get; set; }

    public string PageKey { get; set; }

    public string[] SearchKeywords { get; set; }

    public RelayCommand InvokeCommand { get; }
}