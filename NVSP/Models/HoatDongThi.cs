using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("hoat_dong_thi")]
    public class HoatDongThi
    {
        [Key]
        [Column("id_hd")]
        public int IdHd { get; set; }

        [Column("ten_hd")]
        [MaxLength(255)]
        public string TenHd { get; set; }

        [Column("id_rubric")]
        public int IdRubric { get; set; }

        [Column("hinh_thuc")]
        [MaxLength(20)]
        public string HinhThuc { get; set; }

        [Column("tg_bat_dau")]
        public DateTime TgBatDau { get; set; }

        [Column("tg_ket_thuc")]
        public DateTime TgKetThuc { get; set; }

        [Column("dia_diem")]
        [MaxLength(50)]
        public string DiaDiem { get; set; }

        [Column("id_sk")]
        public int IdSk { get; set; }

        public DsRubrics DsRubrics { get; set; }
        public SuKien SuKien { get; set; }
        public ICollection<BanGiamKhao> BanGiamKhaos { get; set; }
        public ICollection<HoatDongThamDu> HoatDongThamDus { get; set; }
        public ICollection<HoatDongHoTro> HoatDongHoTros { get; set; }
        public ICollection<DangKyCaNhan> DangKyCaNhans { get; set; }
        public ICollection<DangKyNhom> DangKyNhoms { get; set; }
        public ICollection<ChamDiem> ChamDiems { get; set; }
        public ICollection<ThongTinGCN> ThongTinGCNs { get; set; }
    }
}