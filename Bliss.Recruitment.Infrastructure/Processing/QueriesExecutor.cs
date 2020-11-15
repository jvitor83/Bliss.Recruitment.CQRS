using System.Threading.Tasks;
using Autofac;
using Bliss.Recruitment.Application.Configuration.Queries;
using MediatR;
using SampleProject.Infrastructure;

namespace Bliss.Recruitment.Infrastructure.Processing
{
    public static class QueriesExecutor
    {
        public static async Task<TResult> Execute<TResult>(IQuery<TResult> query)
        {
            using (var scope = CompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}