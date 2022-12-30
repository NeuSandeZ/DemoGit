using Microsoft.EntityFrameworkCore;
using StackOverflowProject.Entities;

namespace StackOverflowProject;

public class StackOverflowContext : DbContext
{
    public StackOverflowContext(DbContextOptions<StackOverflowContext> options) : base(options)
    {
    }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<QuestionTag> QuestionTags { get; set; }

    public DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}