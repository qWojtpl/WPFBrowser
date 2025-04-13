using WPFBrowser.Data;
using WPFBrowser.Models;

namespace WPFBrowser.Repositories;

public class TabsRepository : IRepository<Tab>
{

    private AppDbContext _context;
    
    public TabsRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void Create(Tab entity)
    {
        _context.Tabs.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Tab entity)
    {
        _context.Tabs.Update(entity);
        _context.SaveChanges();
    }

    public Tab? Get(int id)
    {
        return _context.Tabs.Find(id);
    }

    public IEnumerable<Tab> GetAll()
    {
        return _context.Tabs.ToList();
    }

    public void Delete(Tab entity)
    {
        _context.Tabs.Remove(entity);
        _context.SaveChanges();
    }
}