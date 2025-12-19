using Microsoft.EntityFrameworkCore;
using FileManagement.Repository.Entities;

namespace FileManagement.Repository.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<FileEntity> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.Property(u => u.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<FileEntity>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.OriginalFileName).IsRequired().HasMaxLength(255);
            entity.Property(f => f.StoredFileName).IsRequired().HasMaxLength(255);
            entity.Property(f => f.ContentType).IsRequired().HasMaxLength(100);
            
            entity.HasOne(f => f.User)
                .WithMany(u => u.Files)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
