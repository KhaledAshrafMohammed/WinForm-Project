namespace ELDOKKAN.Repositories;
public interface IProductRepository : IRepository<Product>
{
    IQueryable<Product> SearchByName(string productName);
}