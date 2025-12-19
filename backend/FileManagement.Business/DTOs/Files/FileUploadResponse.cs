namespace FileManagement.Business.DTOs.Files;

public class FileUploadResponse
{
    public int Id { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}
