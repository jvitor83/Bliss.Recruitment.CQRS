using Bliss.Recruitment.Domain.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bliss.Recruitment.Domain.Questions.Choices
{
    public class Choice : Entity
    {
        internal ChoiceId Id;

        private string _choice;

        private int _votes;

        private Choice()
        {

        }

        private Choice(string choice)
        {
            this.Id = new ChoiceId(Guid.NewGuid());
            this._choice = choice;
        }

        internal static Choice CreateForQuestion(string choice)
        {
            return new Choice(choice);
        }
    }
}
