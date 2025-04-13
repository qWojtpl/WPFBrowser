using System.Collections.ObjectModel;
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
        CurrentTab = Tabs.First(n => n.IsSelected);
    }

    public void AddTab(string uri)
    {
        Tab tab = new Tab();
        tab.Id = Tabs.Count() + 1;
        SetCurrentTab(tab);
        LoadCurrentTabPage(uri, true);
        _tabsRepository.Create(tab);
        Tabs.Add(tab);
    }

    public void SetCurrentTab(Tab tab)
    {
        if (CurrentTab != null)
        {
            CurrentTab.IsSelected = false;
        }
        tab.IsSelected = true;
        CurrentTab = tab;
    }

    public void OpenTab(int id)
    {
        Tab tab = Tabs.Where(n => n.Id == id)!.First();
        SetCurrentTab(tab);
        LoadCurrentTabPage(tab.Uri.ToString());
    }

    public void LoadCurrentTabPage(string strUri, bool skipUpdate = false)
    {
        Uri uri = UriValidator.ValidateUri(strUri);
        if (uri == null)
        {
            return;
        }
        CurrentTab.Uri = uri;
        CurrentTab.SmallHistory.Add(strUri);
        CurrentTab.SmallHistoryPointer = CurrentTab.SmallHistory.Count;
        if (!skipUpdate)
        {
            _tabsRepository.Update(CurrentTab);
        }
    }

    public void PreviousPage()
    {
        CurrentTab.SmallHistoryPointer--;
        CurrentTab.Uri = new Uri(CurrentTab.SmallHistory[CurrentTab.SmallHistoryPointer]);
        _tabsRepository.Update(CurrentTab);
    }
    
}