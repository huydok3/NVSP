using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("chi_tiet_rubric")]
    public class ChiTietRubric
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("tieu_chi")]
        [MaxLength(50)]
        public string TieuChi { get; set; }

        [Column("diem_toi_da")]
        public int DiemToiDa { get; set; }

        public DsRubrics DsRubrics { get; set; }
    }
}