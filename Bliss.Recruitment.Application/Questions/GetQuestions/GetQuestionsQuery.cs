using System;
using System.Collections.Generic;
using MediatR;
using Bliss.Recruitment.Application.Configuration.Queries;
using Bliss.Recruitment.Application.Questions;

namespace Bliss.Recruitment.Application.Orders.GetQuestions
{
    public class GetQuestionsQuery : IQuery<List<QuestionDto>>
    {
        public int Offset { get; }
        public int Limit { get; }
        public string Filter { get; }

        public GetQuestionsQuery(int offset, int limit, string filter)
        {
            this.Offset = offset;
            this.Limit = limit;
            this.Filter = filter;
        }
    }
}