
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
    public ICommand LoadPageCommand { get; }
    public ICommand HistoryWindowCommand { get; }

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
        PreviousPageCommand = new RelayCommand((object? p) => _historyService.History.Any(), PreviousPage);
        LoadPageCommand = new RelayCommand((object? p) => true, LoadPage);
        HistoryWindowCommand = new RelayCommand((object? p) => true, ShowHistoryWindow);
    }

    private void PreviousPage(object? p)
    {
        CurrentUri = new Uri(_historyService.History.Last().Uri);
    }

    private void LoadPage(object? p)
    {
        Uri? uri = UriValidator.ValidateUri(CurrentTextBoxUri);
        if (uri == null)
        {
            return;
        }
        CurrentUri = uri;
        _historyService.AddHistoryRecord(uri.ToString());
    }

    private void ShowHistoryWindow(object? p)
    {
        HistoryWindow.Show();
    }
    
}
