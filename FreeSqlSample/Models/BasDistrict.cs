using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSqlSample.Models
{
    [Table("bas_district")]
    public partial class BasDistrict
    {
        [Key]
        [Column("district_id")]
        [StringLength(50)]
        public string DistrictId { get; set; } = null!;
        [Column("district_code")]
        [StringLength(50)]
        public string DistrictCode { get; set; } = null!;
        [Column("text_en_us")]
        [StringLength(50)]
        public string? TextEnUs { get; set; }
        [Column("text_zh_cn")]
        [StringLength(50)]
        public string? TextZhCn { get; set; }
        [Column("text_ja_jp")]
        [StringLength(50)]
        public string? TextJaJp { get; set; }
        [Column("is_enabled")]
        public bool? IsEnabled { get; set; }
        [Column("seq")]
        public int? Seq { get; set; }
        [Column("update_user_id")]
        [StringLength(50)]
        public string? UpdateUserId { get; set; }
        [Column("update_time", TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
        [Column("create_time", TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [Column("create_user_id")]
        [StringLength(50)]
        public string? CreateUserId { get; set; }
        [Column("is_manage")]
        public bool IsManage { get; set; }
        [Column("crm_value")]
        [StringLength(50)]
        public string? CrmValue { get; set; }
    }
}
