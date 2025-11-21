namespace NVSP.DTOs
{
    public class ImportTaiKhoanDTO
    {
        public string MaCaNhan { get; set; }
        public string HoTen { get; set; }
        public string LoaiTk { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
    }

    public class ImportResultDTO
    {
        public int TotalRecords { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}