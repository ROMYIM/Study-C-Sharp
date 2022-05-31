using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NpoiSample.Models
{
    [Table("cus_customer")]
    public partial class CusCustomer
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        [Key]
        [Column("customer_id")]
        [StringLength(50)]
        public string CustomerId { get; set; } = null!;
        /// <summary>
        /// 客户简称
        /// </summary>
        [Column("customer_code")]
        [StringLength(100)]
        public string? CustomerCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [Column("customer_name")]
        [StringLength(1000)]
        public string? CustomerName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [Column("customer_name_en")]
        [StringLength(2000)]
        public string? CustomerNameEn { get; set; }
        /// <summary>
        /// 信誉等级
        /// </summary>
        [Column("customer_credit")]
        [StringLength(50)]
        public string? CustomerCredit { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        [Column("country_id")]
        [StringLength(50)]
        public string? CountryId { get; set; }
        /// <summary>
        /// 客户负责人
        /// </summary>
        [Column("corporation")]
        [StringLength(50)]
        public string? Corporation { get; set; }
        /// <summary>
        /// 客户来源
        /// </summary>
        [Column("customer_from")]
        [StringLength(50)]
        public string? CustomerFrom { get; set; }
        /// <summary>
        /// （未使用？）
        /// </summary>
        [Column("head_user_id1")]
        [StringLength(50)]
        public string? HeadUserId1 { get; set; }
        /// <summary>
        /// 案源类别( personal 表示个案,public表示公案)
        /// </summary>
        [Column("head_user_type")]
        [StringLength(50)]
        public string? HeadUserType { get; set; }
        /// <summary>
        /// 申请人类型
        /// </summary>
        [Column("type_id")]
        [StringLength(50)]
        public string? TypeId { get; set; }
        /// <summary>
        /// 保密等级
        /// </summary>
        [Column("secret_id")]
        [StringLength(50)]
        public string? SecretId { get; set; }
        /// <summary>
        /// 电话
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
        [StringLength(200)]
        public string? Email { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("is_enabled")]
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// （未使用？）
        /// </summary>
        [Column("introducer")]
        [StringLength(50)]
        public string? Introducer { get; set; }
        /// <summary>
        /// （未使用？）
        /// </summary>
        [Column("introducer_tel")]
        [StringLength(50)]
        public string? IntroducerTel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        [StringLength(2000)]
        public string? Remark { get; set; }
        /// <summary>
        /// 更新人（关联）
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
        /// 创建人（关联）
        /// </summary>
        [Column("create_user_id")]
        [StringLength(50)]
        public string CreateUserId { get; set; } = null!;
        /// <summary>
        /// 利益冲突
        /// </summary>
        [Column("is_conflict")]
        public bool? IsConflict { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        [Column("industry")]
        [StringLength(50)]
        public string? Industry { get; set; }
        [Column("customer_full_name")]
        [StringLength(2000)]
        public string? CustomerFullName { get; set; }
        [Column("parent_id")]
        [StringLength(50)]
        public string? ParentId { get; set; }
        [Column("is_applicant")]
        public bool? IsApplicant { get; set; }
        [Column("is_cooperation")]
        public bool? IsCooperation { get; set; }
        [Column("customer_name_other")]
        [StringLength(300)]
        public string? CustomerNameOther { get; set; }
        [Column("website")]
        [StringLength(1000)]
        public string? Website { get; set; }
        [Column("district")]
        [StringLength(50)]
        public string? District { get; set; }
        [Column("balance_way")]
        [StringLength(50)]
        public string? BalanceWay { get; set; }
        [Column("pay_way")]
        [StringLength(50)]
        public string? PayWay { get; set; }
        [Column("receive_rule")]
        [StringLength(50)]
        public string? ReceiveRule { get; set; }
        [Column("payment_name")]
        [StringLength(50)]
        public string? PaymentName { get; set; }
        [Column("manage_company")]
        [StringLength(50)]
        public string? ManageCompany { get; set; }
        [Column("is_import")]
        public bool IsImport { get; set; }
        [Column("crm_customer_id")]
        [StringLength(50)]
        public string? CrmCustomerId { get; set; }
        [Column("clue_user_id")]
        [StringLength(50)]
        public string? ClueUserId { get; set; }
        [Column("busi_user_id")]
        [StringLength(50)]
        public string? BusiUserId { get; set; }
        [Column("crm_customer_code")]
        [StringLength(50)]
        public string? CrmCustomerCode { get; set; }
        [Column("addr_province")]
        [StringLength(256)]
        public string? AddrProvince { get; set; }
        [Column("addr_city")]
        [StringLength(256)]
        public string? AddrCity { get; set; }
        [Column("addr_district")]
        [StringLength(256)]
        public string? AddrDistrict { get; set; }
    }
}
