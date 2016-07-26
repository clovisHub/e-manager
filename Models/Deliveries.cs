using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BeesApp.Models
{
  

    public class DeliveriesModel
    {
        public DeliveriesModel()
        {
            
        }

        public int DeliveriesModelId { set; get; }

        [DefaultValue(false)]
        public bool IsOnPending { set; get; }

        [Required]
        public string DriversName { get; set; }
            
        [Required]
        public string DriversId { get; set; }

        [Required]
        public string OrdersId { get; set; }
        [Required]
        public string ManagerName { get; set; }

        [Required]
        public string ApprovalCod { get; set; }

        [DefaultValue(false)]
        public bool Approved { get; set; }

        public DateTime DateAssigned { get; set; }

    }

    public class SavedReportModel
    {
        public SavedReportModel() { }

        public int SavedReportModelId { set; get; }

        [Required]
        public string DriversName { get; set; }

        [Required]
        public string DriverId { get; set; }

        [Required]
        public string OrdersId { get; set; }

        [Required]
        public string ManagerName { get; set; }

        [Required]
        public string ApprovalCode { get; set; }

        [DefaultValue(false)]
        public bool Approved { get; set; }

        public string ClientName { get; set; }

        public string Quantity { set; get; }

        public DateTime DateAssigned { get; set; }

    }
    
   
    public class Shipping
    {

        public Shipping()
        {
            Drivers = new List<ApplicationUser>();

            Orders = new List<OnlineOrderModel>();

            TempReport = new List<DeliveriesModel>();
        }


        public int ShippingId { set; get; }

        public ICollection<ApplicationUser> Drivers { get; set; }

        public ICollection<OnlineOrderModel> Orders { set; get; }

        public ICollection<DeliveriesModel> TempReport { set; get; }
    }


 

}