using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("dang_ky_ca_nhan")]
    public class DangKyCaNhan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_hd")]
        public int IdHd { get; set; }

        [Column("ma_sv")]
        [MaxLength(50)]
        public string MaSv { get; set; }

        [Column("trang_thai")]
        public sbyte TrangThai { get; set; }

        public HoatDongThi HoatDongThi { get; set; }
        public SinhVien SinhVien { get; set; }
        public ICollection<ChamDiem> ChamDiems { get; set; }
    }
}