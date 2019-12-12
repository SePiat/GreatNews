using GreatNews.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatR_.Queries
{
    public class GetNewsAllQuery : IRequest<IEnumerable<News>>
    {
        public GetNewsAllQuery()
        {
        }
    }
}
