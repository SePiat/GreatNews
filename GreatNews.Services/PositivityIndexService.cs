using CORE_;
using GreatNews.Models;
using System.Threading.Tasks;

namespace GreatNews.Services
{
    public class PositivityIndexService : IPositivityIndexService
    {

        private readonly ILemmatizationService _lemmatizationService;
        private readonly ApplicationContext _context;

        public PositivityIndexService(ILemmatizationService lemmatizationService, ApplicationContext context)
        {
            _lemmatizationService = lemmatizationService;
            _context = context;
        }

        public async Task<bool> AddPsitiveIndexToNews()
        {
            
            foreach (var news in _context.News_)
            {
                if (news.PositiveIndex == 0)
                {
                    try
                    {
                        news.PositiveIndex = await _lemmatizationService.GetPositiveIndex(news.Id);
                    }
                    catch (System.Exception)
                    {
                    _context.Remove(news);                    
                        continue;
                    }
              }
                 
            }
            _context.SaveChanges();
            return true;
        }

    }
}
