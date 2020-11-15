using System.Reflection;
using Bliss.Recruitment.Application.Questions.CreateQuestion;

namespace Bliss.Recruitment.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CreateQuestionCommand).Assembly;
    }
}