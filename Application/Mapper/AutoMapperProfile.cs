namespace ELDOKKAN.Application.Mapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateAdminDTO, Admin>().ReverseMap();
        CreateMap<GetAllAdminDTO, Admin>().ReverseMap();
        CreateMap<UpdateAdminDTO, Admin>().ReverseMap();

        CreateMap<CreateCategoryDTO, Category>().ReverseMap();
        CreateMap<GetAllCategoryDTO, Category>().ReverseMap();
        CreateMap<UpdateCategoryDTO, Category>().ReverseMap();

        CreateMap<CreateCustomerDTO, Customer>().ReverseMap();
        CreateMap<GetAllCustomerDTO, Customer>().ReverseMap();
        CreateMap<UpdateCustomerDTO, Customer>().ReverseMap();

        CreateMap<CreateProductDTO, Product>().ReverseMap();
        CreateMap<GetAllProductDTO, Product>().ReverseMap();
        CreateMap<UpdateProductDTO, Product>().ReverseMap();

        CreateMap<CreateOrderDTO, Order>().ReverseMap();
        CreateMap<GetAllOrderDTO, Order>().ReverseMap();
        CreateMap<UpdateOrderDTO, Order>().ReverseMap();

        CreateMap<CreateOrderDetailsDTO, OrderDetails>().ReverseMap();
        CreateMap<GetAllOrderDetailsDTO, OrderDetails>().ReverseMap();
        CreateMap<UpdateOrderDetailsDTO, OrderDetails>().ReverseMap();
    }
}