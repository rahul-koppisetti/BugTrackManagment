using BugTrackManagment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Views.Programmer.Components.WorkingOn
{
    public class WorkingOnViewComponent:ViewComponent
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<AppUser> userManager;

        public WorkingOnViewComponent(AppDbContext appDbContext,UserManager<AppUser> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }



        public async Task<IViewComponentResult> InvokeAsync(bool isIndex)
        {
            ViewBag.isIndex = isIndex;

            AppUser user = await userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

            IEnumerable<Bug> model = await appDbContext.Bug.Where(m =>
                                m.Status == StatusOptions.Assigined
                                &&
                                m.AssigineddUser == user).ToListAsync();
            if (isIndex)
            {
                if (model.Count()>4) {model= model.TakeLast(4); }
            }


            return View(model);
        }

    }
}
