using System.Threading.Tasks;
using Bliss.Recruitment.Application.Configuration.Commands;
using MediatR;

namespace Bliss.Recruitment.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}