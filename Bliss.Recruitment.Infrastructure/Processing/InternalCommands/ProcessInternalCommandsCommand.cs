using Bliss.Recruitment.Application.Configuration.Commands;
using Bliss.Recruitment.Infrastructure.Processing.Outbox;
using MediatR;

namespace Bliss.Recruitment.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}