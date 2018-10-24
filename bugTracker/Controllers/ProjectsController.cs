using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bugTracker.Models;
using bugTracker.Helpers;
using Microsoft.AspNet.Identity;

namespace bugTracker.Controllers
{
    [RequireHttps]
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper userHelper = new UserRolesHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();

        // GET: Projects
        
        public ActionResult Index()
        {
            var projects = projectHelper.ListUserProjects(User.Identity.GetUserId()).ToList();
            ViewBag.Projects = string.Empty.ToList();

            if (User.IsInRole("Developer") || User.IsInRole("Submitter"))
            {
                return View(projects);
            }

            ViewBag.Projects = projects;

            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Administrator, Project Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Details,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Created = DateTimeOffset.Now;

                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Administrator, Project Manager")]
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            Project project = db.Projects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            if (!(projectHelper.IsUserOnProject(User.Identity.GetUserId(), project.Id) && User.IsInRole("Project Manager")) && !(User.IsInRole("Administrator")))
            {
                return HttpNotFound();
            }

            ViewBag.UserOn = new MultiSelectList(projectHelper.UsersInProjNotInRole(id, "Project Manager"), "Id", "UserName");
            ViewBag.UserOff = new MultiSelectList(projectHelper.UsersNotOnProjOrInRole(id, "Project Manager"), "Id", "UserName");

            foreach (var pm in userHelper.UsersInRole("Project Manager"))
            {
                if(projectHelper.IsUserOnProject(pm.Id, id))
                {
                    ViewBag.CurrentPm = pm.UserName;
                }
            }

            ViewBag.PMs = new SelectList(userHelper.UsersInRole("Project manager"), "Id", "UserName");

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Details,Created")] Project project, List<string> UserOff, List<string> UserOn, string PMs)
        {
            if (ModelState.IsValid)
            {
                if (!(UserOff is null))
                {
                    foreach (var user in UserOff)
                    {
                        projectHelper.AddUserToProject(user, project.Id);
                    }
                }

                if (!(UserOn is null))
                {
                    foreach (var user in UserOn)
                    {
                        projectHelper.RemoveUserFromProject(user, project.Id);
                    }
                }

                if (!(string.IsNullOrEmpty(PMs)))
                {
                    if(!(projectHelper.IsUserOnProject(PMs, project.Id)))
                    {
                        projectHelper.AddUserToProject(PMs, project.Id);
                    }
                }

                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //TODO: Remove all users from project

            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
