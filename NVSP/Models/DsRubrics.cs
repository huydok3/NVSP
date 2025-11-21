using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("ds_rubrics")]
    public class DsRubrics
    {
        [Key]
        [Column("id_rubric")]
        public int IdRubric { get; set; }

        [Column("ten_rubric")]
        [MaxLength(255)]
        public string TenRubric { get; set; }

        public ICollection<ChiTietRubric> ChiTietRubrics { get; set; }
        public ICollection<HoatDongThi> HoatDongThis { get; set; }
    }
}