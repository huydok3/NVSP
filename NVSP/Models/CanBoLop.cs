using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("can_bo_lop")]
    public class CanBoLop
    {
        [Key]
        [Column("ma_sinh_vien")]
        [MaxLength(50)]
        public string MaSinhVien { get; set; }

        [Column("bat_dau_nk")]
        public DateTime BatDauNk { get; set; }

        [Column("ket_thuc_nk")]
        public DateTime KetThucNk { get; set; }

        [Column("trang_thai")]
        public bool TrangThai { get; set; }

        public SinhVien SinhVien { get; set; }
    }
}