using GreatNews.Models;
using MediatR;
using System;
using System.Collections.Generic;


namespace MediatR_.Queries
{
    public class GetCommentsByNewsIdQuery : IRequest<IEnumerable<Comment>>
    {
        public Guid Id { get; }
        public GetCommentsByNewsIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
