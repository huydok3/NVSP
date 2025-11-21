namespace NVSP.DTOs
{
    public class SinhVienDTO
    {
        public string MaSinhVien { get; set; }
        public string HoTen { get; set; }
        public string NienKhoa { get; set; }
        public string Lop { get; set; }
        public string Nganh { get; set; }
        public string Khoa { get; set; }
        public string Email { get; set; }
    }

    public class CreateSinhVienDTO
    {
        public string MaSinhVien { get; set; }
        public string HoTen { get; set; }
        public string NienKhoa { get; set; }
        public string Lop { get; set; }
        public string Nganh { get; set; }
        public string Khoa { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
    }
}