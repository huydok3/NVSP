using Microsoft.AspNetCore.Mvc;
using NVSP.DTOs;
using NVSP.Services;

namespace NVSP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);

            if (result == null)
            {
                return Unauthorized("Mã cá nhân hoặc mật khẩu không đúng");
            }

            return Ok(result);
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var success = await _authService.ChangePasswordAsync(
                changePasswordDTO.MaCaNhan,
                changePasswordDTO.CurrentPassword,
                changePasswordDTO.NewPassword
            );

            if (!success)
            {
                return BadRequest("Mật khẩu hiện tại không đúng");
            }

            return Ok("Đổi mật khẩu thành công");
        }
    }

    public class ChangePasswordDTO
    {
        public string MaCaNhan { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}