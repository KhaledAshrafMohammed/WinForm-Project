namespace ELDOKKAN.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository repository;
    private readonly IMapper mapper;
    public CategoryService(ICategoryRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    public GetAllCategoryDTO AddCategory(CreateCategoryDTO createCategoryDto)
    {
        Category category = mapper.Map<Category>(createCategoryDto);
        if (category == null)
            return null!;
        repository.Add(category);
        repository.SaveChanges();
        return mapper.Map<GetAllCategoryDTO>(category);
    }

    public bool DeleteCategory(int categoryId)
    {
        repository.Delete(categoryId);
        return repository.SaveChanges() == 1;
    }

    public GetAllCategoryDTO GetCategoryById(int categoryId)
    {
        Category category = repository.GetById(categoryId);
        GetAllCategoryDTO getAllCategoryDto = mapper.Map<GetAllCategoryDTO>(category);
        return getAllCategoryDto;
    }

    public IEnumerable<GetAllCategoryDTO> GetAllCategories()
    {
        return repository.GetAll().Select(ad => mapper.Map<GetAllCategoryDTO>(ad));
    }
    public bool UpdateCategory(int categoryId, GetAllCategoryDTO updateCategoryDTO)
    {
        Category category = repository.GetById(categoryId);
        if(category == null)
            return false;
        category.CategoryName = updateCategoryDTO.CategoryName;
        category.Description = updateCategoryDTO.Description;
        return repository.SaveChanges() == 1;
    }
}