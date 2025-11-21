using NVSP.Models;

namespace NVSP.Data.Seeds
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultDataAsync(ApplicationDbContext context)
        {
            if (!context.TaiKhoans.Any())
            {
                var adminAccount = new TaiKhoan
                {
                    MaCaNhan = "admin",
                    HoTen = "Administrator",
                    MatKhau = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                    LoaiTk = "Admin",
                    Email = "admin@nvsp.com"
                };

                context.TaiKhoans.Add(adminAccount);
                await context.SaveChangesAsync();
            }
        }
    }
}