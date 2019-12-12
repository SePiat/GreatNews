using MediatR;
using System;

namespace MediatR_.Commands
{
    public class DeleteNewsByIdCommand : IRequest<string>
    {
        public Guid Id { get; }
        public DeleteNewsByIdCommand(Guid id)
        {
            Id = id;
        }


    }
}
