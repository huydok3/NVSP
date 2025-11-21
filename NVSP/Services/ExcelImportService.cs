using OfficeOpenXml;
using NVSP.DTOs;
using NVSP.Repositories;
using NVSP.Models;

namespace NVSP.Services
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IGiangVienRepository _giangVienRepository;

        public ExcelImportService(
            ITaiKhoanRepository taiKhoanRepository,
            ISinhVienRepository sinhVienRepository,
            IGiangVienRepository giangVienRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
            _sinhVienRepository = sinhVienRepository;
            _giangVienRepository = giangVienRepository;
        }

        public async Task<ImportResultDTO> ImportTaiKhoanFromExcel(Stream fileStream)
        {
            var result = new ImportResultDTO();

            using var package = new ExcelPackage(fileStream);
            var worksheet = package.Workbook.Worksheets[0];

            var rowCount = worksheet.Dimension.Rows;
            result.TotalRecords = rowCount - 1;

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    var importDto = new ImportTaiKhoanDTO
                    {
                        MaCaNhan = worksheet.Cells[row, 1].Value?.ToString(),
                        HoTen = worksheet.Cells[row, 2].Value?.ToString(),
                        LoaiTk = worksheet.Cells[row, 3].Value?.ToString(),
                        Email = worksheet.Cells[row, 4].Value?.ToString(),
                        MatKhau = worksheet.Cells[row, 5].Value?.ToString() ?? "123456"
                    };

                    if (string.IsNullOrEmpty(importDto.MaCaNhan))
                    {
                        result.Errors.Add($"Dòng {row}: Mã cá nhân không được để trống");
                        result.ErrorCount++;
                        continue;
                    }

                    if (await _taiKhoanRepository.MaCaNhanExistsAsync(importDto.MaCaNhan))
                    {
                        result.Errors.Add($"Dòng {row}: Mã cá nhân '{importDto.MaCaNhan}' đã tồn tại");
                        result.ErrorCount++;
                        continue;
                    }

                    var taiKhoan = new TaiKhoan
                    {
                        MaCaNhan = importDto.MaCaNhan,
                        HoTen = importDto.HoTen,
                        LoaiTk = importDto.LoaiTk,
                        Email = importDto.Email,
                        MatKhau = BCrypt.Net.BCrypt.HashPassword(importDto.MatKhau)
                    };

                    await _taiKhoanRepository.CreateAsync(taiKhoan);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Dòng {row}: {ex.Message}");
                    result.ErrorCount++;
                }
            }

            return result;
        }

        public async Task<ImportResultDTO> ImportSinhVienFromExcel(Stream fileStream)
        {
            var result = new ImportResultDTO();

            using var package = new ExcelPackage(fileStream);
            var worksheet = package.Workbook.Worksheets[0];

            var rowCount = worksheet.Dimension.Rows;
            result.TotalRecords = rowCount - 1;

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    var maSinhVien = worksheet.Cells[row, 1].Value?.ToString();
                    var nienKhoa = worksheet.Cells[row, 2].Value?.ToString();
                    var lop = worksheet.Cells[row, 3].Value?.ToString();
                    var nganh = worksheet.Cells[row, 4].Value?.ToString();
                    var khoa = worksheet.Cells[row, 5].Value?.ToString();
                    var hoTen = worksheet.Cells[row, 6].Value?.ToString();
                    var email = worksheet.Cells[row, 7].Value?.ToString();

                    if (string.IsNullOrEmpty(maSinhVien))
                    {
                        result.Errors.Add($"Dòng {row}: Mã sinh viên không được để trống");
                        result.ErrorCount++;
                        continue;
                    }

                    if (await _taiKhoanRepository.MaCaNhanExistsAsync(maSinhVien))
                    {
                        result.Errors.Add($"Dòng {row}: Mã sinh viên '{maSinhVien}' đã tồn tại");
                        result.ErrorCount++;
                        continue;
                    }

                    var taiKhoan = new TaiKhoan
                    {
                        MaCaNhan = maSinhVien,
                        HoTen = hoTen,
                        LoaiTk = "Sinh viên",
                        Email = email,
                        MatKhau = BCrypt.Net.BCrypt.HashPassword("123456")
                    };

                    await _taiKhoanRepository.CreateAsync(taiKhoan);

                    var sinhVien = new SinhVien
                    {
                        MaSinhVien = maSinhVien,
                        NienKhoa = nienKhoa,
                        Lop = lop,
                        Nganh = nganh,
                        Khoa = khoa
                    };

                    await _sinhVienRepository.CreateAsync(sinhVien);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Dòng {row}: {ex.Message}");
                    result.ErrorCount++;
                }
            }

            return result;
        }

        public async Task<ImportResultDTO> ImportGiangVienFromExcel(Stream fileStream)
        {
            var result = new ImportResultDTO();

            using var package = new ExcelPackage(fileStream);
            var worksheet = package.Workbook.Worksheets[0];

            var rowCount = worksheet.Dimension.Rows;
            result.TotalRecords = rowCount - 1;

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    var maGiangVien = worksheet.Cells[row, 1].Value?.ToString();
                    var khoa = worksheet.Cells[row, 2].Value?.ToString();
                    var hoTen = worksheet.Cells[row, 3].Value?.ToString();
                    var email = worksheet.Cells[row, 4].Value?.ToString();

                    if (string.IsNullOrEmpty(maGiangVien))
                    {
                        result.Errors.Add($"Dòng {row}: Mã giảng viên không được để trống");
                        result.ErrorCount++;
                        continue;
                    }

                    if (await _taiKhoanRepository.MaCaNhanExistsAsync(maGiangVien))
                    {
                        result.Errors.Add($"Dòng {row}: Mã giảng viên '{maGiangVien}' đã tồn tại");
                        result.ErrorCount++;
                        continue;
                    }

                    var taiKhoan = new TaiKhoan
                    {
                        MaCaNhan = maGiangVien,
                        HoTen = hoTen,
                        LoaiTk = "Giảng viên",
                        Email = email,
                        MatKhau = BCrypt.Net.BCrypt.HashPassword("123456")
                    };

                    await _taiKhoanRepository.CreateAsync(taiKhoan);

                    var giangVien = new GiangVien
                    {
                        MaGiangVien = maGiangVien,
                        Khoa = khoa
                    };

                    await _giangVienRepository.CreateAsync(giangVien);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Dòng {row}: {ex.Message}");
                    result.ErrorCount++;
                }
            }

            return result;
        }
    }
}