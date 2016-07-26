using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeesApp.Models;
using IdentitySample.Models;

namespace BeesApp.Controllers.Employees
{
    public class ManagerAdmiModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManagerAdmiModels
        public ActionResult Index()
        {
            return View(db.Managers.ToList());
        }

        // GET: ManagerAdmiModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerAdmiModel managerAdmiModel = db.Managers.Find(id);
            if (managerAdmiModel == null)
            {
                return HttpNotFound();
            }
            return View(managerAdmiModel);
        }

        // GET: ManagerAdmiModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerAdmiModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagerAdmiModelId,ManagerName")] ManagerAdmiModel managerAdmiModel)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(managerAdmiModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(managerAdmiModel);
        }

        // GET: ManagerAdmiModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerAdmiModel managerAdmiModel = db.Managers.Find(id);
            if (managerAdmiModel == null)
            {
                return HttpNotFound();
            }
            return View(managerAdmiModel);
        }

        // POST: ManagerAdmiModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagerAdmiModelId,ManagerName")] ManagerAdmiModel managerAdmiModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managerAdmiModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(managerAdmiModel);
        }

        // GET: ManagerAdmiModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerAdmiModel managerAdmiModel = db.Managers.Find(id);
            if (managerAdmiModel == null)
            {
                return HttpNotFound();
            }
            return View(managerAdmiModel);
        }

        // POST: ManagerAdmiModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManagerAdmiModel managerAdmiModel = db.Managers.Find(id);
            db.Managers.Remove(managerAdmiModel);
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
