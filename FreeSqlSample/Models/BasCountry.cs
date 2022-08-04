using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSqlSample.Models
{
    [Table("bas_country")]
    public partial class BasCountry
    {
        /// <summary>
        /// 国家主键ID
        /// </summary>
        [Key]
        [Column("country_id")]
        [StringLength(50)]
        public string CountryId { get; set; } = null!;
        /// <summary>
        /// 中文名称
        /// </summary>
        [Column("country_zh_cn")]
        [StringLength(50)]
        public string? CountryZhCn { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [Column("country_en_us")]
        [StringLength(100)]
        public string? CountryEnUs { get; set; }
        /// <summary>
        /// 日文名称
        /// </summary>
        [Column("country_ja_jp")]
        [StringLength(100)]
        public string? CountryJaJp { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("seq")]
        public short? Seq { get; set; }
        /// <summary>
        /// 是否为马德里成员国
        /// </summary>
        [Column("is_madrid")]
        public bool IsMadrid { get; set; }
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
        public DateTime? UpdateTime { get; set; }
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
        /// 代码
        /// </summary>
        [Column("country_code")]
        [StringLength(50)]
        public string? CountryCode { get; set; }
        /// <summary>
        /// 国家ID
        /// </summary>
        [Column("id")]
        [StringLength(50)]
        public string Id { get; set; } = null!;
        [Column("is_ep")]
        public bool? IsEp { get; set; }
    }
}
