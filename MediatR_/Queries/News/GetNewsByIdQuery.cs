using GreatNews.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatR_.Queries
{
    public class GetNewsByIdQuery:IRequest<News>
    {
        public Guid Id { get; }
        public GetNewsByIdQuery(Guid id)
        {
            Id = id;
        }

       
    }
}
