using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillStatus.Models;

namespace BillStatus.Models.Contexts
{
    public class BillTypeContext : DbContext
    {
        public DbSet<BillType> BillTypes { get; set; }

        public BillTypeContext(DbContextOptions<BillTypeContext> options) : base(options) { }

        public DbSet<BillStatus.Models.BillPricePart> BillPricePart { get; set; }
    }
}
