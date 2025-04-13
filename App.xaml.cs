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

    public static HistoryRepository HistoryRepository
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

    public static TabsRepository TabsRepository
    {
        get
        {
            if (_tabsRepository == null)
            {
                _tabsRepository = new TabsRepository(DbContext);
            }
            return _tabsRepository;
        }
    }
    
    private static TabsRepository? _tabsRepository;

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

    public static TabsService TabsService
    {
        get
        {
            if (_tabsService == null)
            {
                _tabsService = new TabsService(HistoryRepository, TabsRepository);
            }
            return _tabsService;
        }
    }
    
    private static TabsService _tabsService;

}
