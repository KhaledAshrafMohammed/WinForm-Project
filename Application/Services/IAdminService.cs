using ELDOKKAN.Application.DTOs.Admin;

namespace ELDOKKAN.Application.Services;
public interface IAdminService
{
    GetAllAdminDTO GetAdminById(int adminId);
    List<GetAllAdminDTO> GetAllAdmins();
    CreateAdminDTO AddAdmin(CreateAdminDTO createAdminDTO);
    bool UpdateAdmin(int adminId, GetAllAdminDTO updateAdminDTO);
    bool DeleteAdmin(int adminId);
} 