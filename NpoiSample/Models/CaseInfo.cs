using FreeSql.DataAnnotations;

namespace NpoiSample.Models 
{

	[Table(Name = "case_info", DisableSyncStructure = true)]
	public partial class CaseInfo {

		/// <summary>
		/// 案件ID
		/// </summary>
		[Column(Name = "case_id", StringLength = 50, IsPrimary = true, IsNullable = false)]
		public string CaseId { get; set; } = Guid.NewGuid().ToString().ToUpper();

		/// <summary>
		/// 惠州活动案
		/// </summary>
		[Column(Name = "active_case")]
		public bool ActiveCase { get; set; } = false;

		/// <summary>
		/// 修改类别
		/// </summary>
		[Column(Name = "active_type_id", StringLength = 50)]
		public string ActiveTypeId { get; set; }

		/// <summary>
		/// 境内代理
		/// </summary>
		[Column(Name = "agency_id", StringLength = 50)]
		public string AgencyId { get; set; }

		/// <summary>
		/// 申请日
		/// </summary>
		[Column(Name = "app_date")]
		public DateTime? AppDate { get; set; }

		/// <summary>
		/// 申请号
		/// </summary>
		[Column(Name = "app_no", StringLength = 50)]
		public string AppNo { get; set; }

		/// <summary>
		/// 第一申请人ID
		/// </summary>
		[Column(Name = "applicant_id", StringLength = 50)]
		public string ApplicantId { get; set; }

		[Column(Name = "apply_channel", StringLength = 50)]
		public string ApplyChannel { get; set; }

		/// <summary>
		/// 提案ID
		/// </summary>
		[Column(Name = "apply_id", StringLength = 50)]
		public string ApplyId { get; set; }

		/// <summary>
		/// 提案名称
		/// </summary>
		[Column(Name = "apply_name", StringLength = 500)]
		public string ApplyName { get; set; }

		/// <summary>
		/// 提案号
		/// </summary>
		[Column(Name = "apply_no", StringLength = 50)]
		public string ApplyNo { get; set; }

		/// <summary>
		/// 申请类型ID
		/// </summary>
		[Column(Name = "apply_type_id", StringLength = 50)]
		public string ApplyTypeId { get; set; }

		/// <summary>
		/// 颁证日期
		/// </summary>
		[Column(Name = "banzheng_riqi")]
		public DateTime? BanzhengRiqi { get; set; }

		/// <summary>
		/// 案源分所
		/// </summary>
		[Column(Name = "belong_company", StringLength = 50)]
		public string BelongCompany { get; set; }

		/// <summary>
		/// 案源地区
		/// </summary>
		[Column(Name = "belong_district", StringLength = 50)]
		public string BelongDistrict { get; set; }

		/// <summary>
		/// 业务类型ID
		/// </summary>
		[Column(Name = "business_type_id", StringLength = 50)]
		public string BusinessTypeId { get; set; }

		/// <summary>
		/// 案件描述说明
		/// </summary>
		[Column(Name = "case_descriptions", StringLength = 4000)]
		public string CaseDescriptions { get; set; }

		/// <summary>
		/// 案件流向
		/// </summary>
		[Column(Name = "case_direction", StringLength = 50)]
		public string CaseDirection { get; set; }

		/// <summary>
		/// 急案标识
		/// </summary>
		[Column(Name = "case_emergent_id", StringLength = 50)]
		public string CaseEmergentId { get; set; }

		/// <summary>
		/// 案件级别ID
		/// </summary>
		[Column(Name = "case_level_id", StringLength = 50)]
		public string CaseLevelId { get; set; }

		/// <summary>
		/// 发明名称
		/// </summary>
		[Column(Name = "case_name", StringLength = 500)]
		public string CaseName { get; set; }

		/// <summary>
		/// 案件英文名称
		/// </summary>
		[Column(Name = "case_name_en", StringLength = 500)]
		public string CaseNameEn { get; set; }

