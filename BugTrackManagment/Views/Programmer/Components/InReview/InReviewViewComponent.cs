using BugTrackManagment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Views.Programmer.Components.InReview
{
    public class InReviewViewComponent:ViewComponent
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<AppUser> userManager;

        public InReviewViewComponent(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }





        public async Task<IViewComponentResult> InvokeAsync(bool isIndex)
        {
            ViewBag.isIndex = isIndex;

            AppUser user = await userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

            IEnumerable<Bug> model = await appDbContext.Bug.Where(m =>
                                m.AssigineddUser == user
                                &&
                                (
                                m.Status==StatusOptions.solved
                                ||
                                m.Status == StatusOptions.created
                                ||
                                m.Status == StatusOptions.verfied
                                )
                                ).ToListAsync();
            if (isIndex)
            {
                if (model.Count() > 4) { model =model.TakeLast(4); }
            }


            return View(model);
        }











    }








}
