using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FileManagement.Business.DTOs.Files;
using FileManagement.Business.Interfaces;

namespace FileManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Invalid user token");
        }
        return userId;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<FileUploadResponse>> Upload([FromForm] IFormFile file)
    {
        var userId = GetUserId();
        var response = await _fileService.UploadFileAsync(file, userId);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FileInfoDto>>> GetFiles()
    {
        var userId = GetUserId();
        var files = await _fileService.GetUserFilesAsync(userId);
        return Ok(files);
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(int id)
    {
        var userId = GetUserId();
        var (fileContent, contentType, fileName) = await _fileService.DownloadFileAsync(id, userId);
        return File(fileContent, contentType, fileName);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();
        await _fileService.DeleteFileAsync(id, userId);
        return NoContent();
    }
}
