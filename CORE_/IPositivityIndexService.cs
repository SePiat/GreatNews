using System.Threading.Tasks;

namespace CORE_
{
    public interface IPositivityIndexService
    {
        Task<bool> AddPsitiveIndexToNews();
    }
}
