using Microsoft.AspNetCore.Mvc;
using NVSP.DTOs;
using NVSP.Services;

namespace NVSP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;

        public ImportController(IExcelImportService excelImportService)
        {
            _excelImportService = excelImportService;
        }

        [HttpPost("tai-khoan")]
        public async Task<ActionResult<ImportResultDTO>> ImportTaiKhoan(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File không được để trống");
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Chỉ chấp nhận file Excel (.xlsx)");
            }

            try
            {
                using var stream = file.OpenReadStream();
                var result = await _excelImportService.ImportTaiKhoanFromExcel(stream);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi import file: {ex.Message}");
            }
        }

        [HttpPost("sinh-vien")]
        public async Task<ActionResult<ImportResultDTO>> ImportSinhVien(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File không được để trống");
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Chỉ chấp nhận file Excel (.xlsx)");
            }

            try
            {
                using var stream = file.OpenReadStream();
                var result = await _excelImportService.ImportSinhVienFromExcel(stream);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi import file: {ex.Message}");
            }
        }

        [HttpPost("giang-vien")]
        public async Task<ActionResult<ImportResultDTO>> ImportGiangVien(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File không được để trống");
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Chỉ chấp nhận file Excel (.xlsx)");
            }

            try
            {
                using var stream = file.OpenReadStream();
                var result = await _excelImportService.ImportGiangVienFromExcel(stream);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi import file: {ex.Message}");
            }
        }
    }
}