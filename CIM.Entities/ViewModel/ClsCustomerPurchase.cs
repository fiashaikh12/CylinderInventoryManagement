using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIM.Entities
{
    public class ClsCustomerPurchase
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public bool IsDepositGiven { get; set; }
        public bool IsDepositReturn { get; set; }
        public double DepositAmount { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public int Quantity { get; set; }
        public int BusinessId { get; set; }
    }
    public class ClsUser
    {
        public int UserId { get; set; }
        public int BusinessId { get; set; }
    }

    public class ClsCustomerPurchaseReturn: ClsUser
    {
        public int ProductId { get; set; }
        public int PurchaseQuantity { get; set; }
        public int ReturnQuantity { get; set; }
        public int HoldingStock { get; set; }
        public string ChallanNumber { get; set; }
        public int DefectQuantity { get; set; }
    }

    public class ClsCustomerDeposiit: ClsUser
    {
        public string DepositType { get; set; }
        public double DepositAmount { get; set; }
    }

    public class CustomerReport: ClsCustomerPurchase
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
    }

    public class CustomerReportResponse
    {
        public string LedgerDate { get; set; }
        public string ChallanNumber { get; set; }
        public string CategoryName { get; set; }
        public string FullIssue { get; set; }
        public string ReturnStock { get; set; }
        public string HoldingStock { get; set; }
        public int DefectQuantity { get; set; }
    }
}
