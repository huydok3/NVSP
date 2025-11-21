using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("log_tra_cuu")]
    public class LogTraCuu
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_gcn")]
        public int IdGcn { get; set; }

        [Column("tg_tra_cuu")]
        public DateTime TgTraCuu { get; set; }

        public ThongTinGCN ThongTinGCN { get; set; }
    }
}