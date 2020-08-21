using BugTrackManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Views.Manager.Components.ProjectCount
{
    public class ProjectCountViewComponent:ViewComponent
    {
        private readonly AppDbContext appDbContext;

        public ProjectCountViewComponent(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
             ViewBag.count = await appDbContext.Project.CountAsync();
            return View();
        }

    }
}
