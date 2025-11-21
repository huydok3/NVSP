using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("diem_danh")]
    public class DiemDanh
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("anh_minh_chung")]
        public byte[] AnhMinhChung { get; set; }

        [Column("thoi_gian")]
        public DateTime ThoiGian { get; set; }

        [Column("trang_thai")]
        public sbyte TrangThai { get; set; }
    }
}