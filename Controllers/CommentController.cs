using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Good_news_Blog.Models;
using GreatNews.Models;
using GreatNews.UoW;
using GreatNews.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreatNews.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserIdent> _userManager;


        public CommentController(IUnitOfWork uow, UserManager<UserIdent> userManager)
        {
            _unitOfWork = uow;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(Guid id)
        {
        
            var News__ = _unitOfWork.News_.AsQueryable();
            var Comments_ = _unitOfWork.Comments.AsQueryable();

            var news = News__.Where(p => p.Id.Equals(id)).FirstOrDefault();
            var commentModel = await Comments_.Include("UsersId").Include("News").ToListAsync();
            var comments = commentModel.Where(i => i.News.Id.Equals(id)).OrderBy(o => o.DatePub);

            var newsModel = new NewsCommentViewModel()
            {
                News = news,
                Comments = comments
            };

            return View(newsModel);
        }

      




        [HttpPost]
        public async Task<IActionResult> SendComment([FromBody] CommentModel model)
        {
            var News = _unitOfWork.News_.AsQueryable();

            var author = _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            var comment = new Comment()
            {
                UsersId = author,
                TextComment = model.Text,
                DatePub = DateTime.SpecifyKind(
                    DateTime.UtcNow,
                    DateTimeKind.Utc),
                News = News.Where(i => i.Id.Equals(model.Id)).FirstOrDefault()
            };

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();

            return Json(comment);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment([FromBody] Guid id)
        {
            _unitOfWork.Comments.Delete(id);
            await _unitOfWork.SaveAsync();

            return Ok();
        }

        //[OverrideAuthorization] для Web API
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> CommPart(Guid id)
        {
            var News__ = _unitOfWork.News_.AsQueryable();
            var Comments_ = _unitOfWork.Comments.AsQueryable();

            var news = News__.Where(p => p.Id.Equals(id)).FirstOrDefault();
            var commentModel = await Comments_.Include("UsersId").Include("News").ToListAsync();
            var comments = commentModel.Where(i => i.News.Id.Equals(id)).OrderBy(o => o.DatePub);

            var newsModel = new NewsCommentViewModel()
            {
                News = news,
                Comments = comments
            };

            return PartialView("CommPart", newsModel);
        }
    }
}