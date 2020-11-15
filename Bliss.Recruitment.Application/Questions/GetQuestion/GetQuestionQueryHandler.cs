using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Bliss.Recruitment.Application.Configuration.Data;
using Bliss.Recruitment.Application.Configuration.Queries;
using Bliss.Recruitment.Application.Questions;
using Bliss.Recruitment.Application.Orders.GetQuestions;
using System.Collections.Generic;
using Bliss.Recruitment.Application;
using static Bliss.Recruitment.Application.Questions.QuestionDto;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails
{
    internal sealed class GetQuestionQueryHandler : IQueryHandler<GetQuestionQuery, QuestionDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetQuestionQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<QuestionDto> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            string queryQuestion = @$"
                SELECT 
                    [Question].[Id],
                    [Question].[ImageUrl],
                    [Question].[ThumbUrl],
                    [Question].[Question],
                    [Question].[PublishedAt]
                FROM questions.Questions AS [Question]
                WHERE [Question].[Id] = @QuestionId
            ";

            var question = await connection.QuerySingleOrDefaultAsync<QuestionDto>(
                queryQuestion,
                new {
                    QuestionId = request.Id,
                }
                );

            string queryChoices = @$"
                SELECT 
                    [Choice].[Id],
                    [Choice].[Votes],
                    [Choice].[Choice],
                    [Choice].[QuestionId]
                FROM questions.Choices AS [Choice]
                WHERE [Choice].[QuestionId] = @QuestionId
            ";

            if (question == null)
            {
                return null;
            }
            
            var choices = await connection.QueryAsync<ChoiceDto>(
                queryChoices,
                new
                {
                    QuestionId = question.Id,
                }
                );


            question.Choices = choices.AsList();

            return question;
        }
    }
}