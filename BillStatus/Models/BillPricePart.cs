using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillStatus.Models
{
    public class BillPricePart
    {
        public int Id { get; set; }
        public int TypeID { get; set; }
        public BillType Type { get; set; }
        public double Value { get; set; }
        public bool Constant { get; set; }
    }
}
