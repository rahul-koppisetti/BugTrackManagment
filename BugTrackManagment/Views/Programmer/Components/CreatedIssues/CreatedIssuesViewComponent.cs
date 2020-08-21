using BugTrackManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Views.Programmer.Components.CreatedIssues
{
    public class CreatedIssuesViewComponent : ViewComponent
    {
        private readonly AppDbContext appDbContext;

        public CreatedIssuesViewComponent(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<IViewComponentResult> InvokeAsync(bool isIndex)
        {
            ViewBag.isIndex = isIndex;

            var model = await appDbContext.Bug.Where(m => m.Status == StatusOptions.created).ToListAsync();

            if (isIndex)
            {
                if (model.Count > 4) { model.TakeLast(4); }
            }


            return View(model);
        }
    }
}
