namespace ELDOKKAN.Repositories;
public interface IRepository<TEntity>
where TEntity : class
{
    TEntity GetById(int id);
    IEnumerable<TEntity> GetAll();
    TEntity Add(TEntity entity);
    void Update(TEntity entity);
    TEntity Delete(int id);
    int SaveChanges();
}