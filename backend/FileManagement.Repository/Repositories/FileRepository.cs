using Microsoft.EntityFrameworkCore;
using FileManagement.Repository.Data;
using FileManagement.Repository.Entities;
using FileManagement.Repository.Interfaces;

namespace FileManagement.Repository.Repositories;

public class FileRepository : IFileRepository
{
    private readonly ApplicationDbContext _context;

    public FileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FileEntity> CreateAsync(FileEntity file)
    {
        _context.Files.Add(file);
        await _context.SaveChangesAsync();
        return file;
    }

    public async Task<FileEntity?> GetByIdAsync(int id)
    {
        return await _context.Files
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<FileEntity>> GetUserFilesAsync(int userId)
    {
        return await _context.Files
            .Where(f => f.UserId == userId)
            .OrderByDescending(f => f.UploadedAt)
            .ToListAsync();
    }

    public async Task DeleteAsync(FileEntity file)
    {
        _context.Files.Remove(file);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id, int userId)
    {
        return await _context.Files.AnyAsync(f => f.Id == id && f.UserId == userId);
    }
}
