using System.Collections.Generic;
using MvvmHelpers;
using DemoDemo.Models.NavigationMenu;

namespace DemoDemo.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}