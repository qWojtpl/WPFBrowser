using WPFBrowser.Services;

namespace WPFBrowser.ViewModels;

public class TabPageViewModel
{

    public TabsService TabsService { get; private set; }
    
    public TabPageViewModel()
    {
        TabsService = App.TabsService;
    }
    
}