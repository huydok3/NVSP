using NVSP.DTOs;
using NVSP.Repositories;

namespace NVSP.Services
{
    public class CanBoLopService : ICanBoLopService
    {
        private readonly ICanBoLopRepository _canBoLopRepository;
        private readonly ISinhVienRepository _sinhVienRepository;

        public CanBoLopService(ICanBoLopRepository canBoLopRepository, ISinhVienRepository sinhVienRepository)
        {
            _canBoLopRepository = canBoLopRepository;
            _sinhVienRepository = sinhVienRepository;
        }

        public async Task<CanBoLopDTO> GetByMaSinhVienAsync(string maSinhVien)
        {
            var canBoLop = await _canBoLopRepository.GetByMaSinhVienAsync(maSinhVien);
            return MapToDTO(canBoLop);
        }

        public async Task<IEnumerable<CanBoLopDTO>> GetAllAsync()
        {
            var canBoLops = await _canBoLopRepository.GetAllAsync();
            return canBoLops.Select(MapToDTO);
        }

        public async Task<IEnumerable<CanBoLopDTO>> GetByLopAsync(string lop)
        {
            var canBoLops = await _canBoLopRepository.GetByLopAsync(lop);
            return canBoLops.Select(MapToDTO);
        }

        public async Task<IEnumerable<CanBoLopDTO>> GetCanBoLopDươngNhiemAsync()
        {
            var canBoLops = await _canBoLopRepository.GetCanBoLopDươngNhiemAsync();
            return canBoLops.Select(MapToDTO);
        }

        public async Task<CanBoLopDTO> CreateAsync(CreateCanBoLopDTO createCanBoLopDTO)
        {
            var sinhVien = await _sinhVienRepository.GetByMaSinhVienAsync(createCanBoLopDTO.MaSinhVien);
            if (sinhVien == null)
            {
                throw new Exception("Sinh viên không tồn tại");
            }

            if (await _canBoLopRepository.ExistsAsync(createCanBoLopDTO.MaSinhVien))
            {
                throw new Exception("Sinh viên đã là Cán bộ lớp");
            }

            // Kiểm tra lớp đã có CBL đương nhiệm chưa
            var currentCbl = await _canBoLopRepository.GetCanBoLopDươngNhiemAsync();
            var cblInSameClass = currentCbl.FirstOrDefault(c => c.SinhVien?.Lop == sinhVien.Lop);
            if (cblInSameClass != null)
            {
                throw new Exception($"Lớp {sinhVien.Lop} đã có Cán bộ lớp đương nhiệm");
            }

            var canBoLop = new Models.CanBoLop
            {
                MaSinhVien = createCanBoLopDTO.MaSinhVien,
                BatDauNk = createCanBoLopDTO.BatDauNk,
                KetThucNk = createCanBoLopDTO.KetThucNk,
                TrangThai = true
            };

            await _canBoLopRepository.CreateAsync(canBoLop);
            return await GetByMaSinhVienAsync(createCanBoLopDTO.MaSinhVien);
        }

        public async Task<CanBoLopDTO> UpdateAsync(string maSinhVien, CreateCanBoLopDTO updateCanBoLopDTO)
        {
            var canBoLop = await _canBoLopRepository.GetByMaSinhVienAsync(maSinhVien);
            if (canBoLop == null)
            {
                throw new Exception("Cán bộ lớp không tồn tại");
            }

            canBoLop.BatDauNk = updateCanBoLopDTO.BatDauNk;
            canBoLop.KetThucNk = updateCanBoLopDTO.KetThucNk;

            await _canBoLopRepository.UpdateAsync(canBoLop);
            return await GetByMaSinhVienAsync(maSinhVien);
        }

        public async Task<bool> DeleteAsync(string maSinhVien)
        {
            await _canBoLopRepository.DeleteAsync(maSinhVien);
            return true;
        }

        private CanBoLopDTO MapToDTO(Models.CanBoLop canBoLop)
        {
            if (canBoLop == null) return null;

            return new CanBoLopDTO
            {
                MaSinhVien = canBoLop.MaSinhVien,
                HoTen = canBoLop.SinhVien?.TaiKhoan?.HoTen,
                Lop = canBoLop.SinhVien?.Lop,
                BatDauNk = canBoLop.BatDauNk,
                KetThucNk = canBoLop.KetThucNk,
                TrangThai = canBoLop.TrangThai
            };
        }
    }
}