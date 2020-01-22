using System;

namespace BillStatus.Models
{
    public class Measure
    {
        public int Id { get; set; }
        public BillType Type { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}