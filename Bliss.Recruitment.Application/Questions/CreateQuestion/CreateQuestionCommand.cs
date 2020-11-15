using Bliss.Recruitment.Application.Configuration.Commands;
using Bliss.Recruitment.Application.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Application.Questions.CreateQuestion
{
    public class CreateQuestionCommand : CommandBase<QuestionDto>
    {
        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public string Question { get; set; }

        public List<string> Choices { get; set; }

        public CreateQuestionCommand(string question, string imageUrl, string thumbUrl, List<string> choices)
        {
            this.Question = question;
            this.ImageUrl = imageUrl;
            this.ThumbUrl = thumbUrl;
            this.Choices = choices;
        }

    }
}
