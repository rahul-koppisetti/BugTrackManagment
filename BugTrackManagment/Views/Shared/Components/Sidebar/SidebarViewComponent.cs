using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.Sidebar
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}