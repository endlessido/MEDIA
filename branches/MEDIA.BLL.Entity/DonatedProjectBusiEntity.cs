using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEDIA.BLL.Entity
{
    public class DonatedProjectBusiEntity
    {
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectSummary { get; set; }
        public decimal? SumDonateFunding { get; set; }
        public string CurrencyStr { get; set; }
        public string ImgUrl { get; set; }
    }
}
