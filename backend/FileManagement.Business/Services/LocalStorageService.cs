using Microsoft.Extensions.Configuration;
using FileManagement.Business.Interfaces;

namespace FileManagement.Business.Services;

public class LocalStorageService : IStorageService
{
    private readonly string _uploadPath;

    public LocalStorageService(IConfiguration configuration)
    {
        _uploadPath = configuration["FileStorage:UploadPath"] ?? "uploads";
        
        if (!Directory.Exists(_uploadPath))
        {
            Directory.CreateDirectory(_uploadPath);
        }
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
    {
        var filePath = Path.Combine(_uploadPath, fileName);
        
        using (var fileStreamOut = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(fileStreamOut);
        }

        return fileName;
    }

    public async Task<byte[]> GetFileAsync(string storedFileName)
    {
        var filePath = Path.Combine(_uploadPath, storedFileName);
        
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found");

        return await File.ReadAllBytesAsync(filePath);
    }

    public Task DeleteFileAsync(string storedFileName)
    {
        var filePath = Path.Combine(_uploadPath, storedFileName);
        
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }
}
