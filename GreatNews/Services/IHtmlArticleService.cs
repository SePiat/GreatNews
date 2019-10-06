﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatNews.Models;

namespace AgilityPackSample.Services
{
    public interface IHtmlArticleService : IArticleService
    {
        IEnumerable<News> GetArticleFromUrl();
        bool AddRange(IEnumerable<News> news);
    }
}
