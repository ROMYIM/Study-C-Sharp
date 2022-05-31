using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NpoiSample.Models
{
    [Table("bas_customer_from")]
    public partial class BasCustomerFrom
    {
        /// <summary>
        /// 客户来源ID
        /// </summary>
        [Key]
        [Column("from_id")]
        [StringLength(50)]
        public string FromId { get; set; } = null!;
        /// <summary>
        /// 客户来源中文
        /// </summary>
        [Column("from_name_zh_cn")]
        [StringLength(200)]
        public string? FromNameZhCn { get; set; }
        /// <summary>
        /// 客户来源英文
        /// </summary>
        [Column("from_name_en_us")]
        [StringLength(200)]
        public string? FromNameEnUs { get; set; }
        /// <summary>
        /// 客户来源日文
        /// </summary>
        [Column("from_name_ja_jp")]
        [StringLength(200)]
        public string? FromNameJaJp { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Required]
        [Column("is_enabled")]
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_user_id")]
        [StringLength(50)]
        public string? UpdateUserId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time", TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time", TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_user_id")]
        [StringLength(50)]
        public string? CreateUserId { get; set; }
    }
}
