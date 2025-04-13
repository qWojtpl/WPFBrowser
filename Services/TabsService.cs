using System.Collections.ObjectModel;
using WPFBrowser.Data;
using WPFBrowser.Models;
using WPFBrowser.Repositories;

namespace WPFBrowser.Services;

public class TabsService
{

    private readonly TabsRepository _tabsRepository;
    public ObservableCollection<Tab> Tabs { get; private set; } = new();
    
    public TabsService(TabsRepository tabsRepository)
    {
        _tabsRepository = tabsRepository;
        Tabs = new ObservableCollection<Tab>(_tabsRepository.GetAll());
    }

    public void AddTab(string uri)
    {
        Tab tab = new Tab()
        {
            Uri = uri
        };
        _tabsRepository.Create(tab);
        Tabs.Add(tab);
    }
    
}