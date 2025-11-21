using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("su_kien")]
    public class SuKien
    {
        [Key]
        [Column("id_sk")]
        public int IdSk { get; set; }

        [Column("ten_sk")]
        [MaxLength(255)]
        public string TenSk { get; set; }

        [Column("nam_hoc")]
        public int NamHoc { get; set; }

        [Column("ma_btc")]
        [MaxLength(50)]
        public string MaBtc { get; set; }

        [Column("trang_thai")]
        public bool TrangThai { get; set; }

        public BanToChuc BanToChuc { get; set; }
        public ICollection<HoatDongThi> HoatDongThis { get; set; }
    }
}