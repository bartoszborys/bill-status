using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillStatus.Models
{
    public class NewBillTypeDetails
    {
        public BillType Type { get; set; }
        public List<BillPricePart> PriceParts { get; set; }
    }
}
