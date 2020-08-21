using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.Footer
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}