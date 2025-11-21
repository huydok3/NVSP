using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("hoat_dong_ho_tro")]
    public class HoatDongHoTro
    {
        [Key]
        [Column("id_hd_ho_tro")]
        public int IdHdHoTro { get; set; }

        [Column("ten_hd")]
        [MaxLength(255)]
        public string TenHd { get; set; }

        [Column("loai_ho_tro")]
        [MaxLength(50)]
        public string LoaiHoTro { get; set; }

        [Column("tg_bat_dau")]
        public DateTime TgBatDau { get; set; }

        [Column("tg_ket_thuc")]
        public DateTime TgKetThuc { get; set; }

        [Column("dia_diem")]
        [MaxLength(50)]
        public string DiaDiem { get; set; }

        [Column("id_hd")]
        public int IdHd { get; set; }

        [Column("ma_gv")]
        [MaxLength(50)]
        public string MaGv { get; set; }

        public HoatDongThi HoatDongThi { get; set; }
        public GiangVien GiangVien { get; set; }
    }
}