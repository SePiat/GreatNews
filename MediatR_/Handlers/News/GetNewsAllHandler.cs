using GreatNews.Models;
using MediatR;
using MediatR_.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_.Handlers
{
    public class GetNewsAllHandler : IRequestHandler<GetNewsAllQuery, IEnumerable<News>>
    {
        private readonly ApplicationContext _context;

        public GetNewsAllHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> Handle(GetNewsAllQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.News_.ToListAsync<News>();
            return result;
        }
    }
}
