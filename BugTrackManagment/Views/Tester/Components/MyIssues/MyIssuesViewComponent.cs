using BugTrackManagment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Views.Tester.Components.MyIssues
{
    public class MyIssuesViewComponent : ViewComponent
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<AppUser> userManager;

        public MyIssuesViewComponent(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync(bool isFull)
        {
            ViewBag.page = isFull;
            AppUser user = await userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

            IEnumerable<Bug> model = await appDbContext.Bug.Where(q => q.CreatedUser == user).ToListAsync();

            if (isFull)
            {
                if (model.Count() > 3) { model = model.TakeLast(3); }

            }



            return View(model);
        }

    }
}
