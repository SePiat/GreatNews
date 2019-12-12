using GreatNews.Models;
using GreatNews.UoW;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GreatNews.Services
{
    public class NewsGetterService : INewsGetterService
    {
        private readonly IHtmlArticleServiceS13 _ArtServ13;
        private readonly IHtmlArticleServiceOnliner _ArtServOnliner;
        private readonly IUnitOfWork _unitOfWork;

        public NewsGetterService(IUnitOfWork unitOfWork, IHtmlArticleServiceS13 ArtServ13, IHtmlArticleServiceOnliner ArtServOnliner)
        {
            _ArtServ13 = ArtServ13;
            _ArtServOnliner = ArtServOnliner;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<bool> AutoRefresh()
        {
            var newsFromArt13 = _ArtServ13.GetArticleFromUrl();
            _ArtServ13.AddRange(newsFromArt13);

            var newsFromArtOnl = _ArtServOnliner.GetArticleFromUrl();
            _ArtServOnliner.AddRange(newsFromArtOnl);

           _unitOfWork.Save();

            return true;
        }
    }
}
