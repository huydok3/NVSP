namespace NVSP.DTOs
{
    public class TaiKhoanDTO
    {
        public string MaCaNhan { get; set; }
        public string HoTen { get; set; }
        public string LoaiTk { get; set; }
        public string Email { get; set; }
    }

    public class CreateTaiKhoanDTO
    {
        public string MaCaNhan { get; set; }
        public string HoTen { get; set; }
        public string MatKhau { get; set; }
        public string LoaiTk { get; set; }
        public string Email { get; set; }
    }

    public class UpdateTaiKhoanDTO
    {
        public string HoTen { get; set; }
        public string Email { get; set; }
    }
}