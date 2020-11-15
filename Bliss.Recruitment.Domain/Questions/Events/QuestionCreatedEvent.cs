using Bliss.Recruitment.Domain.Framework;

namespace Bliss.Recruitment.Domain.Questions
{
    public class QuestionCreatedEvent : DomainEventBase
    {
        public QuestionId QuestionId { get; }

        public QuestionCreatedEvent(QuestionId questionId)
        {
            this.QuestionId = questionId;
        }
    }
}