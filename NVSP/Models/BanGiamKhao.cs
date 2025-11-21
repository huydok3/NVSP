using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("ban_giam_khao")]
    public class BanGiamKhao
    {
        [Column("id_hd")]
        public int IdHd { get; set; }

        [Column("ma_gv")]
        [MaxLength(50)]
        public string MaGv { get; set; }

        public HoatDongThi HoatDongThi { get; set; }
        public GiangVien GiangVien { get; set; }
    }
}