using System;
using System.Reflection;
using System.Threading.Tasks;
using Bliss.Recruitment.Application.Questions.CreateQuestion;
using Bliss.Recruitment.Infrastructure.Database;
using Bliss.Recruitment.Infrastructure.Processing;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bliss.Recruitment.Infrastructure.Processing.InternalCommands
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly RecruitmentContext _recruitmentContext;

        public CommandsDispatcher(
            IMediator mediator, 
            RecruitmentContext recruitmentContext)
        {
            this._mediator = mediator;
            this._recruitmentContext = recruitmentContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            var internalCommand = await this._recruitmentContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(CreateQuestionCommand)).GetType(internalCommand.Type);
            dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

            internalCommand.ProcessedDate = DateTime.UtcNow;

            await this._mediator.Send(command);
        }
    }
}