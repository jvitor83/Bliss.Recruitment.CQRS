namespace Bliss.Recruitment.Domain.Framework
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}