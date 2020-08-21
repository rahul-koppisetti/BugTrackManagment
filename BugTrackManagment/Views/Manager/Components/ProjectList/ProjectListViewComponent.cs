using BugTrackManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackManagment.Views.Manager.Components.ProjectList
{
    public class ProjectListViewComponent : ViewComponent
    {
        private readonly AppDbContext appDbContext;

        public ProjectListViewComponent(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // GET: Projects
        public async Task<IViewComponentResult> InvokeAsync(bool isIndex)
        {
            ViewBag.page = isIndex;

            IEnumerable<Project> model = await appDbContext.Project.ToListAsync();

            if (isIndex)
            {
                if (model.Count() > 4) model= model.TakeLast(4);
            }

            return View(model);
        }

    }
}
