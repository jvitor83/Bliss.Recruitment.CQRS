using Bliss.Recruitment.Domain.Questions;
using Bliss.Recruitment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Data.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly RecruitmentContext _context;

        public QuestionRepository(RecruitmentContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(Question question)
        {
            await this._context.Questions.AddAsync(question);

        }

        public async Task<Question> GetQuestionAsync(QuestionId id)
        {
            IQueryable<Question> questionsQueryable = this._context.Questions;
            IQueryable<Question> questionsFilteredQueryable = questionsQueryable.Where(r => r.Id == id);
            Question questionsSingleOrDefaultQueryable = await questionsFilteredQueryable.SingleOrDefaultAsync();
            return questionsSingleOrDefaultQueryable;
        }

    }
}
