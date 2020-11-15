using System.Threading.Tasks;
using Bliss.Recruitment.Infrastructure.Processing;
using Bliss.Recruitment.Infrastructure.Processing.InternalCommands;
using Quartz;

namespace Bliss.Recruitment.Infrastructure.Processing.InternalCommands
{
    [DisallowConcurrentExecution]
    public class ProcessInternalCommandsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInternalCommandsCommand());
        }
    }
}