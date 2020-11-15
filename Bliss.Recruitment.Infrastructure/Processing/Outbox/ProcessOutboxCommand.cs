using MediatR;
using Bliss.Recruitment.Application;
using Bliss.Recruitment.Application.Configuration.Commands;

namespace Bliss.Recruitment.Infrastructure.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}