using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AjaxTest.Models;
using NPOI.HWPF;

namespace AjaxTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Word()
    {
        var path = @"I:\yim\Documents\工作交接-2021-05\OA中专路由.doc";
        await using var fileStream = System.IO.File.OpenRead(path);
        var doc = new HWPFDocument(fileStream);
        doc.Text.AppendLine("新增内容").AppendLine(DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        await using var memoryStream = new MemoryStream();
        doc.Write(memoryStream);
        var fileData = memoryStream.ToArray();
        return Json(new ResponseResult<FileDownloadDto>()
        {
            Success = true,
            Data = new FileDownloadDto()
            {
                FileData = Convert.ToBase64String(fileData),
                FileName = fileStream.Name
            }
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}