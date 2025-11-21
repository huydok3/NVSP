using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("cham_diem")]
    public class ChamDiem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_hd_thi")]
        public int IdHdThi { get; set; }

        [Column("loai_bai_thi")]
        [MaxLength(20)]
        public string LoaiBaiThi { get; set; }

        [Column("id_dang_ky")]
        public int IdDangKy { get; set; }

        [Column("diem")]
        public decimal Diem { get; set; }

        [Column("nhan_xet")]
        public string NhanXet { get; set; }

        [Column("ma_bgk")]
        [MaxLength(50)]
        public string MaBgk { get; set; }

        public HoatDongThi HoatDongThi { get; set; }
        public KetQua KetQua { get; set; }
    }
}