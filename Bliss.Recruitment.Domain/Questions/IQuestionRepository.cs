using Bliss.Recruitment.Domain.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Data.Repository
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question question);
        //Task UpdateAsync(Question question);
        Task<Question> GetQuestionAsync(QuestionId id);
        //Task<IEnumerable<Question>> GetQuestionsAsync(int offset = 0, int limit = 10, string filter = null);
    }
}