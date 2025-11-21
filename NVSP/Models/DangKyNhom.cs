using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("dang_ky_nhom")]
    public class DangKyNhom
    {
        [Key]
        [Column("id_nhom")]
        public int IdNhom { get; set; }

        [Column("ten_nhom")]
        [MaxLength(255)]
        public string TenNhom { get; set; }

        [Column("ma_tham_gia")]
        public int MaThamGia { get; set; }

        [Column("id_hd")]
        public int IdHd { get; set; }

        [Column("trang_thai")]
        public sbyte TrangThai { get; set; }

        public HoatDongThi HoatDongThi { get; set; }
        public ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; }
        public ICollection<ChamDiem> ChamDiems { get; set; }
    }
}