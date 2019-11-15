using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.UoW;
using GreatNews.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GreatNews.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(Guid id)
        {
            var _news = _unitOfWork.News_.AsQueryable();
            var news = _news.Where(p => p.Id.Equals(id)).FirstOrDefault();

            var commentModel = _unitOfWork.Comments.AsQueryable();
           /* var commentModel = await _unitOfWork.Comments.Include("Author").Include("News").ToListAsync();*/
            var comments = commentModel.Where(i =>i.NewsId.Equals(id)).OrderByDescending(o => o.DatePub);

            var newsModel = new NewsCommentViewModel()
            {
                News = news,
                Comments = comments
            };

            return View(newsModel);
        }
    }
}