using GreatNews.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatR_.Commands
{
    public class AddCommentCommand : IRequest<Guid>
    {
        public Comment _comment { get; }

        public AddCommentCommand(Comment comment)
        {
            _comment = comment;
        }

              
    }
}

