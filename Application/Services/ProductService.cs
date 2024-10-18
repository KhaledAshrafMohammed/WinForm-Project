using ELDOKKAN.Application.DTOs.Customer;
using ELDOKKAN.Repositories;

namespace ELDOKKAN.Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository repository;
    private readonly IMapper mapper;
    public ProductService(IProductRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    public GetAllProductDTO AddProduct(CreateProductDTO createProductDto)
    {
        Product product = mapper.Map<Product>(createProductDto);
        if(product == null)
            return null!;
        repository.Add(product);
        repository.SaveChanges();
        return mapper.Map<GetAllProductDTO>(product);
    }

    public bool DeleteProduct(int productId)
    {
        repository.Delete(productId);
        return repository.SaveChanges() == 1;
    }

    public GetAllProductDTO GetProductById(int productId)
    {
        Product product = repository.GetById(productId);
        GetAllProductDTO getAllProductDto = mapper.Map<GetAllProductDTO>(product);
        return getAllProductDto;
    }

    public IQueryable<GetAllProductDTO> GetProductByName(string ProductName)
    {
        if(ProductName == null)
            return null!;
            
        return repository.SearchByName(ProductName).Select(p => mapper.Map<GetAllProductDTO>(p));
    }

    public IEnumerable<GetAllProductDTO> GetAllProducts()
    {
        return repository.GetAll().Select(ad => mapper.Map<GetAllProductDTO>(ad));
    }
    public bool UpdateProduct(int productId, GetAllProductDTO getAllProductDTO)
    {
        Product product = repository.GetById(productId);

        if(product == null)
            return false;

        product.Name = getAllProductDTO.Name;
        product.UnitPrice = getAllProductDTO.UnitPrice;
        product.UnitsInStock = getAllProductDTO.UnitsInStock; 
        
        return repository.SaveChanges() == 1;
    }

          public List<GetAllProductDTO> GetPagination(int count, int pageNum)
    {
        var productList = repository.GetAll().Where(p => p.UnitsInStock > 0).Skip(count * (pageNum - 1)).Take(count)
                .Select(p => new GetAllProductDTO { Name = p.Name, UnitPrice = p.UnitPrice, CategoryID = p.CategoryID }).ToList();
        return productList;
    }

   
}