		/// <summary>
		/// 案件状态ID
		/// </summary>
		[Column(Name = "case_status_id", StringLength = 50)]
		public string CaseStatusId { get; set; }

		/// <summary>
		/// 案件类型ID
		/// </summary>
		[Column(Name = "case_type_id", StringLength = 50)]
		public string CaseTypeId { get; set; }

		/// <summary>
		/// 证书号
		/// </summary>
		[Column(Name = "certificate_no", StringLength = 50)]
		public string CertificateNo { get; set; }

		[Column(Name = "charge_rule", StringLength = 50)]
		public string ChargeRule { get; set; }

		/// <summary>
		/// 合同编号
		/// </summary>
		[Column(Name = "contract_no", StringLength = 200)]
		public string ContractNo { get; set; }

		/// <summary>
		/// 国家ID
		/// </summary>
		[Column(Name = "country_id", StringLength = 50)]
		public string CountryId { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		[Column(Name = "create_time")]
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 创建用户
		/// </summary>
		[Column(Name = "create_user_id", StringLength = 50)]
		public string CreateUserId { get; set; }

		[Column(Name = "crm_case_id", StringLength = 50)]
		public string CrmCaseId { get; set; }

		/// <summary>
		/// 客户文号
		/// </summary>
		[Column(Name = "customer_case_no", StringLength = 100)]
		public string CustomerCaseNo { get; set; }

		/// <summary>
		/// 客户ID
		/// </summary>
		[Column(Name = "customer_id", StringLength = 300)]
		public string CustomerId { get; set; }

		[Column(Name = "das_code", StringLength = 50)]
		public string DasCode { get; set; }

		/// <summary>
		/// 递交机构(商标)
		/// </summary>
		[Column(Name = "deliver_agency", StringLength = 50)]
		public string DeliverAgency { get; set; }

		/// <summary>
		/// 新申请递交日
		/// </summary>
		[Column(Name = "deliver_date")]
		public DateTime? DeliverDate { get; set; }

		/// <summary>
		/// 分案属性
		/// </summary>
		[Column(Name = "division_property", StringLength = 50)]
		public string DivisionProperty { get; set; }

		/// <summary>
		/// 委案日期
		/// </summary>
		[Column(Name = "entrust_date")]
		public DateTime? EntrustDate { get; set; }

		/// <summary>
		/// 进入实审日
		/// </summary>
		[Column(Name = "examime_date")]
		public DateTime? ExamimeDate { get; set; }

		/// <summary>
		/// 届满日
		/// </summary>
		[Column(Name = "expire_date")]
		public DateTime? ExpireDate { get; set; }

		/// <summary>
		/// 费减比例
		/// </summary>
		[Column(Name = "fee_reduce")]
		public int? FeeReduce { get; set; }

		/// <summary>
		/// 申请方式
		/// </summary>
		[Column(Name = "filing_type", StringLength = 50)]
		public string FilingType { get; set; }

		/// <summary>
		/// 第一代理人
		/// </summary>
		[Column(Name = "first_agency_user", StringLength = 50)]
		public string FirstAgencyUser { get; set; }

		/// <summary>
		/// 办登年费阶段
		/// 首缴年度
		/// </summary>
		[Column(Name = "first_pay_annual", StringLength = 50)]
		public string FirstPayAnnual { get; set; }

		/// <summary>
		/// 最早优先权日期
		/// </summary>
		[Column(Name = "first_priority_date")]
		public DateTime? FirstPriorityDate { get; set; }

		/// <summary>
		/// 境外代理
		/// </summary>
		[Column(Name = "foregin_agency_id", StringLength = 50)]
		public string ForeginAgencyId { get; set; }

		/// <summary>
		/// 外所文号
		/// </summary>
		[Column(Name = "foregin_case_no", StringLength = 50)]
		public string ForeginCaseNo { get; set; }

		// [Column(Name = "i_is_import")]
		// public bool IIsImport { get; set; } = false;

		/// <summary>
		/// 案件编号
		/// </summary>
		[Column(Name = "invalid_code", StringLength = 50)]
		public string InvalidCode { get; set; }

		/// <summary>
		/// 专利权持有人
		/// </summary>
		[Column(Name = "invalid_holder_name", StringLength = 4000)]
		public string InvalidHolderName { get; set; }

		/// <summary>
		/// 请求人
		/// </summary>
		[Column(Name = "invalid_request_user", StringLength = 1000)]
		public string InvalidRequestUser { get; set; }

		/// <summary>
		/// 主动修改
		/// </summary>
		[Column(Name = "is_active")]
		public bool? IsActive { get; set; }

		/// <summary>
		/// 预审
		/// </summary>
		[Column(Name = "is_advance_check")]
		public bool? IsAdvanceCheck { get; set; } = false;

		/// <summary>
		/// 提前公布
		/// </summary>
		[Column(Name = "is_ahead_pub")]
		public bool? IsAheadPub { get; set; } = false;

		/// <summary>
		/// CA申请
		/// </summary>
		[Column(Name = "is_ca")]
		public bool? IsCa { get; set; }

		/// <summary>
		/// CIP申请
		/// </summary>
		[Column(Name = "is_cip")]
		public bool? IsCip { get; set; }

		[Column(Name = "is_color")]
		public bool IsColor { get; set; } = false;

		/// <summary>
		/// 分案
		/// </summary>
		[Column(Name = "is_division")]
		public bool? IsDivision { get; set; }

		/// <summary>
		/// 是否有效
		/// </summary>
		[Column(Name = "is_enabled")]
		public bool? IsEnabled { get; set; } = true;

		/// <summary>
		/// 是否实质审查
		/// </summary>
		[Column(Name = "is_essence_exam")]
		public bool? IsEssenceExam { get; set; } = false;

		/// <summary>
		/// 请求费用减缓
		/// </summary>
		[Column(Name = "is_fee_reduce")]
		public bool? IsFeeReduce { get; set; } = false;

		/// <summary>
		/// 请求宽限（32个月）
		/// </summary>
		[Column(Name = "is_grace")]
		public bool IsGrace { get; set; } = false;

		/// <summary>
		/// 无效程序标识
		/// </summary>
		[Column(Name = "is_invalid_program")]
		public bool? IsInvalidProgram { get; set; }

		/// <summary>
		/// PPH申请
		/// </summary>
		[Column(Name = "is_pph")]
		public bool? IsPph { get; set; }

		[Column(Name = "is_preservation")]
		public bool? IsPreservation { get; set; }

		/// <summary>
		/// 是否优先审查
		/// </summary>
		[Column(Name = "is_priority_review")]
		public bool? IsPriorityReview { get; set; }

		/// <summary>
		/// 请求DAS
		/// </summary>
		[Column(Name = "is_request_das")]
		public bool? IsRequestDas { get; set; } = false;

		/// <summary>
		/// 是否同日申请
		/// </summary>
		[Column(Name = "is_same_day")]
		public bool? IsSameDay { get; set; } = false;

		/// <summary>
		/// 请求保密审查
		/// </summary>
		[Column(Name = "is_secrecy_request")]
		public bool? IsSecrecyRequest { get; set; } = false;

		[Column(Name = "is_synchronization")]
		public bool? IsSynchronization { get; set; } = true;

		/// <summary>
		/// 公告日
		/// </summary>
		[Column(Name = "issue_date")]
		public DateTime? IssueDate { get; set; }

		/// <summary>
		/// 公告号
		/// </summary>
		[Column(Name = "issue_no", StringLength = 50)]
		public string IssueNo { get; set; }

		/// <summary>
		/// 管理分所（广、北、深公司）
		/// </summary>
		[Column(Name = "manage_company", StringLength = 50)]
		public string ManageCompany { get; set; }

		/// <summary>
		/// 管理地区
		/// </summary>
		[Column(Name = "manage_district", StringLength = 50)]
		public string ManageDistrict { get; set; }

		[Column(Name = "member_country", StringLength = 500)]
		public string MemberCountry { get; set; }

		/// <summary>
		/// 申请方式
		/// </summary>
		[Column(Name = "multi_type", StringLength = 50)]
		public string MultiType { get; set; }

		/// <summary>
		/// 官方状态
		/// </summary>
		[Column(Name = "official_status_id", StringLength = 50)]
		public string OfficialStatusId { get; set; }

		/// <summary>
		/// PCT进入
		/// </summary>
		[Column(Name = "pct_enter")]
		public bool PctEnter { get; set; } = false;

		/// <summary>
		/// 项目编号
		/// </summary>
		[Column(Name = "project_no", StringLength = 50)]
		public string ProjectNo { get; set; }

		/// <summary>
		/// 公开日
		/// </summary>
		[Column(Name = "pub_date")]
		public DateTime? PubDate { get; set; }

		/// <summary>
		/// 公开号
		/// </summary>
		[Column(Name = "pub_no", StringLength = 50)]
		public string PubNo { get; set; }

		/// <summary>
		/// 商标注册号
		/// </summary>
		[Column(Name = "register_no", StringLength = 50)]
		public string RegisterNo { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[Column(Name = "remark", StringLength = 4000)]
		public string Remark { get; set; }

		/// <summary>
		/// 续展日，仅用于商标
		/// </summary>
		[Column(Name = "renewal_date")]
		public DateTime? RenewalDate { get; set; }

		/// <summary>
		/// 本案客户要求
		/// </summary>
		[Column(Name = "request_remark", StringLength = 4000)]
		public string RequestRemark { get; set; }

		/// <summary>
		/// 案源人、业务人员、洽案人
		/// </summary>
		[Column(Name = "sales_user_id", StringLength = 50)]
		public string SalesUserId { get; set; }

		/// <summary>
		/// 第二代理人
		/// </summary>
		[Column(Name = "second_agency_user", StringLength = 50)]
		public string SecondAgencyUser { get; set; }

		[Column(Name = "show_mode", StringLength = 50)]
		public string ShowMode { get; set; }

		[Column(Name = "sz_volume", StringLength = 50)]
		public string SzVolume { get; set; }

		/// <summary>
		/// 技术领域ID
		/// </summary>
		[Column(Name = "tech_field_id", StringLength = 50)]
		public string TechFieldId { get; set; }

		[Column(Name = "track_user", StringLength = 50)]
		public string TrackUser { get; set; }

		/// <summary>
		/// 商标分类
		/// </summary>
		[Column(Name = "trademark_class", StringLength = 500)]
		public string TrademarkClass { get; set; }

		/// <summary>
		/// 商标联系人ID
		/// </summary>
		[Column(Name = "trademark_contact_id", StringLength = 50)]
		public string TrademarkContactId { get; set; }

		/// <summary>
		/// 承办部门
		/// </summary>
		[Column(Name = "undertake_dept_id", StringLength = 50)]
		public string UndertakeDeptId { get; set; }

		/// <summary>
		/// 承办人
		/// </summary>
		[Column(Name = "undertake_main_user_id", StringLength = 200)]
		public string UndertakeMainUserId { get; set; }

		/// <summary>
		/// 更新时间
		/// </summary>
		[Column(Name = "update_time")]
		public DateTime? UpdateTime { get; set; }

		/// <summary>
		/// 更新用户
		/// </summary>
		[Column(Name = "update_user_id", StringLength = 50)]
		public string UpdateUserId { get; set; }

		/// <summary>
		/// 版本
		/// </summary>
		[Column(Name = "version_id", StringLength = 50)]
		public string VersionId { get; set; }

		/// <summary>
		/// 卷号
		/// </summary>
		[Column(Name = "volume", StringLength = 50)]
		public string Volume { get; set; }

		/// <summary>
		/// 原卷号
		/// </summary>
		[Column(Name = "volume_old", StringLength = 50)]
		public string VolumeOld { get; set; }

	}

}
