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
    public class CultivatorModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CultivatorModels
        public ActionResult Index()
        {
            return View(db.Cultivators.ToList());
        }

        // GET: CultivatorModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CultivatorModel cultivatorModel = db.Cultivators.Find(id);
            if (cultivatorModel == null)
            {
                return HttpNotFound();
            }
            return View(cultivatorModel);
        }

        // GET: CultivatorModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CultivatorModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CultivatorModelId,EstimatedBeesNumber,Weighted,DateWeighted")] CultivatorModel cultivatorModel)
        {
            if (ModelState.IsValid)
            {
                db.Cultivators.Add(cultivatorModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cultivatorModel);
        }

        // GET: CultivatorModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CultivatorModel cultivatorModel = db.Cultivators.Find(id);
            if (cultivatorModel == null)
            {
                return HttpNotFound();
            }
            return View(cultivatorModel);
        }

        // POST: CultivatorModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CultivatorModelId,EstimatedBeesNumber,Weighted,DateWeighted")] CultivatorModel cultivatorModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cultivatorModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cultivatorModel);
        }

        // GET: CultivatorModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CultivatorModel cultivatorModel = db.Cultivators.Find(id);
            if (cultivatorModel == null)
            {
                return HttpNotFound();
            }
            return View(cultivatorModel);
        }

        // POST: CultivatorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CultivatorModel cultivatorModel = db.Cultivators.Find(id);
            db.Cultivators.Remove(cultivatorModel);
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
