using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeesApp.Models;
using IdentitySample.Models;

namespace BeesApp.Controllers.Employees
{
    public class CashierModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CashierModels
        [Authorize(Roles="Admin, SuperAdmin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Cashiers.ToListAsync());
        }

        // GET: CashierModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashierModel cashierModel = await db.Cashiers.FindAsync(id);
            if (cashierModel == null)
            {
                return HttpNotFound();
            }
            return View(cashierModel);
        }

        // GET: CashierModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CashierModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CashierModelId,PricePerPound,ClientName,QuantitySold,AmountToPaid,Name,LogInTime,LogOutTime,InStock")] CashierModel cashierModel)
        {
            if (ModelState.IsValid)
            {
                db.Cashiers.Add(cashierModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cashierModel);
        }

        // GET: CashierModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashierModel cashierModel = await db.Cashiers.FindAsync(id);
            if (cashierModel == null)
            {
                return HttpNotFound();
            }
            return View(cashierModel);
        }

        // POST: CashierModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CashierModelId,PricePerPound,ClientName,QuantitySold,AmountToPaid,Name,LogInTime,LogOutTime,InStock")] CashierModel cashierModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashierModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cashierModel);
        }

        // GET: CashierModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashierModel cashierModel = await db.Cashiers.FindAsync(id);
            if (cashierModel == null)
            {
                return HttpNotFound();
            }
            return View(cashierModel);
        }

        // POST: CashierModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CashierModel cashierModel = await db.Cashiers.FindAsync(id);
            db.Cashiers.Remove(cashierModel);
            await db.SaveChangesAsync();
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
