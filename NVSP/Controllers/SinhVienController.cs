using Microsoft.AspNetCore.Mvc;
using NVSP.Attributes;
using NVSP.DTOs;
using NVSP.Helpers;
using NVSP.Services;

namespace NVSP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinhVienController : ControllerBase
    {
        private readonly ISinhVienService _sinhVienService;
        private readonly IAuthorizationService _authorizationService;

        public SinhVienController(ISinhVienService sinhVienService, IAuthorizationService authorizationService)
        {
            _sinhVienService = sinhVienService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien)]
        public async Task<ActionResult<IEnumerable<SinhVienDTO>>> GetAll()
        {
            var sinhViens = await _sinhVienService.GetAllAsync();
            return Ok(sinhViens);
        }

        [HttpGet("{maSinhVien}")]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien, UserRoles.SinhVien)]
        public async Task<ActionResult<SinhVienDTO>> GetByMaSinhVien(string maSinhVien)
        {
            var currentUserMaCaNhan = User.FindFirst("MaCaNhan")?.Value;

            // Sinh viên chỉ được xem thông tin của chính mình
            if (await _authorizationService.IsSinhVienAsync(currentUserMaCaNhan) && currentUserMaCaNhan != maSinhVien)
            {
                return Forbid("Sinh viên chỉ được xem thông tin của chính mình");
            }

            var sinhVien = await _sinhVienService.GetByMaSinhVienAsync(maSinhVien);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return Ok(sinhVien);
        }

        [HttpPost]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien)]
        public async Task<ActionResult<SinhVienDTO>> Create(CreateSinhVienDTO createSinhVienDTO)
        {
            var currentUserMaCaNhan = User.FindFirst("MaCaNhan")?.Value;

            if (!await _authorizationService.CanManageSinhVienAsync(currentUserMaCaNhan))
            {
                return Forbid();
            }

            try
            {
                var sinhVien = await _sinhVienService.CreateAsync(createSinhVienDTO);
                return CreatedAtAction(nameof(GetByMaSinhVien), new { maSinhVien = sinhVien.MaSinhVien }, sinhVien);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{maSinhVien}")]
        [AuthorizeRole(UserRoles.Admin, UserRoles.GiangVien)]
        public async Task<ActionResult<SinhVienDTO>> Update(string maSinhVien, UpdateSinhVienDTO updateSinhVienDTO)
        {
            var currentUserMaCaNhan = User.FindFirst("MaCaNhan")?.Value;

            if (!await _authorizationService.CanManageSinhVienAsync(currentUserMaCaNhan))
            {
                return Forbid();
            }

            try
            {
                var sinhVien = await _sinhVienService.UpdateAsync(maSinhVien, updateSinhVienDTO);
                return Ok(sinhVien);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{maSinhVien}")]
        [AuthorizeRole(UserRoles.Admin)]
        public async Task<ActionResult> Delete(string maSinhVien)
        {
            var success = await _sinhVienService.DeleteAsync(maSinhVien);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}