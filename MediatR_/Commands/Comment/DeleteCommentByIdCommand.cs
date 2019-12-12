using MediatR;
using System;

namespace MediatR_.Commands
{
    public class DeleteCommentByIdCommand : IRequest<string>
    {
        public Guid Id { get; }
        public DeleteCommentByIdCommand(Guid id)
        {
            Id = id;
        }


    }
}
