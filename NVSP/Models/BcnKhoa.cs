using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("bcn_khoa")]
    public class BcnKhoa
    {
        [Key]
        [Column("ma_giang_vien")]
        [MaxLength(50)]
        public string MaGiangVien { get; set; }

        [Column("khoa")]
        [MaxLength(255)]
        public string Khoa { get; set; }

        [Column("bat_dau_nk")]
        public DateTime BatDauNk { get; set; }

        [Column("ket_thuc_nk")]
        public DateTime KetThucNk { get; set; }

        [Column("trang_thai")]
        public bool TrangThai { get; set; }

        public GiangVien GiangVien { get; set; }
    }
}