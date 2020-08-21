using BugTrackManagment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BugTrackManagment.Views.Manager.Components.BugList
{
    public class BugListViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;


        public BugListViewComponent(AppDbContext context)
        {
            _context = context;

        }

        public IViewComponentResult Invoke(bool isIndex)
        {
            ViewBag.isIndex = isIndex;


           IEnumerable<Bug> model= _context.Bug;

            if (isIndex)
            {
                if (model.Count() > 4)
                {
                  model=  model.TakeLast(4);
                }
            }


            return View(model);
        }

    }
}
