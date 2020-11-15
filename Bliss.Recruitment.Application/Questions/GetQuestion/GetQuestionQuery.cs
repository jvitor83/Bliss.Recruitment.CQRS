using Bliss.Recruitment.Application.Configuration.Queries;
using Bliss.Recruitment.Application.Questions;
using MediatR;
using System;

namespace Bliss.Recruitment.Application
{
    public class GetQuestionQuery : IQuery<QuestionDto>, IGetQuestionQuery
    {
        public Guid Id { get; }

        public GetQuestionQuery(Guid id)
        {
            this.Id = id;
        }
    }
}