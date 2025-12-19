namespace FileManagement.Business.Interfaces;

public interface IStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName);
    Task<byte[]> GetFileAsync(string storedFileName);
    Task DeleteFileAsync(string storedFileName);
}
