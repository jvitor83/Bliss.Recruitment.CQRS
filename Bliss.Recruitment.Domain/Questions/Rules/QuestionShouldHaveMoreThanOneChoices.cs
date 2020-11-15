using Bliss.Recruitment.Domain.Framework;
using System.Collections.Generic;

namespace Bliss.Recruitment.Domain.Questions
{
    internal class QuestionShouldHaveMoreThanOneChoices : IBusinessRule
    {
        private readonly List<string> _choices;

        public QuestionShouldHaveMoreThanOneChoices(List<string> choices)
        {
            this._choices = choices;
        }

        public bool IsBroken()
        {
            return _choices.Count <= 1;
        }

        public string Message => "Question should have at least 2 choices.";
        
    }
}