using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("thong_tin_gcn")]
    public class ThongTinGCN
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("loai_chung_nhan")]
        [MaxLength(20)]
        public string LoaiChungNhan { get; set; }

        [Column("doi_tuong")]
        [MaxLength(255)]
        public string DoiTuong { get; set; }

        [Column("giai_thuong")]
        [MaxLength(50)]
        public string GiaiThuong { get; set; }

        [Column("id_hd")]
        public int IdHd { get; set; }

        [Column("trang_thai")]
        public bool TrangThai { get; set; }

        [Column("ma_btc")]
        public int MaBtc { get; set; }

        public HoatDongThi HoatDongThi { get; set; }
        public ICollection<LogGCN> LogGCNs { get; set; }
        public ICollection<LogTraCuu> LogTraCuus { get; set; }
    }
}