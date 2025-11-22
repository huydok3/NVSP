namespace NVSP.DTOs
{
    public class BcnKhoaDTO
    {
        public string MaGiangVien { get; set; }
        public string HoTen { get; set; }
        public string Khoa { get; set; }
        public DateTime BatDauNk { get; set; }
        public DateTime KetThucNk { get; set; }
        public bool TrangThai { get; set; }
    }

    public class CreateBcnKhoaDTO
    {
        public string MaGiangVien { get; set; }
        public string Khoa { get; set; }
        public DateTime BatDauNk { get; set; }
        public DateTime KetThucNk { get; set; }
    }

    public class BanToChucDTO
    {
        public string MaGiangVien { get; set; }
        public string HoTen { get; set; }
        public DateTime BatDauNk { get; set; }
        public DateTime KetThucNk { get; set; }
        public bool TrangThai { get; set; }
    }

    public class CreateBanToChucDTO
    {
        public string MaGiangVien { get; set; }
        public DateTime BatDauNk { get; set; }
        public DateTime KetThucNk { get; set; }
    }

    public class CanBoLopDTO
    {
        public string MaSinhVien { get; set; }
        public string HoTen { get; set; }
        public string Lop { get; set; }
        public DateTime BatDauNk { get; set; }
        public DateTime KetThucNk { get; set; }
        public bool TrangThai { get; set; }
    }

    public class CreateCanBoLopDTO
    {
        public string MaSinhVien { get; set; }
        public DateTime BatDauNk { get; set; }
        public DateTime KetThucNk { get; set; }
    }
}