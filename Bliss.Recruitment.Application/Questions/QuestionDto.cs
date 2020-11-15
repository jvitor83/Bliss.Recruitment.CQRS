using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Bliss.Recruitment.Application.Questions
{
    public class QuestionDto
    {

        public QuestionDto()
        {
            this.Choices = new List<ChoiceDto>();
        }

        public Guid Id { get; set; }


        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("thumb_url")]
        public string ThumbUrl { get; set; }

        public string Question { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        public List<ChoiceDto> Choices { get; set; }
        public class ChoiceDto
        {
            [JsonIgnore]
            public Guid Id { get; set; }

            [JsonIgnore]
            public Guid QuestionId { get; set; }

            public string Choice { get; set; }
            public int Votes { get; set; }
        }
    }
}
