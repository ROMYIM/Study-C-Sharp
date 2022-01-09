using System.Text.Json.Serialization;

namespace AjaxTest.Models;

public class ResponseResult<T>
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = "操作成功";
}