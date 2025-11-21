using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("diem_ren_luyen")]
    public class DiemRenLuyen
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("ma_sv")]
        [MaxLength(50)]
        public string MaSv { get; set; }

        [Column("diem")]
        public int Diem { get; set; }

        [Column("xep_loai")]
        [MaxLength(20)]
        public string XepLoai { get; set; }

        [Column("ky_hoc")]
        [MaxLength(10)]
        public string KyHoc { get; set; }

        [Column("nam_hoc")]
        [MaxLength(10)]
        public string NamHoc { get; set; }

        public SinhVien SinhVien { get; set; }
    }
}