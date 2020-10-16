using System.Threading.Tasks;
using Pharmacy.Sessions.Dto;

namespace Pharmacy.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
