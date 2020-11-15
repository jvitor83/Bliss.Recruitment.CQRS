using Bliss.Recruitment.Application.Configuration.DomainEvents;
using Bliss.Recruitment.Domain.Questions;


namespace SampleProject.Application.Customers.IntegrationHandlers
{
    public class QuestionCreatedNotification : DomainNotificationBase<QuestionCreatedEvent>
    {
        public QuestionId QuestionId { get; }

        public QuestionCreatedNotification(QuestionCreatedEvent domainEvent) : base(domainEvent)
        {
            this.QuestionId = domainEvent.QuestionId;
        }

        // [JsonConstructor]
        public QuestionCreatedNotification(QuestionId questionId) : base(null)
        {
            this.QuestionId = questionId;
        }
    }
}