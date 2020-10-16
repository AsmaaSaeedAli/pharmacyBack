using System.Threading.Tasks;
using DemoDemo.Views;
using Xamarin.Forms;

namespace DemoDemo.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
