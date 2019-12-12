using GreatNews.Models;
using MediatR;
using MediatR_.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_.Handlers
{
    public class GetNewsByIdHandler : IRequestHandler<GetNewsByIdQuery, News>
    {
        private readonly ApplicationContext _context;

        public GetNewsByIdHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<News> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var result =await _context.News_.FirstOrDefaultAsync(x=>x.Id.Equals(request.Id), cancellationToken);

            return result;
        }
    }
}
