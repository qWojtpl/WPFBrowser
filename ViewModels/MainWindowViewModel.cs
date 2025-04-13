
using System.Windows;
using System.Windows.Input;
using WPFBrowser.Commands;
using WPFBrowser.Data;
using WPFBrowser.Models;
using WPFBrowser.Services;
using WPFBrowser.Views;

namespace WPFBrowser.ViewModels;

public class MainWindowViewModel : GenericPropertyChanged
{
    public ICommand PreviousPageCommand { get; }    
    public ICommand NextPageCommand { get; }
    public ICommand LoadPageCommand { get; }
    public ICommand NewTabCommand { get; }
    public ICommand HistoryWindowCommand { get; }
    public ICommand OpenTabCommand { get; }
    public ICommand RemoveTabCommand { get; }

    public HistoryWindow HistoryWindow
    {
        get
        {
            if (_historyWindow == null)
            {
                _historyWindow = new HistoryWindow();
            } else if (PresentationSource.FromVisual(_historyWindow) == null)
            {
                _historyWindow = new HistoryWindow();
            }

            return _historyWindow;
        }
    }
    
    private HistoryWindow _historyWindow;
    private readonly HistoryService _historyService;
    public TabsService TabsService { get => _tabsService; }
    private readonly TabsService _tabsService;
    private Tab _selectedTab;

    public MainWindowViewModel()
    {
        _historyService = App.HistoryService;
        _tabsService = App.TabsService;
        PreviousPageCommand = new RelayCommand((object? p) => _tabsService.CurrentTab.SmallHistory.Count() > 1, PreviousPage);
        NextPageCommand = new RelayCommand((object? p) => _tabsService.CurrentTab.SmallHistoryPointer != _tabsService.CurrentTab.SmallHistory.Count(), NextPage);
        LoadPageCommand = new RelayCommand((object? p) => true, LoadPage);
        NewTabCommand = new RelayCommand((object? p) => true, NewTab);
        OpenTabCommand = new RelayCommand((object? p) => true, OpenTab);
        RemoveTabCommand = new RelayCommand((object? p) => _tabsService.Tabs.Count() > 1, RemoveTab);
        HistoryWindowCommand = new RelayCommand((object? p) => true, ShowHistoryWindow);
    }

    private void PreviousPage(object? p)
    {
        _tabsService.PreviousPage();
    }

    private void NextPage(object? p)
    {
        _tabsService.PreviousPage();
    }

    private void LoadPage(object? p)
    {
        _tabsService.LoadCurrentTabPage(TabsService.CurrentUri);
    }

    private void NewTab(object? p)
    {
        _tabsService.AddTab("https://google.com");
    }

    private void OpenTab(object? p)
    {
        _tabsService.OpenTab((int) p);
    }

    private void RemoveTab(object? p)
    {
        bool openLater = _tabsService.CurrentTab.Id == (int)p;
        _tabsService.RemoveTab((int) p);
        if (openLater)
        {
            OpenTab(_tabsService.Tabs.First().Id);
        }
    }

    private void ShowHistoryWindow(object? p)
    {
        HistoryWindow.Show();
    }
    
}
