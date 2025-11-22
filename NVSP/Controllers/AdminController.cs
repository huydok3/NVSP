using Microsoft.AspNetCore.Mvc;
using NVSP.DTOs;
using NVSP.Services;

namespace NVSP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IBcnKhoaService _bcnKhoaService;
        private readonly IBanToChucService _banToChucService;
        private readonly ICanBoLopService _canBoLopService;

        public AdminController(
            IBcnKhoaService bcnKhoaService,
            IBanToChucService banToChucService,
            ICanBoLopService canBoLopService)
        {
            _bcnKhoaService = bcnKhoaService;
            _banToChucService = banToChucService;
            _canBoLopService = canBoLopService;
        }

        [HttpGet("bcn-khoa")]
        public async Task<ActionResult<IEnumerable<BcnKhoaDTO>>> GetAllBcnKhoa()
        {
            var bcnKhoas = await _bcnKhoaService.GetAllAsync();
            return Ok(bcnKhoas);
        }

        [HttpGet("bcn-khoa/{maGiangVien}")]
        public async Task<ActionResult<BcnKhoaDTO>> GetBcnKhoa(string maGiangVien)
        {
            var bcnKhoa = await _bcnKhoaService.GetByMaGiangVienAsync(maGiangVien);
            if (bcnKhoa == null)
            {
                return NotFound();
            }
            return Ok(bcnKhoa);
        }

        [HttpPost("bcn-khoa")]
        public async Task<ActionResult<BcnKhoaDTO>> CreateBcnKhoa(CreateBcnKhoaDTO createBcnKhoaDTO)
        {
            try
            {
                var bcnKhoa = await _bcnKhoaService.CreateAsync(createBcnKhoaDTO);
                return CreatedAtAction(nameof(GetBcnKhoa), new { maGiangVien = bcnKhoa.MaGiangVien }, bcnKhoa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("bcn-khoa/{maGiangVien}")]
        public async Task<ActionResult<BcnKhoaDTO>> UpdateBcnKhoa(string maGiangVien, CreateBcnKhoaDTO updateBcnKhoaDTO)
        {
            try
            {
                var bcnKhoa = await _bcnKhoaService.UpdateAsync(maGiangVien, updateBcnKhoaDTO);
                return Ok(bcnKhoa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("bcn-khoa/{maGiangVien}")]
        public async Task<ActionResult> DeleteBcnKhoa(string maGiangVien)
        {
            var success = await _bcnKhoaService.DeleteAsync(maGiangVien);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("ban-to-chuc")]
        public async Task<ActionResult<IEnumerable<BanToChucDTO>>> GetAllBanToChuc()
        {
            var banToChucs = await _banToChucService.GetAllAsync();
            return Ok(banToChucs);
        }

        [HttpPost("ban-to-chuc")]
        public async Task<ActionResult<BanToChucDTO>> CreateBanToChuc(CreateBanToChucDTO createBanToChucDTO)
        {
            try
            {
                var banToChuc = await _banToChucService.CreateAsync(createBanToChucDTO);
                return CreatedAtAction(nameof(GetAllBanToChuc), banToChuc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("ban-to-chuc/{maGiangVien}")]
        public async Task<ActionResult> DeleteBanToChuc(string maGiangVien)
        {
            var success = await _banToChucService.DeleteAsync(maGiangVien);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("can-bo-lop")]
        public async Task<ActionResult<IEnumerable<CanBoLopDTO>>> GetAllCanBoLop()
        {
            var canBoLops = await _canBoLopService.GetAllAsync();
            return Ok(canBoLops);
        }

        [HttpPost("can-bo-lop")]
        public async Task<ActionResult<CanBoLopDTO>> CreateCanBoLop(CreateCanBoLopDTO createCanBoLopDTO)
        {
            try
            {
                var canBoLop = await _canBoLopService.CreateAsync(createCanBoLopDTO);
                return CreatedAtAction(nameof(GetAllCanBoLop), canBoLop);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("can-bo-lop/{maSinhVien}")]
        public async Task<ActionResult> DeleteCanBoLop(string maSinhVien)
        {
            var success = await _canBoLopService.DeleteAsync(maSinhVien);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}