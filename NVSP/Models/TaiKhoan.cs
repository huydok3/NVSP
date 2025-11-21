using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("tai_khoan")]
    public class TaiKhoan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("ma_ca_nhan")]
        [MaxLength(50)]
        public string MaCaNhan { get; set; }

        [Column("ho_ten")]
        [MaxLength(50)]
        public string HoTen { get; set; }

        [Column("mat_khau")]
        [MaxLength(255)]
        public string MatKhau { get; set; }

        [Column("loai_tk")]
        [MaxLength(20)]
        public string LoaiTk { get; set; }

        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; }

        public SinhVien SinhVien { get; set; }
        public GiangVien GiangVien { get; set; }
    }
}