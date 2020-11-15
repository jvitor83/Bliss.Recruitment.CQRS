using System;
using Bliss.Recruitment.Domain.Framework;

namespace Bliss.Recruitment.Domain.Questions
{
    public class ChoiceId : TypedIdValueBase
    {
        public ChoiceId(Guid value) : base(value)
        {
        }
    }
}