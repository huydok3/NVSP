using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("hoat_dong_tham_du")]
    public class HoatDongThamDu
    {
        [Key]
        [Column("id_hd_tham_du")]
        public int IdHdThamDu { get; set; }

        [Column("ten_hd")]
        [MaxLength(255)]
        public string TenHd { get; set; }

        [Column("id_hd")]
        public int IdHd { get; set; }

        public HoatDongThi HoatDongThi { get; set; }
        public ICollection<DangKyThamDu> DangKyThamDus { get; set; }
    }
}