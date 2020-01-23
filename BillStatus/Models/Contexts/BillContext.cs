using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillStatus.Models;

namespace BillStatus.Models.Contexts
{
    public class BillContext : DbContext
    {
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<BillPricePart> BillPriceParts { get; set; }
        public DbSet<BillMeasure> BillMeasures { get; set; }

        public BillContext(DbContextOptions<BillContext> options) : base(options) { }

    }
}
