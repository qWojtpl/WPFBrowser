using System.Collections.ObjectModel;
using WPFBrowser.Data;
using WPFBrowser.Models;
using WPFBrowser.Repositories;
using WPFBrowser.Validators;

namespace WPFBrowser.Services;

public class TabsService : GenericPropertyChanged
{

    private readonly HistoryService _historyService;
    private readonly TabsRepository _tabsRepository;
    public ObservableCollection<Tab> Tabs { get; private set; } = new();
    public Tab CurrentTab { get; private set; }

    public string CurrentUri
    {
        get => _currentUri;
        set
        {
            if (_currentUri != value)
            {
                _currentUri = value;
                OnPropertyChanged();
            }
        }
    }
    
    private string _currentUri;
    
    public TabsService(HistoryService historyService, TabsRepository tabsRepository)
    {
        _historyService = historyService;
        _tabsRepository = tabsRepository;
        List<Tab> tabs = _tabsRepository.GetAll().ToList();
        foreach (Tab tab in tabs)
        {
            tab.TabsService = this;
        }
        Tabs = new ObservableCollection<Tab>(tabs);
        if (!Tabs.Any())
        {
            AddTab("https://google.com");
        }
        CurrentTab = Tabs.First(n => n.IsSelected);
        CurrentUri = CurrentTab.Uri.ToString();
    }

    public void AddTab(string uri)
    {
        Tab tab = new Tab();
        if (Tabs.Count() == 0)
        {
            tab.Id = 1;
        }
        else
        {
            tab.Id = Tabs.Max(n => n.Id) + 1;
        }
        tab.TabsService = this;
        SetCurrentTab(tab);
        _tabsRepository.Create(tab);
        LoadCurrentTabPage(uri);
        Tabs.Add(tab);
    }

    public void SetCurrentTab(Tab tab)
    {
        foreach (Tab t in Tabs)
        {
            t.IsSelected = false;
        }
        tab.IsSelected = true;
        CurrentTab = tab;
        if (tab.Uri != null)
        {
            CurrentUri = tab.Uri.ToString();
        }
    }

    public void OpenTab(int id)
    {
        Tab tab = Tabs.Where(n => n.Id == id)!.First();
        SetCurrentTab(tab);
        LoadCurrentTabPage(tab.Uri.ToString());
    }

    public void RemoveTab(int id)
    {
        Tab tab = Tabs.Where(n => n.Id == id)!.First();
        _tabsRepository.Delete(tab);
        Tabs.Remove(tab);
    }

    public void SaveTab(Tab tab)
    {
        _tabsRepository.Update(tab);
        _historyService.AddHistoryRecord(tab.Uri.ToString());
    }

    public void LoadCurrentTabPage(string strUri)
    {
        Uri uri = UriValidator.ValidateUri(strUri);
        if (uri == null)
        {
            return;
        }
        CurrentTab.Uri = uri;
        CurrentTab.SmallHistory.Add(uri.ToString());
        CurrentTab.SmallHistoryPointer = CurrentTab.SmallHistory.Count - 1;
        _tabsRepository.Update(CurrentTab);
    }

    public void PreviousPage()
    {
        CurrentTab.SmallHistoryPointer--;
        if (CurrentTab.SmallHistoryPointer < 0)
        {
            CurrentTab.SmallHistoryPointer = 0;
            return;
        }
        CurrentTab.Uri = new Uri(CurrentTab.SmallHistory[CurrentTab.SmallHistoryPointer]);
        _tabsRepository.Update(CurrentTab);
    }

    public void NextPage()
    {
        CurrentTab.SmallHistoryPointer++;
        if (CurrentTab.SmallHistoryPointer >= CurrentTab.SmallHistory.Count)
        {
            CurrentTab.SmallHistoryPointer = CurrentTab.SmallHistory.Count - 1;
            return;
        }
        CurrentTab.Uri = new Uri(CurrentTab.SmallHistory[CurrentTab.SmallHistoryPointer]);
        _tabsRepository.Update(CurrentTab);
    }
    
}