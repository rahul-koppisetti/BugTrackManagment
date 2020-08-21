using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackManagment.Models;
using BugTrackManagment.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BugTrackManagment.Controllers
{
    [Authorize(Roles = "Tester")]
    public class TesterController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> userManager;

        public TesterController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }


        public IActionResult UpdateIndex_BugList()
        {
            return ViewComponent("Index_BugList");
        }



        // GET: Bugs
        public async Task<IActionResult> Index()
        {
            //count All Issues
            var AllIssues = await _context.Bug.Where(q =>
                  q.Status != StatusOptions.closed )
                .CountAsync();
            ViewBag.AllIssues = AllIssues;

            //count Tester created Issues
            AppUser user = await userManager.GetUserAsync(User);
            var MyIssues= await _context.Bug.Where(q => 
            q.CreatedUser == user).CountAsync();
            ViewBag.MyIssues = MyIssues;


            //count tester Verify Queue
            var VeriyQ = await _context.Bug.Where(q =>
                 q.Status == StatusOptions.solved
                 &&
                 q.CreatedUser==user).CountAsync();
            ViewBag.VeriyQ = VeriyQ;

            
            
            
            return View();
        }

        public async Task<IActionResult> TesterIssues()
        {
            AppUser user = await userManager.GetUserAsync(User);


            var model = await _context.Bug.Where(q =>
                  q.Status == StatusOptions.created && q.CreatedUser == user)
                .ToListAsync();
            
            return View(model);
        }

        
        public async Task<IActionResult> VerifyQueue()
        {
            

            return View(await _context.Bug.Where(q =>
                q.Status == StatusOptions.solved )
                .ToListAsync());
        }

        // GET: Bugs/Create
        public async Task<IActionResult> CreateAsync()
        {
            var _projectlist = await _context.Project.ToListAsync();
            var selectList= new SelectList(_projectlist,"Id","Title");

            CreateBugViewModel model = new CreateBugViewModel { ProjectList = selectList };

            //ViewBag.projects = new SelectList (_context.Project.ToList() ,"Id" , "Title");
            return View(model);
        }



        // POST: Bugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBugViewModel bugViewModel)
        {
           

            if (ModelState.IsValid)
            {
                //project
                var id=bugViewModel.Project;
                var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);

                //user
                AppUser user = await userManager.GetUserAsync(User);



                var bug = new Bug {
                    Title = bugViewModel.Title,
                    Summary = bugViewModel.Summary,
                    Description = bugViewModel.Description,
                    Project = project,
                    ProjectTitle = project.Title,
                    CreatedUser = user,
                    CreatedUserName = user.UserName,
                    CreatedTime = DateTime.Now,
                    Status=StatusOptions.created,
                                    
                };
                //adding to db
              await  _context.AddAsync(bug);

                //saving changes
                await _context.SaveChangesAsync();
                
                //returing new updated data to Index View
                var bugs = await _context.Bug.Where(q =>
                  q.Status == StatusOptions.created || q.Status == StatusOptions.solved).ToListAsync();


                return Json(new { isValid = true/*, html = Helper.RenderRazorViewToString(this, "_index", bugs)*/ });

                //return RedirectToAction("index", "tester");
            }


            var _projectlist = await _context.Project.ToListAsync();
            var selectList = new SelectList(_projectlist, "Id", "Title");

            bugViewModel.ProjectList = selectList;

            //return View(bug);

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "create", bugViewModel) }); 
        
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bug.FindAsync(id);
            _context.Bug.Remove(bug);
            await _context.SaveChangesAsync();
            var bugs = await _context.Bug.Where(q =>
                 q.Status == StatusOptions.created || q.Status == StatusOptions.solved).ToListAsync();

            return Json(new { isValid = true/*, html = Helper.RenderRazorViewToString(this, "_index", bugs)*/ });

            //return RedirectToAction(nameof(Index));
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


        

        public async Task<IActionResult> DetailsNewIssue(int? id)
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



      
        public async Task<IActionResult> MarkVerfied(int? id)
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

            bug.Status = StatusOptions.verfied;
            bug.VerfiedTime = DateAndTime.Now;
            bug.VerfiedUser = user;
            bug.VerfiedUserName = user.UserName;

            await _context.SaveChangesAsync();
            
            return RedirectToAction("VerifyQueue", "tester");

            //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_index", bugs) });


        }

        public async Task<IActionResult> markNotSolved(int? id)
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

            bug.Status = StatusOptions.created;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "tester");


        }
    }
}
