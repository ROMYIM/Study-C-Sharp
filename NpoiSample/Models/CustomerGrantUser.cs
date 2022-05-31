using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NpoiSample.Models
{
    [Table("customer_grant_user")]
    public partial class CustomerGrantUser
    {
        [Column("customer_id")]
        [StringLength(50)]
        public string? CustomerId { get; set; }
        [Column("user_id")]
        [StringLength(50)]
        public string? UserId { get; set; }
        /// <summary>
        /// 授权类型  0表示创建人 1表示案源人 2表示跟案人 3表示授权人
        /// </summary>
        [Column("grant_type")]
        [StringLength(50)]
        public string? GrantType { get; set; }
        [Column("create_time", TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column("grant_id")]
        [StringLength(50)]
        public string? GrantId { get; set; }
        [Column("is_add")]
        public bool IsAdd { get; set; }
        [Column("is_batch")]
        public bool IsBatch { get; set; }
    }
}
