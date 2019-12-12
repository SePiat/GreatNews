using GreatNews.Models;
using MediatR;
using MediatR_.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_.Handlers
{
    public class AddCommentHandler: IRequestHandler<AddCommentCommand, Guid>
    {
        private readonly ApplicationContext _context;

        public AddCommentHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            await _context.Comments_.AddAsync(request._comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return request._comment.Id;
        }
    }
}
