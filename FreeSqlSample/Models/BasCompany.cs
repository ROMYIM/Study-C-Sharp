using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSqlSample.Models
{
    [Table("bas_company")]
    public partial class BasCompany
    {
        /// <summary>
        /// 内部机构ID
        /// </summary>
        [Key]
        [Column("company_id")]
        [StringLength(50)]
        public string CompanyId { get; set; } = null!;
        /// <summary>
        /// 代理机构ID（未使用？）
        /// </summary>
        [Column("agency_id")]
        [StringLength(50)]
        public string? AgencyId { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        [Column("company_name_cn")]
        [StringLength(50)]
        public string? CompanyNameCn { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [Column("company_name_en")]
        [StringLength(500)]
        public string? CompanyNameEn { get; set; }
        /// <summary>
        /// 日文名称
        /// </summary>
        [Column("company_name_jp")]
        [StringLength(50)]
        public string? CompanyNameJp { get; set; }
        /// <summary>
        /// 国家ID
        /// </summary>
        [Column("country_id")]
        [StringLength(50)]
        public string? CountryId { get; set; }
        /// <summary>
        /// 所属地区ID
        /// </summary>
        [Column("district_id")]
        [StringLength(50)]
        public string? DistrictId { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        [Column("legal_person")]
        [StringLength(50)]
        public string? LegalPerson { get; set; }
        /// <summary>
        /// 中文地址
        /// </summary>
        [Column("address_cn")]
        [StringLength(200)]
        public string? AddressCn { get; set; }
        /// <summary>
        /// 英文地址
        /// </summary>
        [Column("address_en")]
        [StringLength(500)]
        public string? AddressEn { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [Column("post_code")]
        [StringLength(50)]
        public string? PostCode { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [Column("contacter")]
        [StringLength(50)]
        public string? Contacter { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Column("tel")]
        [StringLength(100)]
        public string? Tel { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        [Column("fax")]
        [StringLength(100)]
        public string? Fax { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("email")]
        [StringLength(100)]
        public string? Email { get; set; }
        /// <summary>
        /// 备注（未使用？）
        /// </summary>
        [Column("remark")]
        [StringLength(500)]
        public string? Remark { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("is_enabled")]
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        [Column("tax_rate", TypeName = "money")]
        public decimal? TaxRate { get; set; }
        /// <summary>
        /// 更新人（关联用户ID）
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
        /// 创建人（关联用户ID）
        /// </summary>
        [Column("create_user_id")]
        [StringLength(50)]
        public string? CreateUserId { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        [Column("company_code")]
        [StringLength(50)]
        public string? CompanyCode { get; set; }
        /// <summary>
        /// 创立时间
        /// </summary>
        [Column("estab_day", TypeName = "datetime")]
        public DateTime? EstabDay { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("seq")]
        public int? Seq { get; set; }
        /// <summary>
        /// 是否管理？（未使用？）
        /// </summary>
        [Column("is_manage")]
        public bool IsManage { get; set; }
        /// <summary>
        /// 中文简称
        /// </summary>
        [Column("short_name_cn")]
        [StringLength(200)]
        public string? ShortNameCn { get; set; }
        /// <summary>
        /// 英国简称（未使用？）
        /// </summary>
        [Column("short_name_en")]
        [StringLength(200)]
        public string? ShortNameEn { get; set; }
        /// <summary>
        /// 日文简称（未使用？）
        /// </summary>
        [Column("short_name_jp")]
        [StringLength(200)]
        public string? ShortNameJp { get; set; }
        /// <summary>
        /// 要点金额？（未使用？）
        /// </summary>
        [Column("point_price", TypeName = "money")]
        public decimal? PointPrice { get; set; }
        [Column("province")]
        [StringLength(50)]
        public string? Province { get; set; }
        [Column("city")]
        [StringLength(50)]
        public string? City { get; set; }
        [Column("area")]
        [StringLength(50)]
        public string? Area { get; set; }
        [Column("address_detail")]
        [StringLength(500)]
        public string? AddressDetail { get; set; }
        [Column("credit_code")]
        [StringLength(50)]
        public string? CreditCode { get; set; }
        [Column("crm_value")]
        [StringLength(50)]
        public string? CrmValue { get; set; }
    }
}
