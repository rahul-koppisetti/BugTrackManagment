﻿using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}