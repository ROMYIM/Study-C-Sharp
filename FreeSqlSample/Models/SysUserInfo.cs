using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSqlSample.Models
{
    [Table("sys_user_info")]
    public partial class SysUserInfo
    {
        [Key]
        [Column("user_id")]
        [StringLength(50)]
        public string UserId { get; set; } = null!;
        [Column("user_type")]
        public int UserType { get; set; }
        [Column("user_name")]
        [StringLength(50)]
        public string? UserName { get; set; }
        [Column("user_pass")]
        [StringLength(50)]
        public string? UserPass { get; set; }
        [Column("cn_name")]
        [StringLength(50)]
        public string CnName { get; set; } = null!;
        [Column("en_name")]
        [StringLength(50)]
        public string? EnName { get; set; }
        [Column("work_no")]
        [StringLength(50)]
        public string? WorkNo { get; set; }
        [Column("gender")]
        [StringLength(50)]
        public string? Gender { get; set; }
        [Column("email")]
        [StringLength(200)]
        public string? Email { get; set; }
        [Column("entry_date", TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [Column("birthday")]
        [StringLength(50)]
        public string? Birthday { get; set; }
        [Column("specialty")]
        [StringLength(300)]
        public string? Specialty { get; set; }
        [Column("card_type")]
        [StringLength(50)]
        public string? CardType { get; set; }
        [Column("card_no")]
        [StringLength(50)]
        public string? CardNo { get; set; }
        [Column("tel")]
        [StringLength(50)]
        public string? Tel { get; set; }
        [Column("mobile")]
        [StringLength(50)]
        public string? Mobile { get; set; }
        [Column("fax")]
        [StringLength(50)]
        public string? Fax { get; set; }
        [Column("address")]
        [StringLength(500)]
        public string? Address { get; set; }
        [Column("dept_id")]
        [StringLength(50)]
        public string? DeptId { get; set; }
        [Column("language")]
        [StringLength(50)]
        public string? Language { get; set; }
        [Column("create_user_id")]
        [StringLength(50)]
        public string? CreateUserId { get; set; }
        [Column("create_time", TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [Column("update_user_id")]
        [StringLength(50)]
        public string? UpdateUserId { get; set; }
        [Column("update_time", TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
        [Column("remark")]
        [StringLength(2000)]
        public string? Remark { get; set; }
        [Required]
        [Column("is_enabled")]
        public bool? IsEnabled { get; set; }
        [Column("security_question")]
        [StringLength(50)]
        public string? SecurityQuestion { get; set; }
        [Column("security_answer")]
        [StringLength(50)]
        public string? SecurityAnswer { get; set; }
        [Column("qq")]
        [StringLength(50)]
        public string? Qq { get; set; }
        [Column("wechart")]
        [StringLength(50)]
        public string? Wechart { get; set; }
        [Column("is_agent")]
        public bool? IsAgent { get; set; }
        [Column("nation")]
        [StringLength(50)]
        public string? Nation { get; set; }
        [Column("is_marry")]
        [StringLength(50)]
        public string? IsMarry { get; set; }
        [Column("political_status")]
        [StringLength(50)]
        public string? PoliticalStatus { get; set; }
        [Column("native_place")]
        [StringLength(500)]
        public string? NativePlace { get; set; }
        [Column("registered_permanent")]
        [StringLength(500)]
        public string? RegisteredPermanent { get; set; }
        [Column("new_user")]
        [StringLength(50)]
        public string? NewUser { get; set; }
        [Column("new_password")]
        [StringLength(50)]
        public string? NewPassword { get; set; }
        [Column("contract_date", TypeName = "datetime")]
        public DateTime? ContractDate { get; set; }
        [Column("urgent_way")]
        [StringLength(1000)]
        public string? UrgentWay { get; set; }
        [Column("driver_license_date", TypeName = "datetime")]
        public DateTime? DriverLicenseDate { get; set; }
        [Column("driver_expire_date", TypeName = "datetime")]
        public DateTime? DriverExpireDate { get; set; }
        [Column("manage_company")]
        [StringLength(50)]
        public string? ManageCompany { get; set; }
        [Column("work_type")]
        [StringLength(50)]
        public string? WorkType { get; set; }
        [Column("sub_tel")]
        [StringLength(50)]
        public string? SubTel { get; set; }
        [Column("user_duty")]
        [StringLength(50)]
        public string? UserDuty { get; set; }
        [Column("contract_start", TypeName = "datetime")]
        public DateTime? ContractStart { get; set; }
        [Column("high_education")]
        [StringLength(500)]
        public string? HighEducation { get; set; }
        [Column("age")]
        public int? Age { get; set; }
        [Column("driver_license_type")]
        [StringLength(50)]
        public string? DriverLicenseType { get; set; }
        [Column("native_type")]
        [StringLength(50)]
        public string? NativeType { get; set; }
        [Column("pay_mode")]
        [StringLength(50)]
        public string? PayMode { get; set; }
        [Column("expire_date", TypeName = "datetime")]
        public DateTime? ExpireDate { get; set; }
        [Column("sys_user_name")]
        [StringLength(200)]
        public string? SysUserName { get; set; }
        [Column("zkt_ssn")]
        [StringLength(50)]
        public string? ZktSsn { get; set; }
        [Required]
        [Column("is_login")]
        public bool? IsLogin { get; set; }
        [Column("is_free_trial")]
        public bool? IsFreeTrial { get; set; }
        [Column("display_number")]
        [StringLength(50)]
        public string? DisplayNumber { get; set; }
        [Column("host_number")]
        [StringLength(50)]
        public string? HostNumber { get; set; }
        [Column("leave_date", TypeName = "datetime")]
        public DateTime? LeaveDate { get; set; }
        [Required]
        [Column("fa_push")]
        public bool? FaPush { get; set; }
        [Column("private_email")]
        [StringLength(100)]
        public string? PrivateEmail { get; set; }
        [Column("socket_token")]
        [StringLength(50)]
        public string? SocketToken { get; set; }
        [Column("is_qc")]
        public bool? IsQc { get; set; }
        [Column("is_qc_teacher")]
        public bool? IsQcTeacher { get; set; }
        [Column("crm_account_id")]
        [StringLength(50)]
        public string? CrmAccountId { get; set; }
        [Column("crm_profile_id")]
        [StringLength(50)]
        public string? CrmProfileId { get; set; }
    }
}
