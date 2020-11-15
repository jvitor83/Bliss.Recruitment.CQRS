using Bliss.Recruitment.Domain.Questions;
using Bliss.Recruitment.Domain.Questions.Choices;
using Bliss.Recruitment.Infrastructure.Processing.InternalCommands;
using Bliss.Recruitment.Infrastructure.Processing.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bliss.Recruitment.Infrastructure.Database
{
    public class RecruitmentContext : DbContext
    {

        public RecruitmentContext(DbContextOptions<RecruitmentContext> options)
              :base(options)
        {
        }
  
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecruitmentContext).Assembly);
        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }
    }
}
