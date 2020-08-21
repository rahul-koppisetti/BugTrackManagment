using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.FooterLeft
{
    public class FooterLeftViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}