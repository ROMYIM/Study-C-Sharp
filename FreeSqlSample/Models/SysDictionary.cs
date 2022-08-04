using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSqlSample.Models
{
    [Table("sys_dictionary")]
    public partial class SysDictionary
    {
        /// <summary>
        /// 数据字典ID
        /// </summary>
        [Key]
        [Column("dictionary_id")]
        [StringLength(50)]
        public string DictionaryId { get; set; } = null!;
        /// <summary>
        /// 字典编号
        /// </summary>
        [Column("dictionary_name")]
        [StringLength(100)]
        public string? DictionaryName { get; set; }
        /// <summary>
        /// 字典描述
        /// </summary>
        [Column("dictionary_desc")]
        [StringLength(200)]
        public string? DictionaryDesc { get; set; }
        /// <summary>
        /// 标识码
        /// </summary>
        [Column("value")]
        [StringLength(50)]
        public string? Value { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [Column("text_en_us")]
        [StringLength(100)]
        public string? TextEnUs { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        [Column("text_zh_cn")]
        [StringLength(100)]
        public string? TextZhCn { get; set; }
        /// <summary>
        /// 日文名称
        /// </summary>
        [Column("text_ja_jp")]
        [StringLength(100)]
        public string? TextJaJp { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        [Column("is_default")]
        public bool? IsDefault { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("seq")]
        public int? Seq { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("is_enabled")]
        public bool? IsEnabled { get; set; }
        [Column("crm_value")]
        [StringLength(50)]
        public string? CrmValue { get; set; }
    }
}
