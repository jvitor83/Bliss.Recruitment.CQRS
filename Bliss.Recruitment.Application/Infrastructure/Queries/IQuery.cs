﻿using MediatR;

namespace Bliss.Recruitment.Application.Configuration.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}