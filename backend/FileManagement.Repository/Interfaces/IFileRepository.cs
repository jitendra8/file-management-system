using FileManagement.Repository.Entities;

namespace FileManagement.Repository.Interfaces;

public interface IFileRepository
{
    Task<FileEntity> CreateAsync(FileEntity file);
    Task<FileEntity?> GetByIdAsync(int id);
    Task<IEnumerable<FileEntity>> GetUserFilesAsync(int userId);
    Task DeleteAsync(FileEntity file);
    Task<bool> ExistsAsync(int id, int userId);
}
