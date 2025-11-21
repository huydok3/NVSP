namespace NVSP.DTOs
{
    public class LoginDTO
    {
        public string MaCaNhan { get; set; }
        public string MatKhau { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string HoTen { get; set; }
        public string LoaiTk { get; set; }
        public string MaCaNhan { get; set; }
    }
}