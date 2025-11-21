using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("giang_vien")]
    public class GiangVien
    {
        [Key]
        [Column("ma_giang_vien")]
        [MaxLength(50)]
        public string MaGiangVien { get; set; }

        [Column("khoa")]
        [MaxLength(255)]
        public string Khoa { get; set; }

        public TaiKhoan TaiKhoan { get; set; }
        public ICollection<BcnKhoa> BcnKhoas { get; set; }
        public ICollection<BanToChuc> BanToChucs { get; set; }
        public ICollection<BanGiamKhao> BanGiamKhaos { get; set; }
        public ICollection<HoatDongHoTro> HoatDongHoTros { get; set; }
    }
}