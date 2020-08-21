using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackManagment.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTrackManagment.Controllers
{
    [Authorize(Roles = "Manager")]

    public class ManagerController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> userManager;

        public ManagerController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }




        public async Task<IActionResult> Index()
        {

            ViewBag.BugsCount = await _context.Bug.Where(x=>x.Status!=StatusOptions.solved).CountAsync();
            ViewBag.TesterCount = await userManager.Users.Where(x => x.Department == Dept.Tester).CountAsync();
            ViewBag.ProgrammerCount = await userManager.Users.Where(x => x.Department == Dept.Programmer).CountAsync();

            return View(await _context.Project.ToListAsync());

            //return View();
        }

        public  IActionResult ProjectList()
        {
            return View();
        }


        // GET: Projects/Details/5
        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult ProjectCreate()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectCreate([Bind("Id,Title,Summary")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();

                //return PartialView(await _context.Project.ToListAsync());

                return Json(new { isValid = true, });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ProjectCreate", project) });
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> ProjectEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectEdit(int id, [Bind("Id,Title,Summary")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                    return Json(new { isValid = true });

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ProjectEdit", project) });
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> ProjectDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("ProjectDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("ProjectList");
        }


         public async Task<IActionResult> BugsDashBoardAsync()
        {
            var model = await _context.Bug.ToListAsync();
            return View(model);
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }



        // GET: Bugs/Details/5
        public async Task<IActionResult> BugsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }



            //done
            TimeSpan createdSpanTime = DateTime.Now.Subtract(bug.CreatedTime);
            ViewBag.createdSpanTime = createdSpanTime.Humanize(2);

            //done
            TimeSpan AssiginedSpanTime = DateTime.Now.Subtract(bug.AssiginedTime);
            ViewBag.AssiginedSpanTime = AssiginedSpanTime.Humanize(2);

            //done
            TimeSpan VerfiedSpanTime = DateTime.Now.Subtract(bug.VerfiedTime);
            ViewBag.VerfiedSpanTime = VerfiedSpanTime.Humanize(2);

            //done
            TimeSpan RejectedSpanTime = DateTime.Now.Subtract(bug.RejectedTime);
            ViewBag.RejectedSpanTime = RejectedSpanTime.Humanize(2);

            //done
            TimeSpan DuplicateSpanTime = DateTime.Now.Subtract(bug.DuplicateTime);
            ViewBag.DuplicateSpanTime = DuplicateSpanTime.Humanize(2);

            //done
            TimeSpan SolvedSpanTime = DateTime.Now.Subtract(bug.SolvedTime);
            ViewBag.SolvedSpanTime = SolvedSpanTime.Humanize(2);

            //done
            TimeSpan ClosedSpanTime = DateTime.Now.Subtract(bug.ClosedTime);
            ViewBag.ClosedSpanTime = ClosedSpanTime.Humanize(2);


            return View(bug);

           


        }





    }
}
