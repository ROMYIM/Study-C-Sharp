namespace FreeSqlSample.Models;

public class CustomerFollowUserInfoDto
{
    /// <summary>
    /// 客户表示
    /// </summary>
    public string CustomerId { get; set; } = null!;

    /// <summary>
    /// 客户名称
    /// </summary>
    public string? CustomerFullName { get; set; }

    /// <summary>
    /// 是否有效
    /// </summary>
    public string? CustomerEnabled { get; set; }

    /// <summary>
    /// 案源类别
    /// </summary>
    public string? CaseSourceClass { get; set; }

    /// <summary>
    /// 案件类型
    /// </summary>
    public string CaseType { get; set; } = null!;

    /// <summary>
    /// 案件流向
    /// </summary>
    public string CaseDirection { get; set; } = null!;

    /// <summary>
    /// 用户类型
    /// 用来区分是跟案人还是案源人
    /// </summary>
    public string? CustomerUserType { get; set; }

    /// <summary>
    /// 跟案人或案源人的姓名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 案源地区
    /// </summary>
    public string? District { get; set; }

    /// <summary>
    /// 国家
    /// </summary>
    public string? Country { get; set; }

    public string? AddressProvince { get; set; }

    public string? AddressCity { get; set; }

    public string? AddressDistrict { get; set; }

    /// <summary>
    /// 客户来源
    /// </summary>
    public string? CustomerFrom { get; set; }

    /// <summary>
    /// 客户级别
    /// </summary>
    public string? CustomerLevel { get; set; }

    /// <summary>
    /// 所属行业
    /// </summary>
    public string? Industry { get; set; }

    /// <summary>
    /// 所属分所
    /// </summary>
    public string? ManageCompany { get; set; }

    /// <summary>
    /// 线索提供人
    /// </summary>
    public string? ClueUser { get; set; }

    /// <summary>
    /// 商务人员
    /// </summary>
    public string? BusinessUser { get; set; }

    /// <summary>
    /// 人员是否有效
    /// </summary>
    public bool? FollowerEnabled { get; set; }

    /// <summary>
    /// 用户是否有效
    /// </summary>
    public bool? UserEnabled { get; set; }

    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 是否境外代理
    /// </summary>
    public bool? IsCooperation { get; set; }

    /// <summary>
    /// 授权类型  0表示创建人 1表示案源人 2表示跟案人 3表示授权人
    /// </summary>
    public string? GrantType { get; set; }

    // public string CaseSourceDistrict =>
    //     District switch
    //     {
    //         "GZ" => "广州",
    //         "SZ" => "深圳",
    //         "BJ" => "北京",
    //         "KF" => "广州开发地区",
    //         "CS" => "长沙",
    //         "DG" => "东莞",
    //         "FS" => "佛山",
    //         "HZ" => "惠州",
    //         "SH" => "上海",
    //         "US-SF" => "旧金山",
    //         "ZH" => "珠海",
    //         "ZS" => "中山",
    //         "ZJ" => "杭州",
    //         "XA" => "西安",
    //         "WH" => "武汉",
    //         "NJ" => "南京",
    //         "WX" => "无锡",
    //         "TJ" => "天津",
    //         _ => string.Empty
    //     };

    public string GetCustomerLevel =>
        CustomerLevel switch
        {
            "20525D8B-E792-4074-84AD-D7B19F8DB1D0" => "一般客户",
            "6686E02E-C646-4479-9D78-7E875ADD62EC" => "潜在客户",
            "6EF268A9-7091-4FEC-BE6F-91AFF9B21CF9" => "重要客户",
            _ => string.Empty
        };

    public string GetCaseSourceClass =>
        CaseSourceClass switch
        {
            "0" => "公案",
            "1" => "个案",
            "2" => "商机案",
            _ => string.Empty
        };

    // public string CaseSourceCompany =>
    //     ManageCompany switch
    //     {
    //         "572F0C1D-E0EA-4661-800A-99C1869A3D1D" => "华进联合广州总公司",
    //         "1C0BCDD1-2BCC-4EF7-9E5A-20AC214E204D" => "华进联合深圳分公司",
    //         "2E072389-A752-4336-B850-C2C0E77D853D" => "华进联合开发区分公司",
    //         "19d8efc0-734b-49a0-8142-a2154398e9c6" => "华进联合北京分公司",
    //         "597aa3e4-d9c3-47c6-b8dc-100ccee7bb0d" => "北京华进京联",
    //         "1cb4ce97-958e-4d8a-b26f-e526455f706f" => "华进联合上海分公司",
    //         "e5138c7b-c554-42a4-819e-89ad6076dae2" => "华进联合苏州分公司",
    //         "2478e24d-4dec-4e52-b33f-668be6c78fe2" => "华进联合佛山分公司",
    //         "6ccf18a9-826d-4805-bd23-9aaf5c03122d" => "华进联合东莞分公司",
    //         "fb0bc033-9bde-4618-a682-12032b0c863c" => "华进联合惠州分公司",
    //         "d53b47b9-d26f-41b6-8b9a-2224fa8ece4a" => "华进联合武汉分公司",
    //         "b214e2ae-8816-4646-9357-d0953d28e3c3" => "华进联合长沙分公司",
    //         "740e5671-e1a5-47c2-8c79-dd0b01af4d55" => "华进京联杭州分公司",
    //         "1cacb066-936f-4b7e-8d29-d9593db29056" => "华进京联西安分公司",
    //         "48d74a8d-5812-406e-bbfe-fccf83858152" => "深圳机智联",
    //         "920d22b9-13b2-4dfe-b230-1b4e7890e2f6" => "广东智动力",
    //         "e9acb9e6-5ca8-4519-9787-cd320e4ec941" => "华进深联深圳公司",
    //         "491c20dc-874f-488f-8dab-7720455dcf53" => "广东华进（深圳）律所",
    //         "ce1a5a77-6118-4f46-8857-158ff161412d" => "Advance China IP Law Office Ltd",
    //         "85c712a7-36e6-4dc0-9d84-1ae427ba1b18" => "惠州地区其他公司",
    //         "12b59930-484a-4942-baaf-047557ad0f0d" => "北京华朗律师事务所",
    //         "71bbc91d-d6de-4cef-8749-8e7e944ff1ba" => "苏州华进公司",
    //         "a8bdddee-72b2-4883-8013-7f7033825b55" => "上海华进公司",
    //         "26398b76-b7c8-42f7-8fd3-a5ce53e39370" => "华进"
    //     }
}