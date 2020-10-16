using Xamarin.Forms.Internals;

namespace DemoDemo.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}