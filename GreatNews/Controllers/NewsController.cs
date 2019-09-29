using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreatNews.Models;
using GreatNews.UoW;

namespace GreatNews.Controllers
{
    public class NewsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public NewsController(IUnitOfWork uow)
        {
            _unitOfWork = uow;

        }

        //private readonly NewsContext _context;

        //public NewsController(NewsContext context)
        //{
        //    _context = context;
        //}

        // GET: News
        //public async Task<IActionResult> Index()
        //{
        //    
        //    return View(await _context.News_.ToList());
        //}

        public IActionResult Index()
        {

            return View(_unitOfWork.News_.AsQueryable());
        }

        //public Guid OutId;


        //    [HttpGet]
        //    public IActionResult Comment(Guid Id)
        //    {

        //        ViewBag.newsId = Id;

        //        return View();
        //    }

        //    [HttpPost]
        //    public string Comment(Comment comment)
        //    {
        //        _context.Comments_.Add(comment);
        //        _context.SaveChanges();
        //        return "Comment saved";
        //    }

        //    //GET: News/Details/5
        //        public async Task<IActionResult> Details(Guid? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var news = await _context.News_
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (news == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(news);
        //    }

        //    // GET: News/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: News/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("Id,Source,Heading,Date,PositiveIndex,Content")] News news)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(news);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(news);
        //    }

        //    // GET: News/Edit/5
        //    public async Task<IActionResult> Edit(Guid? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var news = await _context.News_.FindAsync(id);
        //        if (news == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(news);
        //    }

        //    // POST: News/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Source,Heading,Date,PositiveIndex,Content")] News news)
        //    {
        //        if (id != news.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(news);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!NewsExists(news.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(news);
        //    }

        //    // GET: News/Delete/5
        //    public async Task<IActionResult> Delete(Guid? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var news = await _context.News_
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (news == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(news);
        //    }

        //    // POST: News/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(Guid id)
        //    {
        //        var news = await _context.News_.FindAsync(id);
        //        _context.News_.Remove(news);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool NewsExists(Guid id)
        //    {
        //        return _context.News_.Any(e => e.Id == id);
        //    }
    }
}

