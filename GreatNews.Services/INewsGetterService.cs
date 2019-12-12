using GreatNews.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GreatNews.Services
{
    public interface INewsGetterService
    {
      Task<bool> AutoRefresh();
    }
}
