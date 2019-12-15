using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CORE_
{
    public interface ILemmatizationService
    {
        Task<int> GetPositiveIndex(Guid newsId);
    }
}
