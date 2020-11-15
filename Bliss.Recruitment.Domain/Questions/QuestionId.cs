using System;
using Bliss.Recruitment.Domain.Framework;

namespace Bliss.Recruitment.Domain.Questions
{
    public class QuestionId : TypedIdValueBase
    {
        public QuestionId(Guid value) : base(value)
        {
        }
    }
}