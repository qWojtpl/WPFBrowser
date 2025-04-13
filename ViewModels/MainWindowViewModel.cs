
using System.Windows;
using System.Windows.Input;
using WPFBrowser.Commands;
using WPFBrowser.Data;
using WPFBrowser.Models;
using WPFBrowser.Services;
using WPFBrowser.Validators;
using WPFBrowser.Views;

namespace WPFBrowser.ViewModels;

public class MainWindowViewModel : GenericPropertyChanged
{
    
    private static readonly string _startPage = "https://google.com";
    public ICommand PreviousPageCommand { get; }    
    public ICommand NextPageCommand { get; }

    public ICommand LoadPageCommand { get; }
    public ICommand HistoryWindowCommand { get; }

    private int _currentPageId;

    public string CurrentTextBoxUri
    {
        get => _currentTextBoxUri;
        set
        {
            if (_currentTextBoxUri != value)
            {
                _currentTextBoxUri = value;
                OnPropertyChanged();
            }
        }
    }

    private string _currentTextBoxUri = _startPage;

    public Uri CurrentUri
    {
        get => _currentUri;
        set
        {
            if (_currentUri != value)
            {
                _currentUri = value;
                OnPropertyChanged();
                CurrentTextBoxUri = _currentUri.ToString();
                _historyService.AddHistoryRecord(_currentUri.ToString());
                _currentPageId = _historyService.History.First().Id;
            }
        }
    }

    private Uri _currentUri = new Uri(_startPage);

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
        _tabsService.LoadCurrentTabPage(CurrentTextBoxUri);
    }

    private void ShowHistoryWindow(object? p)
    {
        HistoryWindow.Show();
    }
    
}
