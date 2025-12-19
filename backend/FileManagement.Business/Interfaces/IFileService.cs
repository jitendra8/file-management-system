using Microsoft.AspNetCore.Http;
using FileManagement.Business.DTOs.Files;

namespace FileManagement.Business.Interfaces;

public interface IFileService
{
    Task<FileUploadResponse> UploadFileAsync(IFormFile file, int userId);
    Task<IEnumerable<FileInfoDto>> GetUserFilesAsync(int userId);
    Task<(byte[] FileContent, string ContentType, string FileName)> DownloadFileAsync(int fileId, int userId);
    Task DeleteFileAsync(int fileId, int userId);
}
