using Bliss.Recruitment.Domain.Framework;
using Bliss.Recruitment.Infrastructure.Database;
using Bliss.Recruitment.Infrastructure.Processing;
using System.Threading;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecruitmentContext _recruitmentContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            RecruitmentContext recruitmentContext, 
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            this._recruitmentContext = recruitmentContext;
            this._domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._domainEventsDispatcher.DispatchEventsAsync();
            var result = await this._recruitmentContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}