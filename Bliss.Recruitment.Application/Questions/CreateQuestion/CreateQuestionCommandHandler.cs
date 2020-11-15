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

namespace Bliss.Recruitment.Application.Questions.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionDto>
    {
        protected readonly IQuestionRepository _questionRepository;
        
        protected readonly IUnitOfWork _unitOfWork;
        public CreateQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
        {
            this._questionRepository = questionRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<QuestionDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            
            Question question = Question.CreateQuestion(request.Question, request.ImageUrl, request.ThumbUrl, request.Choices);

            await this._questionRepository.AddAsync(question);

            await this._unitOfWork.CommitAsync(cancellationToken);

            return new QuestionDto { Id = question.Id.Value };
        }
    }

}
