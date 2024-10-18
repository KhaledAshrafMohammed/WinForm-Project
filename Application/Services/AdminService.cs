namespace ELDOKKAN.Application.Services;
public class AdminService : IAdminService
{
    private readonly IAdminRepository repository;
    private readonly IMapper mapper;
    public AdminService(IAdminRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    public CreateAdminDTO AddAdmin(CreateAdminDTO createAdminDTO)
    {
        Admin admin = mapper.Map<Admin>(createAdminDTO);
        if (admin == null)
            return null!;
        repository.Add(admin);
        repository.SaveChanges();
        return createAdminDTO;
    }

    public bool DeleteAdmin(int adminId)
    {
        repository.Delete(adminId);
        return repository.SaveChanges() == 1;
    }

    public GetAllAdminDTO GetAdminById(int adminId)
    {
        Admin admin = repository.GetById(adminId);
        GetAllAdminDTO getAllAdminDTO = mapper.Map<GetAllAdminDTO>(admin);
        return getAllAdminDTO;
    }

    public List<GetAllAdminDTO> GetAllAdmins()
    {
        return repository.GetAll().Select(ad => mapper.Map<GetAllAdminDTO>(ad)).ToList();
    }
    public bool UpdateAdmin(int adminId, GetAllAdminDTO updateAdminDTO)
    {
        Admin admin = repository.GetById(adminId);
        if(admin == null)
            return false;
        admin.Name = updateAdminDTO.Name;
        admin.Password = updateAdminDTO.Password;
         return repository.SaveChanges() == 1;
    }

   
}