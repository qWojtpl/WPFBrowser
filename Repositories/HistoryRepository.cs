using WPFBrowser.Data;
using WPFBrowser.Models;

namespace WPFBrowser.Repositories;

public class HistoryRepository : IRepository<HistoryElement>
{

    private readonly AppDbContext _context;
    
    public HistoryRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    
    
    public void Create(HistoryElement entity)
    {
        _context.History.Add(entity);
        _context.SaveChanges();
    }

    public void Update(HistoryElement entity)
    {
        _context.History.Update(entity);
        _context.SaveChanges();
    }

    public HistoryElement? Get(int id)
    {
        return _context.History.Find(id);
    }

    public IEnumerable<HistoryElement> GetAll()
    {
        return _context.History.ToList();
    }

    public void Delete(HistoryElement entity)
    {
        _context.History.Remove(entity);
        _context.SaveChanges();
    }
    
}