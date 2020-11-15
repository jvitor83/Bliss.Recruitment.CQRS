using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bliss.Recruitment.Api.Models.Response
{
    public class CreateQuestionResponseModel
    {
        public int Id { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("thumb_url")]
        public string ThumbUrl { get; set; }

        public string Question { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        public ICollection<ChoiceCreateQuestionRequestModel> Choices { get; set; }

        public class ChoiceCreateQuestionRequestModel
        {
            public string Choice { get; set; }
            public int Votes { get; set; }
        }

    }

}
