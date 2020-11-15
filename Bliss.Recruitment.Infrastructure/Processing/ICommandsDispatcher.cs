using System;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Infrastructure.Processing
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
