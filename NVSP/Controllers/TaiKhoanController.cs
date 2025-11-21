using Microsoft.AspNetCore.Mvc;
using NVSP.DTOs;
using NVSP.Services;

namespace NVSP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanService;

        public TaiKhoanController(ITaiKhoanService taiKhoanService)
        {
            _taiKhoanService = taiKhoanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoanDTO>>> GetAll()
        {
            var taiKhoans = await _taiKhoanService.GetAllAsync();
            return Ok(taiKhoans);
        }

        [HttpGet("{maCaNhan}")]
        public async Task<ActionResult<TaiKhoanDTO>> GetByMaCaNhan(string maCaNhan)
        {
            var taiKhoan = await _taiKhoanService.GetByMaCaNhanAsync(maCaNhan);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return Ok(taiKhoan);
        }

        [HttpPost]
        public async Task<ActionResult<TaiKhoanDTO>> Create(CreateTaiKhoanDTO createTaiKhoanDTO)
        {
            try
            {
                var taiKhoan = await _taiKhoanService.CreateAsync(createTaiKhoanDTO);
                return CreatedAtAction(nameof(GetByMaCaNhan), new { maCaNhan = taiKhoan.MaCaNhan }, taiKhoan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{maCaNhan}")]
        public async Task<ActionResult<TaiKhoanDTO>> Update(string maCaNhan, UpdateTaiKhoanDTO updateTaiKhoanDTO)
        {
            try
            {
                var taiKhoan = await _taiKhoanService.UpdateAsync(maCaNhan, updateTaiKhoanDTO);
                return Ok(taiKhoan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _taiKhoanService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}