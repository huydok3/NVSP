using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("dang_ky_tham_du")]
    public class DangKyThamDu
    {
        [Column("ma_sv")]
        [MaxLength(50)]
        public string MaSv { get; set; }

        [Column("id_hd")]
        public int IdHd { get; set; }

        [Column("trang_thai")]
        public sbyte TrangThai { get; set; }

        public SinhVien SinhVien { get; set; }
        public HoatDongThamDu HoatDongThamDu { get; set; }
    }
}