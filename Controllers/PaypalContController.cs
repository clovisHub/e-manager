using BeesApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeesApp.Controllers
{
    public class PaypalContController : Controller
    {
       // to send data to the paypal form view that will transfer us to Paypal website
       public ActionResult ValidateCommand([Bind(Include = "OnlineOrderModelId,ClientName,Address,Telephone,ConfirmOrder,QuantityPurchased,ClientEmail")] OnlineOrderModel onlineOrder)
       {
              bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
              var paypal = new PaypalTrans(useSandbox);

              paypal.item_name = "honey";
              paypal.amount = "12.5";

              paypal.address1 = onlineOrder.Address;
              paypal.day_phone_a = onlineOrder.Telephone;
              paypal.first_name = onlineOrder.ClientName;
              paypal.payer_email = onlineOrder.ClientEmail;
              paypal.@return = ConfigurationManager.AppSettings["return"];
              paypal.cancel_return = ConfigurationManager.AppSettings["cancel_return"];
              paypal.notify_url = ConfigurationManager.AppSettings["notify_url"];
              paypal.quantity = Convert.ToString(onlineOrder.QuantityPurchased);

              paypal.total_cost = onlineOrder.QuantityPurchased * 12.5;
              

              return View(paypal);
       }

       // to get data from paypal as "GetDataPaypal"
       public ActionResult RedirectFromPaypal()
       {
           var getData = new GetDataFromPaypal();

           var order = GetDataFromPaypal.InformationsOrder(GetDataFromPaypal.GetPaypalRespons(Request.QueryString["tx"]));

           ViewBag.tx = Request["tx"];

           return View(order);
       }

       public ActionResult CancelFromPaypal()
       {
           return View();
       }

       public ActionResult NotifyFromPaypal()
       {
           var getData = new GetDataFromPaypal();

           ViewBag.tx = Request.QueryString["tx"];

           var order = GetDataFromPaypal.InformationsOrder(GetDataFromPaypal.GetPaypalRespons(Request["txn_id"]));

         

           return View(order);
       }

    }
}