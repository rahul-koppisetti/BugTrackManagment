using BugTrackManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Views.Tester.Components.Index_BugList
{
    public class Index_BugListViewComponent:ViewComponent
    {
        
        private readonly AppDbContext appDbContext;

        public Index_BugListViewComponent(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await appDbContext.Bug.ToListAsync();
            return View(model);
        }
    }
}
