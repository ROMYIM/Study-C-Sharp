using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace FreeSqlSample.Models {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "case_proc_info", DisableSyncStructure = true)]
	public partial class CaseProcInfo {

		/// <summary>
		/// 主键ID
		/// </summary>
		[JsonProperty, Column(Name = "proc_id", StringLength = 50, IsPrimary = true, IsNullable = false)]
		public string ProcId { get; set; } = Guid.NewGuid().ToString().ToUpper();

		/// <summary>
		/// 分案日
		/// </summary>
		[JsonProperty, Column(Name = "allocate_date")]
		public DateTime? AllocateDate { get; set; }

		[JsonProperty, Column(Name = "audit_result", StringLength = 500)]
		public string AuditResult { get; set; }

		/// <summary>
		/// 返发明人日
		/// </summary>
		[JsonProperty, Column(Name = "back_inventor_date")]
		public DateTime? BackInventorDate { get; set; }

		/// <summary>
		/// 返接口人日
		/// </summary>
		[JsonProperty, Column(Name = "back_ipr_date")]
		public DateTime? BackIprDate { get; set; }

		[JsonProperty, Column(Name = "bus_type_id", StringLength = 50)]
		public string BusTypeId { get; set; }

		[JsonProperty, Column(Name = "cancellation_reason", StringLength = 2000)]
		public string CancellationReason { get; set; }

		/// <summary>
		/// 主承办人
		/// 商标用
		/// </summary>
		[Column(Name = "proc_undertake_main_user_id")]
		public string? ProcUndertakeMainUserId { get; set; }

		/// <summary>
		/// 案件ID
		/// </summary>
		[JsonProperty, Column(Name = "case_id", StringLength = 50, IsNullable = false)]
		public string CaseId { get; set; }

		[JsonProperty, Column(Name = "create_time")]
		public DateTime? CreateTime { get; set; }

		[JsonProperty, Column(Name = "create_user_id", StringLength = 50)]
		public string CreateUserId { get; set; }

		/// <summary>
		/// 事项名称ID
		/// </summary>
		[JsonProperty, Column(Name = "ctrl_proc_id", StringLength = 200)]
		public string CtrlProcId { get; set; }

		[JsonProperty, Column(Name = "ctrl_proc_mark", StringLength = 50)]
		public string CtrlProcMark { get; set; }

		[JsonProperty, Column(Name = "ctrl_proc_property", StringLength = 50)]
		public string CtrlProcProperty { get; set; }

		/// <summary>
		/// 客户期限
		/// </summary>
		[JsonProperty, Column(Name = "cus_due_date")]
		public DateTime? CusDueDate { get; set; }

		[JsonProperty, Column(Name = "cus_finish_date")]
		public DateTime? CusFinishDate { get; set; }

		[JsonProperty, Column(Name = "cus_first_date")]
		public DateTime? CusFirstDate { get; set; }

		/// <summary>
		/// 客户要求返发明人期限
		/// </summary>
		[JsonProperty, Column(Name = "cus_inventor_date")]
		public DateTime? CusInventorDate { get; set; }

		/// <summary>
		/// 客户要求返接口人期限
		/// </summary>
		[JsonProperty, Column(Name = "cus_ipr_date")]
		public DateTime? CusIprDate { get; set; }

		[JsonProperty, Column(Name = "customer_case_no", StringLength = 50)]
		public string CustomerCaseNo { get; set; }

		/// <summary>
		/// 客户文号(商标)
		/// </summary>
		[JsonProperty, Column(Name = "customer_case_no_t", StringLength = 50)]
		public string CustomerCaseNoT { get; set; }

		[JsonProperty, Column(Name = "entrust_date")]
		public DateTime? EntrustDate { get; set; }

		/// <summary>
		/// 提成比例
		/// </summary>
		[JsonProperty, Column(Name = "examine_percentage", DbType = "money")]
		public decimal? ExaminePercentage { get; set; }

		[JsonProperty, Column(Name = "examiner", StringLength = 50)]
		public string Examiner { get; set; }

		[JsonProperty, Column(Name = "examiner_dept", StringLength = 500)]
		public string ExaminerDept { get; set; }

		[JsonProperty, Column(Name = "examiner_tel", StringLength = 50)]
		public string ExaminerTel { get; set; }

		[JsonProperty, Column(Name = "file_desc_id", StringLength = 50)]
		public string FileDescId { get; set; }

		/// <summary>
		/// 递交方式
		/// </summary>
		[JsonProperty, Column(Name = "filing_type", StringLength = 50)]
		public string FilingType { get; set; }

		/// <summary>
		/// 完成日
		/// </summary>
		[JsonProperty, Column(Name = "finish_date")]
		public DateTime? FinishDate { get; set; }

		/// <summary>
		/// 定稿日
		/// </summary>
		[JsonProperty, Column(Name = "finish_doc_date")]
		public DateTime? FinishDocDate { get; set; }

		/// <summary>
		/// 初稿日
		/// </summary>
		[JsonProperty, Column(Name = "first_doc_date")]
		public DateTime? FirstDocDate { get; set; }

		/// <summary>
		/// 核稿人
		/// </summary>
		[JsonProperty, Column(Name = "first_examine_user_id", StringLength = 50)]
		public string FirstExamineUserId { get; set; }

		/// <summary>
		/// 境外代理(商标)
		/// </summary>
		[JsonProperty, Column(Name = "foregin_agency_id", StringLength = 50)]
		public string ForeginAgencyId { get; set; }

		[JsonProperty, Column(Name = "i_is_import")]
		public bool IIsImport { get; set; } = false;

		/// <summary>
		/// 内部期限
		/// </summary>
		[JsonProperty, Column(Name = "int_due_date")]
		public DateTime? IntDueDate { get; set; }

		/// <summary>
		/// 内部定稿日
		/// </summary>
		[JsonProperty, Column(Name = "int_finish_date")]
		public DateTime? IntFinishDate { get; set; }

		[JsonProperty, Column(Name = "int_first_date")]
		public DateTime? IntFirstDate { get; set; }

		[JsonProperty, Column(Name = "invalidate_code", StringLength = 50)]
		public string InvalidateCode { get; set; }

		[JsonProperty, Column(Name = "invalidate_holder_name", StringLength = 200)]
		public string InvalidateHolderName { get; set; }

		[JsonProperty, Column(Name = "invalidate_request_user", StringLength = 200)]
		public string InvalidateRequestUser { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_enabled")]
		public bool? IsEnabled { get; set; } = true;

		[JsonProperty, Column(Name = "is_insteadofsubmitting")]
		public bool? IsInsteadofsubmitting { get; set; }

		[JsonProperty, Column(Name = "is_substance", StringLength = 50)]
		public string IsSubstance { get; set; }

		/// <summary>
		/// 法定期限
		/// </summary>
		[JsonProperty, Column(Name = "legal_due_date")]
		public DateTime? LegalDueDate { get; set; }

		[JsonProperty, Column(Name = "legal_provisions", StringLength = 50)]
		public string LegalProvisions { get; set; }

		[JsonProperty, Column(Name = "n_CameFileID", StringLength = 50)]
		public string NCameFileID { get; set; }

		[JsonProperty, Column(Name = "n_StatusID", StringLength = 50)]
		public string NStatusID { get; set; }

		[JsonProperty, Column(Name = "notice_name", StringLength = 500)]
		public string NoticeName { get; set; }

		[JsonProperty, Column(Name = "objection", StringLength = 50)]
		public string Objection { get; set; }

		[JsonProperty, Column(Name = "objection_id", StringLength = 1000)]
		public string ObjectionId { get; set; }

		[JsonProperty, Column(Name = "objection_name", StringLength = 2000)]
		public string ObjectionName { get; set; }

		/// <summary>
		/// 官方信息
		/// </summary>
		[JsonProperty, Column(Name = "official_note", StringLength = 4000)]
		public string OfficialNote { get; set; }

		[JsonProperty, Column(Name = "opponents", StringLength = 50)]
		public string Opponents { get; set; }

		[JsonProperty, Column(Name = "out_user", StringLength = 50)]
		public string OutUser { get; set; }

		/// <summary>
		/// 父级任务id
		/// </summary>
		[JsonProperty, Column(Name = "parent_proc_id", StringLength = 50)]
		public string ParentProcId { get; set; }

		[JsonProperty, Column(Name = "postmark_date")]
		public DateTime? PostmarkDate { get; set; }

		[JsonProperty, Column(Name = "private_id", StringLength = 50)]
		public string PrivateId { get; set; }

		[JsonProperty, Column(Name = "proc_app_date")]
		public DateTime? ProcAppDate { get; set; }

		[JsonProperty, Column(Name = "proc_app_no", StringLength = 50)]
		public string ProcAppNo { get; set; }

		[JsonProperty, Column(Name = "proc_dept_id", StringLength = 50)]
		public string ProcDeptId { get; set; }

		/// <summary>
		/// 任务编号
		/// </summary>
		[JsonProperty, Column(Name = "proc_no", StringLength = 50)]
		public string ProcNo { get; set; }

		/// <summary>
		/// 事项备注
		/// </summary>
		[JsonProperty, Column(Name = "proc_note", StringLength = 4000)]
		public string ProcNote { get; set; }

		/// <summary>
		/// 事项状态ID
		/// </summary>
		[JsonProperty, Column(Name = "proc_status_id", StringLength = 50)]
		public string ProcStatusId { get; set; }

		[JsonProperty, Column(Name = "proof_amount", StringLength = 50)]
		public string ProofAmount { get; set; }

		[JsonProperty, Column(Name = "reason_remark", StringLength = 2000)]
		public string ReasonRemark { get; set; }

		/// <summary>
		/// 官方来文日期
		/// </summary>
		[JsonProperty, Column(Name = "receive_date")]
		public DateTime? ReceiveDate { get; set; }

		[JsonProperty, Column(Name = "receive_no", StringLength = 50)]
		public string ReceiveNo { get; set; }

		[JsonProperty, Column(Name = "requestor", StringLength = 2000)]
		public string Requestor { get; set; }

		[JsonProperty, Column(Name = "result_receive_date")]
		public DateTime? ResultReceiveDate { get; set; }

		[JsonProperty, Column(Name = "result_remark", StringLength = 2000)]
		public string ResultRemark { get; set; }

		/// <summary>
		/// 送官方日期
		/// </summary>
		[JsonProperty, Column(Name = "send_official_date")]
		public DateTime? SendOfficialDate { get; set; }

		/// <summary>
		/// 送合作所日期
		/// </summary>
		[JsonProperty, Column(Name = "send_partner_date")]
		public DateTime? SendPartnerDate { get; set; }

		/// <summary>
		/// 事项序号
		/// </summary>
		[JsonProperty, Column(Name = "seq")]
		public short? Seq { get; set; }

		/// <summary>
		/// 用于显示的任务名称，一般后续不会做修改
		/// </summary>
		[JsonProperty, Column(Name = "show_name", StringLength = 50)]
		public string ShowName { get; set; }

		[JsonProperty, Column(Name = "simple_deliver_date")]
		public DateTime? SimpleDeliverDate { get; set; }

		[JsonProperty, Column(Name = "sub_proc_status_id", StringLength = 50)]
		public string SubProcStatusId { get; set; }

		[JsonProperty, Column(Name = "supplementary_deliver_date")]
		public DateTime? SupplementaryDeliverDate { get; set; }

		[JsonProperty, Column(Name = "titular_write_user", StringLength = 50)]
		public string TitularWriteUser { get; set; }

		/// <summary>
		/// 经办人
		/// </summary>
		[JsonProperty, Column(Name = "track_user_id", StringLength = 50)]
		public string TrackUserId { get; set; }

		[JsonProperty, Column(Name = "translate_amount", StringLength = 50)]
		public string TranslateAmount { get; set; }

		[JsonProperty, Column(Name = "translate_reader", StringLength = 50)]
		public string TranslateReader { get; set; }

		[JsonProperty, Column(Name = "translate_type", StringLength = 50)]
		public string TranslateType { get; set; }

		[JsonProperty, Column(Name = "translator", StringLength = 50)]
		public string Translator { get; set; }

		/// <summary>
		/// 承办人
		/// </summary>
		[JsonProperty, Column(Name = "undertake_user_id", StringLength = 50)]
		public string UndertakeUserId { get; set; }

		[JsonProperty, Column(Name = "update_time")]
		public DateTime? UpdateTime { get; set; }

		[JsonProperty, Column(Name = "update_user_id", StringLength = 50)]
		public string UpdateUserId { get; set; }

		/// <summary>
		/// 是否订单添加任务
		/// </summary>
		[JsonProperty, Column(Name = "is_orderadd")]
		public bool? IsOrderadd { get; set; } = false;

		/// <summary>
		/// 对应产品Id
		/// </summary>
		[JsonProperty, Column(Name = "product_id", StringLength = 50)]
		public string ProductId { get; set; }
	}

}
