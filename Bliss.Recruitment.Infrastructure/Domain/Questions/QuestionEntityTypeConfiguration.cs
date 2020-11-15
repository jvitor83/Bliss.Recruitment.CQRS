using System;
using Bliss.Recruitment.Domain.Questions;
using Bliss.Recruitment.Domain.Questions.Choices;
using Bliss.Recruitment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bliss.Recruitment.Infrastructure.Domain.Customers
{
    internal sealed class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {

        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions", SchemaNames.Questions);
            
            builder.HasKey(b => b.Id);

            builder.Property("_question").HasColumnName("Question");
            builder.Property("_imageUrl").HasColumnName("ImageUrl");
            builder.Property("_thumbUrl").HasColumnName("ThumbUrl");
            builder.Property("_publishedAt").HasColumnName("PublishedAt");

            builder.OwnsMany<Choice>("_choices", x =>
            {
                x.WithOwner().HasForeignKey("QuestionId");

                x.ToTable("Choices", SchemaNames.Questions);
                
                x.Property("_choice").HasColumnName("Choice");
                x.Property("_votes").HasColumnName("Votes");
                x.Property<ChoiceId>("Id");
                x.HasKey("Id");

            });
        }
    }
}