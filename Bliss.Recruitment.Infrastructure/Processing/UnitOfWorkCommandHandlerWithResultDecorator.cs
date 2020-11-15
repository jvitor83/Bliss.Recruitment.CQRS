using System;
using System.Threading;
using System.Threading.Tasks;
using Bliss.Recruitment.Application.Configuration.Commands;
using Bliss.Recruitment.Domain.Framework;
using Bliss.Recruitment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bliss.Recruitment.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;

        private readonly IUnitOfWork _unitOfWork;

        private readonly RecruitmentContext _recruitmentContext;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            ICommandHandler<T, TResult> decorated, 
            IUnitOfWork unitOfWork,
            RecruitmentContext recruitmentContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _recruitmentContext = recruitmentContext;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await this._decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase<TResult>)
            {
                var internalCommand = await _recruitmentContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await this._unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}