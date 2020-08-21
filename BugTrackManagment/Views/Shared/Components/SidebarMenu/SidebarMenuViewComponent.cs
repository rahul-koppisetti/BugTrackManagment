using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.SidebarMenu
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}