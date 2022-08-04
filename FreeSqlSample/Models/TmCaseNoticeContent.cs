using FreeSql.DataAnnotations;

namespace FreeSqlSample.Models 
{

	[Table(Name = "tm_case_notice_content", DisableSyncStructure = true)]
	public partial class TmCaseNoticeContent 
	{

		[Column(Name = "file_id", StringLength = 50, IsPrimary = true, IsNullable = false)]
		public string FileId { get; set; }

		[Column(Name = "batch_id", StringLength = 50, IsNullable = false)]
		public string BatchId { get; set; }

		[Column(Name = "case_id", StringLength = 50)]
		public string CaseId { get; set; }

		[Column(Name = "create_time", InsertValueSql = "getdate()")]
		public DateTime CreateTime { get; set; }

		[Column(Name = "create_user_id", StringLength = 50)]
		public string CreateUserId { get; set; }

		[Column(Name = "daili_wenhao", StringLength = 50)]
		public string DailiWenhao { get; set; }

		[Column(Name = "fawen_bianhao", StringLength = 50)]
		public string FawenBianhao { get; set; }

		[Column(Name = "fawen_riqi")]
		public DateTime? FawenRiqi { get; set; }

		[Column(Name = "file_code", StringLength = 50)]
		public string FileCode { get; set; }

		[Column(Name = "file_desc_id", StringLength = 50)]
		public string FileDescId { get; set; }

		[Column(Name = "flow_status", StringLength = 50)]
		public string FlowStatus { get; set; }

		[Column(Name = "fuwu_xiangmu", StringLength = -2)]
		public string FuwuXiangmu { get; set; }

		[Column(Name = "fuwu_xiangmu_totals", StringLength = 50)]
		public string FuwuXiangmuTotals { get; set; }

		[Column(Name = "guoji_fenlie", StringLength = 1000)]
		public string GuojiFenlie { get; set; }

		[Column(Name = "int_due_date")]
		public DateTime? IntDueDate { get; set; }

		[Column(Name = "is_express")]
		public bool IsExpress { get; set; } = false;

		[Column(Name = "legal_due_date")]
		public DateTime? LegalDueDate { get; set; }

		[Column(Name = "pic_file_no", StringLength = 50)]
		public string PicFileNo { get; set; }

		[Column(Name = "proc_id", StringLength = 50)]
		public string ProcId { get; set; }

		[Column(Name = "remark", StringLength = 4000)]
		public string Remark { get; set; }

		[Column(Name = "seq", DbType = "numeric(18,0)")]
		public decimal? Seq { get; set; }

		[Column(Name = "shangbiao_shenqinghao", StringLength = 50)]
		public string ShangbiaoShenqinghao { get; set; }

		[Column(Name = "shenqing_bianhao", StringLength = 50)]
		public string ShenqingBianhao { get; set; }

		[Column(Name = "shenqing_hao", StringLength = 50)]
		public string ShenqingHao { get; set; }

		[Column(Name = "shenqing_riqi")]
		public DateTime? ShenqingRiqi { get; set; }

		[Column(Name = "tongzhishu_name", StringLength = 500)]
		public string TongzhishuName { get; set; }

		[Column(Name = "track_user_id", StringLength = 50)]
		public string TrackUserId { get; set; }

		[Column(Name = "update_time", InsertValueSql = "getdate()")]
		public DateTime UpdateTime { get; set; }

		[Column(Name = "update_user_id", StringLength = 50)]
		public string UpdateUserId { get; set; }

		[Column(Name = "zhuce_hao", StringLength = 50)]
		public string ZhuceHao { get; set; }

	}

}
