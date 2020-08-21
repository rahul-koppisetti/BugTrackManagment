using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.FooterRight
{
    public class FooterRightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}