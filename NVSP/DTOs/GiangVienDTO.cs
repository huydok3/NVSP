namespace NVSP.DTOs
{
    public class GiangVienDTO
    {
        public string MaGiangVien { get; set; }
        public string HoTen { get; set; }
        public string Khoa { get; set; }
        public string Email { get; set; }
    }

    public class CreateGiangVienDTO
    {
        public string MaGiangVien { get; set; }
        public string HoTen { get; set; }
        public string Khoa { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
    }
}