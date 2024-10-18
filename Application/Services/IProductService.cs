using ELDOKKAN.Application.DTOs.Customer;

namespace ELDOKKAN.Application.Services;
public interface IProductService
{
    GetAllProductDTO GetProductById(int ProductId);
    IQueryable<GetAllProductDTO> GetProductByName(string ProductName);
    IEnumerable<GetAllProductDTO> GetAllProducts();
    GetAllProductDTO AddProduct(CreateProductDTO createProductDto);
    bool UpdateProduct(int ProductId, GetAllProductDTO getAllProductDTO);
    bool DeleteProduct(int ProductId);
    public List<GetAllProductDTO> GetPagination(int v, int pageNum);

 
} 