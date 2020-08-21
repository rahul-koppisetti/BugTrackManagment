using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTrackManagment.Controllers
{

    
    [Authorize(Roles = "Programmer")]

    public class ProgrammerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> userManager;

        public ProgrammerController(AppDbContext appDbContext,UserManager<AppUser> userManager )
        {
           _context = appDbContext;
            this.userManager = userManager;
        }


        // GET: Bugs
        public async Task<IActionResult> Index()
        {
            AppUser user = await userManager.GetUserAsync(User);

            ViewBag.CreatedIssuesCount = await _context.Bug.Where(bg => bg.Status == StatusOptions.created).CountAsync();

            ViewBag.WorkingOnCount = await _context.Bug.Where(m => m.AssigineddUser == user
                                                                   &&
                                                                   m.Status==StatusOptions.Assigined).CountAsync();

            ViewBag.InReviewCount = await _context.Bug.Where(m =>
                                m.AssigineddUser == user
                                &&
                                (
                                m.Status != StatusOptions.Assigined
                                ||
                                m.Status==StatusOptions.created
                                ||
                                m.Status==StatusOptions.verfied
                                )
                                ).CountAsync();



            return View();
        }

        public async Task<IActionResult> NewIssues()
        {

            IEnumerable<Bug> allbugs = await _context.Bug.Where(bg => bg.Status == StatusOptions.created ).ToListAsync();

            return View(allbugs);
        }



        public async Task<IActionResult> WorkingOn()
        {
            AppUser user = await userManager.GetUserAsync(User);
            IEnumerable<Bug> allbugs = await _context.Bug.Where(bg => bg.Status == StatusOptions.Assigined && bg.AssigineddUser == user).ToListAsync();

            return View(allbugs);
        }



        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(bug);
        }


        
        public async Task<IActionResult> MarkDuplicate(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FirstOrDefaultAsync(m => m.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            bug.Status = StatusOptions.Duplicate;
            AppUser user = await userManager.GetUserAsync(User);
            bug.DuplicateUser = user;
            bug.DuplicateUserName = user.UserName;
            bug.DuplicateTime = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Programmer");

            //return Json(new { html = Helper.RenderRazorViewToString(this, "_NewIssues", _context.Bug.Where(bg => bg.Status == StatusOptions.created).ToListAsync()) });
        }

        public async Task<IActionResult> Accept(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FirstOrDefaultAsync(m => m.Id == id);

            if (bug == null)
            {
                return NotFound();
            }
            AppUser user = await userManager.GetUserAsync(User);
            bug.AssigineddUser = user;
            bug.AssiginedUserName = user.UserName;
            bug.AssiginedTime = DateTime.Now;
            bug.Status = StatusOptions.Assigined;
            
            await _context.SaveChangesAsync();

            return RedirectToAction("WorkingOn", "Programmer");


        }



        public async Task<IActionResult> markFixed(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FirstOrDefaultAsync(m => m.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            AppUser user = await userManager.GetUserAsync(User);
            bug.SolvedUser = user;
            bug.SolvedUserName = user.UserName;
            bug.SolvedTime = DateTime.Now;
            bug.Status = StatusOptions.solved;
            
            await _context.SaveChangesAsync();

            return RedirectToAction("InReview", "Programmer");


        }



        public async Task<IActionResult> Detailsfixed(int? id)
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
                
            return View(bug);

        }





        public IActionResult InReview(int? id)
        {
            return View();
        }




    }
}
