using Bliss.Recruitment.Domain.Framework;
using Bliss.Recruitment.Domain.Questions;

namespace Bliss.Recruitment.Domain
{
    public class QuestionChangedEvent : DomainEventBase
    {
        private QuestionId id;

        public QuestionChangedEvent(QuestionId id)
        {
            this.id = id;
        }
    }
}