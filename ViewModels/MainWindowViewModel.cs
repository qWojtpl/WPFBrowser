
using System.Windows;
using System.Windows.Input;
using WPFBrowser.Commands;
using WPFBrowser.Data;
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

    public MainWindowViewModel()
    {
        _historyService = App.HistoryService;
        PreviousPageCommand = new RelayCommand((object? p) => _currentPageId >= 2, PreviousPage);
        NextPageCommand = new RelayCommand((object? p) => _currentPageId == 15, NextPage);
        LoadPageCommand = new RelayCommand((object? p) => true, LoadPage);
        HistoryWindowCommand = new RelayCommand((object? p) => true, ShowHistoryWindow);
    }

    private void PreviousPage(object? p)
    {
        int t = _currentPageId - 1;
        CurrentUri = new Uri(_historyService.History.Where(n => n.Id == _currentPageId - 1).First().Uri);
        _currentPageId = t; 
    }

    private void NextPage(object? p)
    {
        CurrentUri = new Uri(_historyService.History.Where(n => n.Id == _currentPageId + 1).First().Uri);
    }

    private void LoadPage(object? p)
    {
        Uri? uri = UriValidator.ValidateUri(CurrentTextBoxUri);
        if (uri == null)
        {
            return;
        }
        CurrentUri = uri;
    }

    private void ShowHistoryWindow(object? p)
    {
        HistoryWindow.Show();
    }
    
}
