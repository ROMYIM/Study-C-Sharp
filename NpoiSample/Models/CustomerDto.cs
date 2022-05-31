namespace NpoiSample.Models;

public class CustomerDto
{
    public string CustomerId { get; set; }

    public string CustomerName { get; set; }

    public bool IsEnabled { get; set; }

    public string EnabledCustomer => IsEnabled ? "是" : "否";

    #region 专利

    /// <summary>
    /// 内内专利案源类别
    /// </summary>
    public string? IiPatentClass { get; set; }

    /// <summary>
    /// 内内专利案源人
    /// </summary>
    public string? IiPatentCaseSourceUser { get; set; }

    /// <summary>
    /// 内内专利跟案人
    /// </summary>
    public string? IiPatentFollowUser { get; set; }
    
    /// <summary>
    /// 内外专利案源类别
    /// </summary>
    public string? IoPatentClass { get; set; }

    /// <summary>
    /// 内外专利案源人
    /// </summary>
    public string? IoPatentCaseSourceUser { get; set; }

    /// <summary>
    /// 内外专利跟案人
    /// </summary>
    public string? IoPatentFollowUser { get; set; }
    
    /// <summary>
    /// 外内专利案源类别
    /// </summary>
    public string? OiPatentClass { get; set; }

    /// <summary>
    /// 外内专利案源人
    /// </summary>
    public string? OiPatentCaseSourceUser { get; set; }

    /// <summary>
    /// 外内专利跟案人
    /// </summary>
    public string? OiPatentFollowUser { get; set; }
    
    /// <summary>
    /// 外外专利案源类别
    /// </summary>
    public string? OoPatentClass { get; set; }

    /// <summary>
    /// 外外专利案源人
    /// </summary>
    public string? OoPatentCaseSourceUser { get; set; }

    /// <summary>
    /// 外外专利跟案人
    /// </summary>
    public string? OoPatentFollowUser { get; set; }

    #endregion

    #region 商标

    /// <summary>
    /// 内内专利案源类别
    /// </summary>
    public string? IiTrademarkClass { get; set; }

    /// <summary>
    /// 内内专利案源人
    /// </summary>
    public string? IiTrademarkCaseSourceUser { get; set; }

    /// <summary>
    /// 内内专利跟案人
    /// </summary>
    public string? IiTrademarkFollowUser { get; set; }
    
    /// <summary>
    /// 内外专利案源类别
    /// </summary>
    public string? IoTrademarkClass { get; set; }

    /// <summary>
    /// 内外专利案源人
    /// </summary>
    public string? IoTrademarkCaseSourceUser { get; set; }

    /// <summary>
    /// 内外专利跟案人
    /// </summary>
    public string? IoTrademarkFollowUser { get; set; }
    
    /// <summary>
    /// 外内专利案源类别
    /// </summary>
    public string? OiTrademarkClass { get; set; }

    /// <summary>
    /// 外内专利案源人
    /// </summary>
    public string? OiTrademarkCaseSourceUser { get; set; }

    /// <summary>
    /// 外内专利跟案人
    /// </summary>
    public string? OiTrademarkFollowUser { get; set; }
    
    /// <summary>
    /// 外外专利案源类别
    /// </summary>
    public string? OoTrademarkClass { get; set; }

    /// <summary>
    /// 外外专利案源人
    /// </summary>
    public string? OoTrademarkCaseSourceUser { get; set; }

    /// <summary>
    /// 外外专利跟案人
    /// </summary>
    public string? OoTrademarkFollowUser { get; set; }

    #endregion
    
    #region 项目

    /// <summary>
    /// 内内专利案源类别
    /// </summary>
    public string? IiProjectClass { get; set; }

    /// <summary>
    /// 内内专利案源人
    /// </summary>
    public string? IiProjectCaseSourceUser { get; set; }

    /// <summary>
    /// 内内专利跟案人
    /// </summary>
    public string? IiProjectFollowUser { get; set; }
    
    /// <summary>
    /// 内外专利案源类别
    /// </summary>
    public string? IoProjectClass { get; set; }

    /// <summary>
    /// 内外专利案源人
    /// </summary>
    public string? IoProjectCaseSourceUser { get; set; }

    /// <summary>
    /// 内外专利跟案人
    /// </summary>
    public string? IoProjectFollowUser { get; set; }
    
    /// <summary>
    /// 外内专利案源类别
    /// </summary>
    public string? OiProjectClass { get; set; }

    /// <summary>
    /// 外内专利案源人
    /// </summary>
    public string? OiProjectCaseSourceUser { get; set; }

    /// <summary>
    /// 外内专利跟案人
    /// </summary>
    public string? OiProjectFollowUser { get; set; }
    
    /// <summary>
    /// 外外专利案源类别
    /// </summary>
    public string? OoProjectClass { get; set; }

    /// <summary>
    /// 外外专利案源人
    /// </summary>
    public string? OoProjectCaseSourceUser { get; set; }

    /// <summary>
    /// 外外专利跟案人
    /// </summary>
    public string? OoProjectFollowUser { get; set; }

    #endregion
}