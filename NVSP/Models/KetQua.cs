using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("ket_qua")]
    public class KetQua
    {
        [Key]
        [Column("id_cham_diem")]
        public int IdChamDiem { get; set; }

        [Column("giai_thuong")]
        [MaxLength(50)]
        public string GiaiThuong { get; set; }

        [Column("trang_thai")]
        public sbyte TrangThai { get; set; }

        public ChamDiem ChamDiem { get; set; }
        public ICollection<ThongTinGCN> ThongTinGCNs { get; set; }
    }
}