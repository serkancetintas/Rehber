﻿using System.Threading.Tasks;

namespace Setur.Services.Contact.Application.Queries
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
