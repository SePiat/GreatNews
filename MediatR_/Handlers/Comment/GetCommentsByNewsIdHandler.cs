using GreatNews.Models;
using MediatR;
using MediatR_.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_.Handlers
{
    public class GetCommentsByNewsIdHandler : IRequestHandler<GetCommentsByNewsIdQuery, IEnumerable<Comment>>
    {
        private readonly ApplicationContext _context;
        public GetCommentsByNewsIdHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> Handle(GetCommentsByNewsIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Comments_.Where(x => x.NewsId == request.Id).ToListAsync();

            return result;
        }
    }
}
