using NVSP.Models;

namespace NVSP.Services
{
    public interface IJwtService
    {
        string GenerateToken(TaiKhoan taiKhoan);
        string ValidateToken(string token);
    }
}