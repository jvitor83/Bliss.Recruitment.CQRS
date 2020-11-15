using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bliss.Recruitment.Application.Questions.CreateQuestion
{
    public class CreateQuestionRequestModel
    {


        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("thumb_url")]
        public string ThumbUrl { get; set; }

        public string Question { get; set; }

        public List<string> Choices { get; set; }
    }

}
