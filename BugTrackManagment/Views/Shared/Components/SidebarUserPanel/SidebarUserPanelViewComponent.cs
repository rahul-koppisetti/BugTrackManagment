using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.SidebarUserPanel
{
    public class SidebarUserPanelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}