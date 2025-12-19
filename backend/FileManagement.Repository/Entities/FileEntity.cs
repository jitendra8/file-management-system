namespace FileManagement.Repository.Entities;

public class FileEntity
{
    public int Id { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    
    public User User { get; set; } = null!;
}
