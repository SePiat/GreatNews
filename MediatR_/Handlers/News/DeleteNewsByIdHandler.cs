using GreatNews.Models;
using MediatR;
using MediatR_.Commands;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_.Handlers
{
    public class DeleteNewsByIdHandler : IRequestHandler<DeleteNewsByIdCommand, string>
    {

        private readonly ApplicationContext _context;

        public DeleteNewsByIdHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteNewsByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.News_.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
            _context.Remove(result);
            await _context.SaveChangesAsync(cancellationToken);

            return result.Id.ToString();
        }
    }
}
