﻿using Microsoft.AspNetCore.Mvc;

namespace BugTrackManagment.Views.Shared.Components.HeaderLeft
{
    public class HeaderLeftViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}