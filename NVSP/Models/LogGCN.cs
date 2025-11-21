using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVSP.Models
{
    [Table("log_gcn")]
    public class LogGCN
    {
        [Key]
        [Column("id_gcn")]
        public int IdGcn { get; set; }

        [Column("hanh_dong")]
        public string HanhDong { get; set; }

        [Column("ma_btc")]
        public int MaBtc { get; set; }

        public ThongTinGCN ThongTinGCN { get; set; }
    }
}