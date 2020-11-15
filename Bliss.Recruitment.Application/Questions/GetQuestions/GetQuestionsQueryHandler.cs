using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Bliss.Recruitment.Application.Configuration.Data;
using Bliss.Recruitment.Application.Configuration.Queries;
using Bliss.Recruitment.Application.Questions;
using Bliss.Recruitment.Application.Orders.GetQuestions;
using System.Collections.Generic;
using static Bliss.Recruitment.Application.Questions.QuestionDto;
using System.Linq;
using System;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails
{
    internal sealed class GetQuestionsQueryHandler : IQueryHandler<GetQuestionsQuery, List<QuestionDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetQuestionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<QuestionDto>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            string filter = string.Empty;
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                filter = @"
                AND q.[Id] IN (
		            SELECT qst.Id 
			            FROM questions.Questions qst
			            LEFT JOIN questions.Choices AS chc ON chc.[QuestionId] = qst.[Id]
		            WHERE qst.Question like '%' + @filter + '%' OR chc.Choice like '%' + @filter + '%'
	            )
                ";
            }

            string query = @$"
            SELECT
                q.[Id],
                q.[ImageUrl],
                q.[ThumbUrl],
                q.[Question],
                q.[PublishedAt]
            FROM questions.Questions AS q
            WHERE 1=1
	            {filter}
            ORDER BY q.[Id]
            OFFSET @offset ROWS 
            FETCH NEXT @limit ROWS ONLY
            ";

            var questions = await connection.QueryAsync<QuestionDto>(
                sql: query,
                param: new
                {
                    offset = request.Offset,
                    limit = request.Limit,
                    filter = request.Filter?.ToLower(),
                }
                );

            string questionsQuery = @"
            SELECT 
                c.[Id],
                c.[Votes],
                c.[Choice],
                c.[QuestionId]
            FROM questions.Choices AS c
            WHERE c.QuestionId IN @ids
            ";

            var choices = await connection.QueryAsync<ChoiceDto>(questionsQuery, new { ids = questions.Select(r => r.Id) });

            var questionsWithChoices = questions.Select(q =>
            {
                q.Choices = choices.Where(c => c.QuestionId == q.Id).ToList();
                return q;
            }).AsList();

            return questionsWithChoices;
        }

    }
}