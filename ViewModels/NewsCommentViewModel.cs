using GreatNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatNews.ViewModels
{
    public class NewsCommentViewModel
    {
        public News News { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
