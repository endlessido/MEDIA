using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEDIA.BLL.Entity
{
    public class OrderBusiEntity
    {
        public string OrderId { get; set; }
        public DateTime OrderDate {get;set;}
        public int Quantity { get; set; }
        public string Goodies {get;set;}
        public string UnitPrices {get;set;}
        public decimal? TotalPrice {get;set;}
        public string CurrencyStr {get;set;}
        public bool? IsPayment { get; set; }
        public string Customer { get; set; }
        public string PayType { get; set; }
        public string FirstName { get; set; }
        public string ProjecName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Town { get; set; }
        public string CountryName { get; set; }
    }
}
