using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflowProject.Entities.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasMany(q => q.Tags)
            .WithMany(t => t.Questions)
            .UsingEntity<QuestionTag>(qt =>
            {
                qt.HasKey(q => new { q.QuestionId, q.TagId });
                qt.ToTable("QuestionTag");
            });
    }
}