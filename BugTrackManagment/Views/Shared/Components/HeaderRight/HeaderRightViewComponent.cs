using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.HeaderRight
{
    public class HeaderRightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}