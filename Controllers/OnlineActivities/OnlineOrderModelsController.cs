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
using System.Net.Mail;
using System.Threading.Tasks;

namespace BeesApp.Controllers.Employees
{
    public class OnlineOrderModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OnlineOrderModels
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: OnlineOrderModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlineOrderModel onlineOrderModel = db.Orders.Find(id);
            if (onlineOrderModel == null)
            {
                return HttpNotFound();
            }
            return View(onlineOrderModel);
        }

        // GET: OnlineOrderModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // might be get data
        // POST: OnlineOrderModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task <ActionResult> Create([Bind(Include = "OnlineOrderModelId,ClientName,Address,Telephone,ConfirmOrder,QuantityPurchased,ClientEmail")] OnlineOrderModel onlineOrder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //        var stringChars = new char[10];
        //        var random = new Random();

        //        for (int i = 0; i < stringChars.Length; i++)
        //        {
        //            stringChars[i] = chars[random.Next(chars.Length)];
        //        }

        //        var finalString = new String(stringChars);

        //        onlineOrder.ConfirmationNumber = finalString;

        //        using (var message = new MailMessage())
        //        {
        //            message.From = new MailAddress(onlineOrder.ClientEmail);
        //            message.To.Add(onlineOrder.ClientEmail);
        //            message.Subject = "confirmation number from Louradobees";
        //            message.Body = "<p>Order for : "
        //                           +onlineOrder.ClientName+"\n </p>"+
        //                           "<p>Your order comfirmation number is : "
        //                           +onlineOrder.ConfirmationNumber+"\n </p>"
        //                           +" You purchased : "
        //                           + onlineOrder.QuantityPurchased+
        //                           " bottles of honey.";

        //            message.IsBodyHtml = true;

        //            var smtp = new SmtpClient();

        //            await smtp.SendMailAsync(message);

        //            db.Orders.Add(onlineOrder);
        //            db.SaveChanges();

        //            return RedirectToAction("Sent");
        //        }
                
        //    }

        //    return View(onlineOrder);
        //}

        public ActionResult Sent()
        {
            return View();
        }

        // GET: OnlineOrderModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlineOrderModel onlineOrderModel = db.Orders.Find(id);
            if (onlineOrderModel == null)
            {
                return HttpNotFound();
            }
            return View(onlineOrderModel);
        }

        // POST: OnlineOrderModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OnlineOrderModelId,ClientName,Address,Telephone,ConfirmOrder,QuantityPurchased")] OnlineOrderModel onlineOrderModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(onlineOrderModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(onlineOrderModel);
        }

        // GET: OnlineOrderModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlineOrderModel onlineOrderModel = db.Orders.Find(id);
            if (onlineOrderModel == null)
            {
                return HttpNotFound();
            }
            return View(onlineOrderModel);
        }

        // POST: OnlineOrderModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OnlineOrderModel onlineOrderModel = db.Orders.Find(id);
            db.Orders.Remove(onlineOrderModel);
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
