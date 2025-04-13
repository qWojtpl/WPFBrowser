using System.Collections.ObjectModel;
using WPFBrowser.Data;
using WPFBrowser.Models;
using WPFBrowser.Repositories;
using WPFBrowser.Validators;

namespace WPFBrowser.Services;

public class TabsService
{

    private readonly HistoryRepository _historyRepository;
    private readonly TabsRepository _tabsRepository;
    public ObservableCollection<Tab> Tabs { get; private set; } = new();
    public Tab CurrentTab { get; private set; }
    
    public TabsService(HistoryRepository historyRepository, TabsRepository tabsRepository)
    {
        _historyRepository = historyRepository;
        _tabsRepository = tabsRepository;
        Tabs = new ObservableCollection<Tab>(_tabsRepository.GetAll());
        if (!Tabs.Any())
        {
            AddTab("https://google.com");
        }
        CurrentTab = Tabs.First();
    }

    public void AddTab(string uri)
    {
        Tab tab = new Tab();
        SetCurrentTab(tab);
        LoadCurrentTabPage(uri);
        _tabsRepository.Create(tab);
        Tabs.Add(tab);
    }

    public void SetCurrentTab(Tab tab)
    {
        CurrentTab = tab;
    }

    public void LoadCurrentTabPage(string strUri)
    {
        Uri uri = UriValidator.ValidateUri(strUri);
        if (uri == null)
        {
            return;
        }
        CurrentTab.Uri = uri;
        CurrentTab.SmallHistory.Add(strUri);
        CurrentTab.SmallHistoryPointer = CurrentTab.SmallHistory.Count;
        _tabsRepository.Update(CurrentTab);
    }

    public void PreviousPage()
    {
        CurrentTab.SmallHistoryPointer--;
        CurrentTab.Uri = new Uri(CurrentTab.SmallHistory[CurrentTab.SmallHistoryPointer]);
        _tabsRepository.Update(CurrentTab);
    }
    
}