using System.Windows;
using WPFBrowser.Data;
using WPFBrowser.Models;
using WPFBrowser.Repositories;
using WPFBrowser.Services;

namespace WPFBrowser;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public static AppDbContext DbContext
    {
        get
        {
            if(_context == null)
            {
                _context = new AppDbContext();
                _context.Database.EnsureCreated();
            }
            return _context;
        }
    }

    private static AppDbContext? _context;

    private static HistoryRepository HistoryRepository
    {
        get
        {
            if (_historyRepository == null)
            {
                _historyRepository = new HistoryRepository(DbContext);
            }
            return _historyRepository;
        }
    }
    
    private static HistoryRepository? _historyRepository;

    public static HistoryService HistoryService
    {
        get
        {
            if (_historyService == null)
            {
                _historyService = new HistoryService(HistoryRepository);
            }
            return _historyService;
            
        }
    }
    
    private static HistoryService _historyService;

}
