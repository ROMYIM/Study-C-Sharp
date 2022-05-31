using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NpoiSample.Models
{
    [Table("cus_follow_list")]
    public partial class CusFollowList
    {
        /// <summary>
        /// 客户跟随主键ID
        /// </summary>
        [Key]
        [Column("cur_id")]
        [StringLength(50)]
        public string CurId { get; set; } = null!;
        /// <summary>
        /// 客户ID(外键)
        /// </summary>
        [Column("customer_id")]
        [StringLength(50)]
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// 跟案人
        /// </summary>
        [Column("track_user")]
        [StringLength(50)]
        public string? TrackUser { get; set; }
        /// <summary>
        /// 案件类型
        /// </summary>
        [Column("case_type")]
        [StringLength(50)]
        public string? CaseType { get; set; }
        /// <summary>
        /// 案件流向
        /// </summary>
        [Column("case_direction")]
        [StringLength(50)]
        public string? CaseDirection { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Required]
        [Column("is_enabled")]
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// 更新用户
        /// </summary>
        [Column("update_user_id")]
        [StringLength(50)]
        public string? UpdateUserId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time", TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [Column("customer_user_type")]
        [StringLength(50)]
        public string CustomerUserType { get; set; } = null!;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time", TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [Column("create_user_id")]
        [StringLength(50)]
        public string? CreateUserId { get; set; }
        /// <summary>
        /// 待确认(无数据)
        /// </summary>
        [Column("head_user_type")]
        [StringLength(50)]
        public string? HeadUserType { get; set; }
    }
}
