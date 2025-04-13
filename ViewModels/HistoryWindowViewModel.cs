using System.Collections.ObjectModel;
using WPFBrowser.Models;
using WPFBrowser.Services;

namespace WPFBrowser.ViewModels;

public class HistoryWindowViewModel
{

    private readonly HistoryService _historyService;
    public ObservableCollection<HistoryElement> History { get => _historyService.History; }
    
    public HistoryWindowViewModel()
    {
        _historyService = App.HistoryService;
    }
    
}