using FreeSql.DataAnnotations;

namespace NpoiSample.Models 
{

	[Table(Name = "tm_case_notice_fenjian", DisableSyncStructure = true)]
	public partial class TmCaseNoticeFenjian {

		[Column(Name = "file_id", StringLength = 50, IsPrimary = true, IsNullable = false)]
		public string FileId { get; set; }

		[Column(Name = "create_time", InsertValueSql = "getdate()")]
		public DateTime CreateTime { get; set; }

		[Column(Name = "create_user_id", StringLength = 50)]
		public string CreateUserId { get; set; }

		[Column(Name = "flow_user", StringLength = 50, IsNullable = false)]
		public string FlowUser { get; set; }

		[Column(Name = "have_reminded")]
		public bool? HaveReminded { get; set; } = false;

		[Column(Name = "is_close")]
		public bool IsClose { get; set; } = false;

		[Column(Name = "is_queshou")]
		public bool IsQueshou { get; set; } = false;

		[Column(Name = "is_send_file")]
		public bool IsSendFile { get; set; } = false;

		[Column(Name = "is_send_mail")]
		public bool IsSendMail { get; set; } = false;

		[Column(Name = "kuaidi_danhao", StringLength = 50)]
		public string KuaidiDanhao { get; set; }

		[Column(Name = "kuaidi_riqi")]
		public DateTime? KuaidiRiqi { get; set; }

		[Column(Name = "queshou_date")]
		public DateTime? QueshouDate { get; set; }

		[Column(Name = "remark", StringLength = 4000)]
		public string Remark { get; set; }

		[Column(Name = "send_mail_riqi")]
		public DateTime? SendMailRiqi { get; set; }

		[Column(Name = "update_time", InsertValueSql = "getdate()")]
		public DateTime UpdateTime { get; set; }

		[Column(Name = "update_user_id", StringLength = 50)]
		public string UpdateUserId { get; set; }

	}

}
