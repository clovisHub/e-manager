using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BeesApp.Models
{
    
    public class BusinessOptionModel
    {
        public BusinessOptionModel() { }

        public int BusinessOptionModelId { get; set; }

        public double InStock { set; get; }

        public double StockAdded { set; get; }

        public double TotalStock { set; get; }

        public double StockLost { set; get; }

        public double CostOfLost { set; get; }

        public double StockRemain { set; get; }

        public double PricePerPound { set; get; }

        public double TotalSales { set; get; }

        public double Gains { set; get; }

        public DateTime ActualDate { set; get; }

        public virtual GlobalReportModel ReportList { get; set; }
    }

    public class GlobalReportModel
    {
        public GlobalReportModel()
        {
            Accounting = new List<BusinessOptionModel>();
        }

        public int GlobalReportModelId { set; get; }

        [Required]
        public string Author { set; get; }

        public virtual ICollection<BusinessOptionModel> Accounting { set; get; }
    }

    public class EmployeeModel
    {
        public EmployeeModel()
        {
            this.MyManagers = new List<ManagerAdmiModel>();
        }

        protected int EmployeeModelId { set; get; }

        public string Name { set; get; }

        public DateTime LogInTime { set; get; }

        public DateTime LogOutTime { set; get; }

        public double InStock { set; get; }

        public virtual ICollection<ManagerAdmiModel> MyManagers { set; get; }

    }

    public class SuperAdminModel 
    {
        public SuperAdminModel() { }

        public int SuperAdminModelId { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public BusinessOptionModel BusinessMgr { get; set; }

        [NotMapped]
        public GlobalReportModel GlobalSales { set; get; }

    }

    public class ManagerAdmiModel 
    {
        public ManagerAdmiModel()
        {
            
        }

        public int ManagerAdmiModelId { set; get; }

        [Required]
        public string ManagerName { get; set; }

        public virtual ICollection<DriverModel> Drivers { set; get; }

        public virtual ICollection<CultivatorModel> Cultivators { set; get; }

        public virtual ICollection<CashierModel> Cashiers { get; set; }

        public virtual ICollection<OnlineOrderModel> OnlineSales { get; set; }

    }

    public class CultivatorModel :EmployeeModel
    {
        public CultivatorModel() { }

        public int CultivatorModelId { set; get; }

        public int  EstimatedBeesNumber { set; get; }

        public double Weighted { set; get; }

        public DateTime DateWeighted { get; set; }
    }

    public class DriverModel:EmployeeModel
    {
        public DriverModel() { }

        public int DriverModelId { set; get; }
        
        public OnlineOrderModel OrderToDeliver { set; get; }

        public DeliveriesModel ForDeliveries { set; get; }

       
    }

    public class CashierModel: EmployeeModel
    {
        public CashierModel() { }

        public int CashierModelId { set; get; }

        
        public double PricePerPound { get; set; }

        public string ClientName { set; get; }

        public double QuantitySold { set; get; }

        public double AmountToPaid { set; get; }        

    }

    public class OnlineOrderModel 
    {
         

        public OnlineOrderModel() { }

      

        public int OnlineOrderModelId { set; get; }

        [Display(Name="Name")]
        [Required]
        public string ClientName { set; get; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { set; get; }
        
        [Display(Name="Is delivered?")]
        [DefaultValue (false)]
        public bool IsDelivered { get; set; }

        [Display(Name="Quantity")]
        public double QuantityPurchased { set; get; }

        [Display(Name="Confirmation Number")]
        public string ConfirmationNumber { set; get; }

        public virtual ManagerAdmiModel TheManager { set; get; }

        public DeliveriesModel DeliveriesOrder { get; set; }

        [Required]
        [NotMapped]
        [Display(Name="Email")]
        public string ClientEmail { get; set; }

    }
  
}