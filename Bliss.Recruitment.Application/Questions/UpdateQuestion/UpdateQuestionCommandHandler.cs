using Bliss.Recruitment.Data.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bliss.Recruitment.Application.Questions;
using Bliss.Recruitment.Domain.Framework;
using Bliss.Recruitment.Domain.Questions;

namespace Bliss.Recruitment.Application.Questions.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, QuestionDto>
    {
        protected readonly IQuestionRepository _questionRepository;

        protected readonly IUnitOfWork _unitOfWork;
        public UpdateQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
        {
            this._questionRepository = questionRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<QuestionDto> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await this._questionRepository.GetQuestionAsync(new QuestionId(request.QuestionId));

            question.ChangeQuestion(request.Question);
            question.ChangeImageUrl(request.ImageUrl);
            question.ChangeThumbUrl(request.ThumbUrl);
            question.ChangeChoices(request.Choices);

            await this._unitOfWork.CommitAsync(cancellationToken);

            return new QuestionDto { Id = question.Id.Value };
        }
    }

}
