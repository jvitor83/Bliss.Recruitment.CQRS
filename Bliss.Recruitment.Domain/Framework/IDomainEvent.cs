using System;
using MediatR;

namespace Bliss.Recruitment.Domain.Framework
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}