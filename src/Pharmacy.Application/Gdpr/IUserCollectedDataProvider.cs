using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Pharmacy.Dto;

namespace Pharmacy.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
