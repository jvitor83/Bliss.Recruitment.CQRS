using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bliss.Recruitment.Api
{
    public class GetQuestionsWithParamsRequestModel : IRequest<GetQuestionsWithParamsResponseModel>
    {
        public int Limit { get; set; }


        public int Offset { get; set; }


        public string Filter { get; set; }


    }
}