using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using FileManagement.Business.DTOs.Files;
using FileManagement.Business.Interfaces;
using FileManagement.Repository.Entities;
using FileManagement.Repository.Interfaces;

namespace FileManagement.Business.Services;

public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;
    private readonly IStorageService _storageService;
    private readonly IConfiguration _configuration;
    private readonly long _maxFileSizeBytes;
    private readonly string[] _allowedExtensions;

    public FileService(
        IFileRepository fileRepository,
        IStorageService storageService,
        IConfiguration configuration)
    {
        _fileRepository = fileRepository;
        _storageService = storageService;
        _configuration = configuration;
        
        _maxFileSizeBytes = _configuration.GetValue<long>("FileUpload:MaxFileSizeMB", 10) * 1024 * 1024;
        _allowedExtensions = _configuration.GetSection("FileUpload:AllowedExtensions").Get<string[]>() 
            ?? new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".pdf", ".doc", ".docx", ".txt", ".zip", ".csv", ".xlsx" };
    }

    public async Task<FileUploadResponse> UploadFileAsync(IFormFile file, int userId)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file uploaded");

        if (file.Length > _maxFileSizeBytes)
            throw new ArgumentException($"File size exceeds maximum allowed size of {_maxFileSizeBytes / 1024 / 1024}MB");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(extension))
            throw new ArgumentException($"File type {extension} is not allowed");

        var storedFileName = $"{Guid.NewGuid()}{extension}";

        using (var stream = file.OpenReadStream())
        {
            await _storageService.SaveFileAsync(stream, storedFileName);
        }

        var fileEntity = new FileEntity
        {
            OriginalFileName = file.FileName,
            StoredFileName = storedFileName,
            ContentType = file.ContentType,
            SizeInBytes = file.Length,
            UserId = userId,
            UploadedAt = DateTime.UtcNow
        };

        var savedFile = await _fileRepository.CreateAsync(fileEntity);

        return new FileUploadResponse
        {
            Id = savedFile.Id,
            OriginalFileName = savedFile.OriginalFileName,
            SizeInBytes = savedFile.SizeInBytes,
            ContentType = savedFile.ContentType,
            UploadedAt = savedFile.UploadedAt
        };
    }

    public async Task<IEnumerable<FileInfoDto>> GetUserFilesAsync(int userId)
    {
        var files = await _fileRepository.GetUserFilesAsync(userId);
        
        return files.Select(f => new FileInfoDto
        {
            Id = f.Id,
            OriginalFileName = f.OriginalFileName,
            SizeInBytes = f.SizeInBytes,
            ContentType = f.ContentType,
            UploadedAt = f.UploadedAt
        });
    }

    public async Task<(byte[] FileContent, string ContentType, string FileName)> DownloadFileAsync(int fileId, int userId)
    {
        var file = await _fileRepository.GetByIdAsync(fileId);

        if (file == null)
            throw new FileNotFoundException("File not found");

        if (file.UserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to access this file");

        var fileContent = await _storageService.GetFileAsync(file.StoredFileName);

        return (fileContent, file.ContentType, file.OriginalFileName);
    }

    public async Task DeleteFileAsync(int fileId, int userId)
    {
        var file = await _fileRepository.GetByIdAsync(fileId);

        if (file == null)
            throw new FileNotFoundException("File not found");

        if (file.UserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to delete this file");

        await _storageService.DeleteFileAsync(file.StoredFileName);
        await _fileRepository.DeleteAsync(file);
    }
}
