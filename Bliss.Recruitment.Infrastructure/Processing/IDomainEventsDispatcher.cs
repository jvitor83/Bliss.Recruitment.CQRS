using System.Threading.Tasks;

namespace Bliss.Recruitment.Infrastructure.Processing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}