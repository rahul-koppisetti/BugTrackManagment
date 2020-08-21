using BugTrackManagment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Views.Tester.Components.VerifyQ
{
    public class VerifyQViewComponent : ViewComponent
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<AppUser> userManager;

        public VerifyQViewComponent(AppDbContext appDbContext ,UserManager<AppUser> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync(bool? isfull)
        {
            ViewBag.page = isfull;

            AppUser user = await userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

            var bugs = await appDbContext.Bug.Where(m => 
            m.Status == StatusOptions.solved
            &&
            m.CreatedUser==user
            ).ToListAsync();
            return View(bugs);
        }
    }
}