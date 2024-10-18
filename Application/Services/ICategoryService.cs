namespace ELDOKKAN.Application.Services;
public interface ICategoryService
{
    GetAllCategoryDTO GetCategoryById(int categoryId);
    IEnumerable<GetAllCategoryDTO> GetAllCategories();
    GetAllCategoryDTO AddCategory(CreateCategoryDTO createCategoryDto);
    bool UpdateCategory(int categoryId, GetAllCategoryDTO updateCategoryDto);
    bool DeleteCategory(int categoryId);
} 