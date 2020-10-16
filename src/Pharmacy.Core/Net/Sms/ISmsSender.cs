using System.Threading.Tasks;

namespace Pharmacy.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}