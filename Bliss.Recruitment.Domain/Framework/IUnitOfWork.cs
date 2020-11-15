using System.Threading;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Domain.Framework
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}