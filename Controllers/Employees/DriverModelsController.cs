using System;
using System.Collections;
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
using Microsoft.AspNet.Identity.Owin;

namespace BeesApp.Controllers.Employees
{

    [Authorize]
    public class DriverModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public DriverModelsController()
        {
        }

        public DriverModelsController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }





        // GET: DriverModels
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> Index()
        {
            //var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role

            var orders = new List<DeliveriesModel>();

            orders = (from or in db.Deliveries select or).ToList();

            Shipping myModel = new Shipping();

            IList<DeliveriesModel> deliveryList = new List<DeliveriesModel>();

            IList<ApplicationUser> myDrivers = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, "Driver"))
                {
                    myDrivers.Add(user);
                    myModel.Drivers.Add(user);
                }
            }


            if (orders == null)
            {

            }
            else if (orders.Count() == 0)
            {

            }
            else
            {
                int i = 0;

                while (i < orders.Count)
                {
                    deliveryList.Add(orders.ElementAt(i));
                    i++;
                }

                for (int k = 0; k < myDrivers.Count; k++)
                {
                    int begin = 0;

                    while (begin < deliveryList.Count)
                    {

                        if (myDrivers.ElementAt(k).UserName.Equals(deliveryList.ElementAt(begin).DriversName))
                        {
                            myModel.Drivers.Remove(myDrivers.ElementAt(k));

                            break;
                        }

                        begin++;
                    }

                }


            }

            IList<OnlineOrderModel> myOrders = new List<OnlineOrderModel>();

            myOrders = (from or in db.Orders where or.IsDelivered == false select or).ToList();

            myModel.Orders = (from or in db.Orders where or.IsDelivered == false select or).ToList();


            if (orders == null)
            {

            }
            else if (orders.Count() == 0)
            {

            }
            else
            {
                int i = 0;

                while (i < orders.Count)
                {
                    deliveryList.Add(orders.ElementAt(i));
                    i++;
                }

                for (int k = 0; k < myOrders.Count; k++)
                {
                    int begin = 0;

                    while (begin < deliveryList.Count)
                    {
                        string myorders = "" + myOrders.ElementAt(k).OnlineOrderModelId;

                        string deliver = "" + deliveryList.ElementAt(begin).OrdersId;

                        if (("" + myOrders.ElementAt(k).OnlineOrderModelId).Equals("" + deliveryList.ElementAt(begin).OrdersId))
                        {
                            myModel.Orders.Remove(myOrders.ElementAt(k));

                            break;
                        }

                        begin++;
                    }

                }


            }








            //myModel.Orders = (from or in db.Orders select or).ToList();

            //ViewBag.Orders = myModel.Orders.ElementAt(1).IsDelivered;

            //ViewBag.Dirvers = myModel.Drivers;


            return View(myModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> Index(FormCollection form)
        {

            string driverName = form["driverName"].ToString();

            string driverId = form["driverId"].ToString();

            string date = form["dateAndHour"].ToString();

            string orderId = form["orderId"].ToString();

            string managerName = form["managerName"].ToString();

            string clientName = form["clientName"].ToString();

            string quantity = form["quantity"].ToString();






            DeliveriesModel sendData = new DeliveriesModel();

            sendData.DriversName = driverName;
            sendData.DriversId = driverId;
            sendData.OrdersId = orderId;
            sendData.ManagerName = managerName;
            sendData.DateAssigned = Convert.ToDateTime(date);

            SavedReportModel savedReport = new SavedReportModel();

            savedReport.DriversName = sendData.DriversName;
            savedReport.DriverId = sendData.DriversId;
            savedReport.OrdersId = sendData.OrdersId;
            savedReport.ManagerName = sendData.ManagerName;
            savedReport.DateAssigned = sendData.DateAssigned;

            savedReport.ClientName = clientName;
            savedReport.Quantity = quantity;


            // set a random approval code



            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            savedReport.ApprovalCode = finalString;

            sendData.ApprovalCod = finalString;


            Shipping getItFromDb = new Shipping();

            //[Bind(Include = "DriversName,OrderId, ManagerName, DateAssigned")] DeliveriesModel toDeliverModel
            if (ModelState.IsValid)
            {
                db.Deliveries.Add(sendData);
                db.SavedReportModel.Add(savedReport);
                await db.SaveChangesAsync();

                getItFromDb.TempReport = (from or in db.Deliveries select or).ToList();
                ViewData["drivers"] = (from or in db.Deliveries select or).ToList();


                return RedirectToAction("Index", getItFromDb);
                // return View(getItFromDb);
            }

            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult GetManagerCode()
        {
            IList<DeliveriesModel> myPendingOrders = new List<DeliveriesModel>();

            myPendingOrders = (from or in db.Deliveries select or).ToList();

            return View(myPendingOrders);
        }


        // GET: DriverModels
        public ActionResult ToDeliver()
        {

            Shipping toDeliver = new Shipping();

            toDeliver.TempReport = (from or in db.Deliveries where or.IsOnPending == false select or).ToList();
            // toDeliver.TempReport = toDeliver.TempReport.Where(or => or.IsOnPending == true).ToList();

            toDeliver.Orders = (from shp in db.Orders where shp.IsDelivered == false select shp).ToList();

            if (toDeliver != null)
            {
                return View(toDeliver);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ToDeliver(FormCollection Form1, FormCollection Form2)
        {
            bool execute = true;
            // Handle exception null value
            if (Form1["managerApproval"] == null && Form2["clientApproval"] != null)
            {
                execute = false;
            }
            else
            {
                execute = true;
            }
            //string first = ""+Form1["managerApproval"].ToString();

            //string second = ""+Form2["managerApproval"].ToString();


            if (execute == true)
            {

                string approveManager = Form1["managerApproval"].ToString().Trim();

                IList<SavedReportModel> saved = new List<SavedReportModel>();
                Shipping toDeliver = new Shipping();

                saved = (from svd in db.SavedReportModel select svd).ToList();
                toDeliver.TempReport = (from or in db.Deliveries select or).ToList();

                for (int i = 0; i < saved.Count; i++)
                {
                    string stored = saved.ElementAt(i).ApprovalCode;

                    string manager = approveManager;


                    if (approveManager.Equals(stored))
                    {

                        // li.Where(w => w.name == "di").Select(s => { s.age = 10; return s; }).ToList();

                        saved.Where(w => w.ApprovalCode == approveManager).Select(s => { s.Approved = true; return s; }).ToList();

                        // toDeliver.TempReport.Select(t => { t.Approved = true; return t; }).ToList();

                        toDeliver.TempReport.Where(w => w.ApprovalCod == approveManager).Select(d => { d.Approved = true; return d; }).ToList();



                        // ViewData["boole"] = true;

                        await db.SaveChangesAsync();


                        break;
                    }
                }


                return RedirectToAction("ToDeliver", toDeliver);
            }
            else
            {

                string verifyClient = Form2["clientApproval"].ToString().Trim();

                Shipping toDeliver = new Shipping();


                toDeliver.Orders = (from svd in db.Orders select svd).ToList();
                toDeliver.TempReport = (from del in db.Deliveries select del).ToList();


                for (int i = 0; i < toDeliver.Orders.Count; i++)
                {
                    string test = "" + toDeliver.Orders.ElementAt(i).ConfirmationNumber;
                    if (("" + toDeliver.Orders.ElementAt(i).ConfirmationNumber).Equals(verifyClient))
                    {
                        string commun = toDeliver.Orders.ElementAt(i).OnlineOrderModelId.ToString();

                        toDeliver.Orders.Where(O => O.ConfirmationNumber == verifyClient).Select(b => { b.IsDelivered = true; return b; }).ToList();

                        toDeliver.TempReport.Where(op => op.IsOnPending == false && op.OrdersId == commun).Select(chng => { chng.IsOnPending = true; return chng; }).ToList();

                        await db.SaveChangesAsync();

                        toDeliver.TempReport = (from de in db.Deliveries where de.IsOnPending == true select de).ToList();

                        foreach (var item in toDeliver.TempReport)
                        {
                            db.Deliveries.Remove(item);
                        }




                        await db.SaveChangesAsync();

                        break;
                    }


                }
                return RedirectToAction("ToDeliver", toDeliver);
            }


            // return View();
        }


        public ActionResult DeleteOrder(string id)
        {
            if (id == null)
            {
                return View();
            }

            var deleteOrderDetails = (from or in db.Deliveries
                                      where or.DeliveriesModelId.ToString() == id
                                      select or).ToList();

            foreach (var detail in deleteOrderDetails)
            {
                db.Deliveries.Remove(detail);
            }

            try
            {
                db.SaveChanges();

                return RedirectToAction("GetManagerCode");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }

            return View();
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
