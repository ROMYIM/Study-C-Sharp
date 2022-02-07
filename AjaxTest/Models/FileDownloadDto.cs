using System.Text.Json.Serialization;

namespace AjaxTest.Models;

public class FileDownloadDto
{
    [JsonPropertyName("filename")]
    public string FileName { get; set; } = null!;

    [JsonPropertyName("file")]
    public string? FileData { get; set; }
}