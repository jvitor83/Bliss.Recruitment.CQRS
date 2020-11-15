using System;
using System.Collections.Generic;
using System.Linq;
using Bliss.Recruitment.Domain.Framework;
using Bliss.Recruitment.Domain.Questions.Choices;

namespace Bliss.Recruitment.Domain.Questions
{
    public class Question : Entity, IAggregateRoot
    {
        public QuestionId Id { get; private set; }

        private string _question;

        private string _imageUrl { get; set; }

        private DateTime _publishedAt { get; set; }

        private string _thumbUrl { get; set; }

        private List<Choice> _choices;


        private Question()
        {
            this._choices = new List<Choice>();
        }
         
        private Question(string question, string imageUrl, string thumbUrl, List<string> choices)
        {
            CheckRule(new QuestionShouldHaveMoreThanOneChoices(choices));

            this.Id = new QuestionId(Guid.NewGuid());
            _question = question;
            _imageUrl = imageUrl;
            _thumbUrl = thumbUrl;
            _publishedAt = DateTimeSystem.Now;
            
            _choices = choices.Select(r => Choice.CreateForQuestion(r)).ToList();

            this.AddDomainEvent(new QuestionCreatedEvent(this.Id));
        }

        public static Question CreateQuestion(string question, string imageUrl, string thumbUrl, List<string> choices)
        {

            return new Question(question, imageUrl, thumbUrl, choices);
        }

        public void ChangeQuestion(string question)
        {
            this._question = question;
            _publishedAt = DateTimeSystem.Now;
            this.AddDomainEvent(new QuestionChangedEvent(this.Id));
        }

        public void ChangeImageUrl(string imageUrl)
        {
            this._imageUrl = imageUrl;
            _publishedAt = DateTimeSystem.Now;
            this.AddDomainEvent(new QuestionChangedEvent(this.Id));
        }

        public void ChangeThumbUrl(string thumbUrl)
        {
            this._thumbUrl = thumbUrl;
            _publishedAt = DateTimeSystem.Now;
            this.AddDomainEvent(new QuestionChangedEvent(this.Id));
        }

        public void ChangeChoices(List<string> choices)
        {
            CheckRule(new QuestionShouldHaveMoreThanOneChoices(choices));

            var choicesDomain = choices.Select(r => Choice.CreateForQuestion(r));

            this._choices = choicesDomain.ToList();
            _publishedAt = DateTimeSystem.Now;
            this.AddDomainEvent(new QuestionChangedEvent(this.Id));
        }

    }
}