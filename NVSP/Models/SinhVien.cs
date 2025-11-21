using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("sinh_vien")]
    public class SinhVien
    {
        [Key]
        [Column("ma_sinh_vien")]
        [MaxLength(50)]
        public string MaSinhVien { get; set; }

        [Column("nien_khoa")]
        [MaxLength(10)]
        public string NienKhoa { get; set; }

        [Column("lop")]
        [MaxLength(10)]
        public string Lop { get; set; }

        [Column("nganh")]
        [MaxLength(255)]
        public string Nganh { get; set; }

        [Column("khoa")]
        [MaxLength(255)]
        public string Khoa { get; set; }

        public TaiKhoan TaiKhoan { get; set; }
        public ICollection<CanBoLop> CanBoLops { get; set; }
        public ICollection<DangKyCaNhan> DangKyCaNhans { get; set; }
        public ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; }
        public ICollection<DangKyThamDu> DangKyThamDus { get; set; }
        public ICollection<DiemRenLuyen> DiemRenLuyens { get; set; }
    }
}