using _06._Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _06._Web_API.Data;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
    {
    }
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Project
        modelBuilder.Entity<Project>(project =>
        {
            project.HasKey(p => p.Id);
            project.Property(p => p.Name).IsRequired().HasMaxLength(200);
            project.Property(p => p.Description).IsRequired().HasMaxLength(1000);
            project.Property(p => p.CreatedAt).IsRequired();
        });

        // TaskItem
        modelBuilder.Entity<TaskItem>(task =>
        {
            task.HasKey(t => t.Id);
            task.Property(t => t.Title).IsRequired().HasMaxLength(200);
            task.Property(t => t.Description).IsRequired().HasMaxLength(1000);
            task.Property(t => t.Status).IsRequired();
            task.Property(t => t.Priority).IsRequired();
            task.Property(t => t.CreatedAt).IsRequired();

            // Relationship
            task.HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
