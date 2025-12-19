namespace FileManagement.Business.DTOs.Files;

public class FileInfoDto
{
    public int Id { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}
