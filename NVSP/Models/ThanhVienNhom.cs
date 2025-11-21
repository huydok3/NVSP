using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("thanh_vien_nhom")]
    public class ThanhVienNhom
    {
        [Column("id_nhom")]
        public int IdNhom { get; set; }

        [Column("ma_sv")]
        [MaxLength(50)]
        public string MaSv { get; set; }

        public DangKyNhom DangKyNhom { get; set; }
        public SinhVien SinhVien { get; set; }
    }
}