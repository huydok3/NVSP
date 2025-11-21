using NVSP.DTOs;

namespace NVSP.Services
{
    public interface IExcelImportService
    {
        Task<ImportResultDTO> ImportTaiKhoanFromExcel(Stream fileStream);
        Task<ImportResultDTO> ImportSinhVienFromExcel(Stream fileStream);
        Task<ImportResultDTO> ImportGiangVienFromExcel(Stream fileStream);
    }
}