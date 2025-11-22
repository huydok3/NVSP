using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NVSP.Models;

namespace NVSP.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expireMinutes;

        public JwtService()
        {
            // Hardcode JWT configuration
            _key = "YourSuperSecretKeyThatIsAtLeast32CharactersLong!";
            _issuer = "NVSP-API";
            _audience = "NVSP-Client";
            _expireMinutes = 60;
        }

        public string GenerateToken(TaiKhoan taiKhoan)
        {
            if (taiKhoan == null)
                throw new ArgumentNullException(nameof(taiKhoan));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, taiKhoan.MaCaNhan),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("maCaNhan", taiKhoan.MaCaNhan ?? ""),
                new Claim("hoTen", taiKhoan.HoTen ?? ""),
                new Claim("loaiTk", taiKhoan.LoaiTk ?? ""),
                new Claim("email", taiKhoan.Email ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_expireMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = true,
                    ValidAudience = _audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var maCaNhan = jwtToken.Claims.First(x => x.Type == "maCaNhan").Value;

                return maCaNhan;
            }
            catch
            {
                return null;
            }
        }
    }
}