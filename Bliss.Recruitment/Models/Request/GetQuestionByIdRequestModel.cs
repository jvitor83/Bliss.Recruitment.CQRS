using Bliss.Recruitment.Domain.Questions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bliss.Recruitment.Api
{
    public class GetQuestionByIdRequestModel : IRequest<GetQuestionByIdResponseModel>
    {
        public QuestionId Id { get; set; }
    }
}