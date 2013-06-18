using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEDIA.BLL.Entity
{
    public class DonatedRecordBusiEntity
    {
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string CurrencyStr { get; set; }
        public decimal? DonateFunding { get; set; }
        public DateTime? DonateDate { get; set; }
        public bool? IsPayment { get; set; }
    }
}
