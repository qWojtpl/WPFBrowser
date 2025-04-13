using System.Collections.ObjectModel;
using WPFBrowser.Models;
using WPFBrowser.Repositories;

namespace WPFBrowser.Services;

public class HistoryService
{

    private readonly IRepository<HistoryElement> _historyRepository;
    public ObservableCollection<HistoryElement> History { get; private set; } = new();
    
    public HistoryService(IRepository<HistoryElement> historyRepository)
    {
        _historyRepository = historyRepository;
        History = new ObservableCollection<HistoryElement>(_historyRepository.GetAll().OrderByDescending(n => n.Id));
    }

    public void AddHistoryRecord(string uri)
    {
        HistoryElement element = new HistoryElement
        {
            Uri = uri,
            Date = DateTime.Now
        };
        History.Insert(0, element);
        _historyRepository.Create(element);
    }
    
}