namespace WPFBrowser.Repositories;

public interface IRepository<T>
{
    
    public void Create(T entity);
    public void Update(T entity);
    public T? Get(int id);
    public IEnumerable<T> GetAll();
    public void Delete(T entity);
    
}