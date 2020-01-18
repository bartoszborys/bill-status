using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillStatus.Models
{
    public class BillType
    {
        public int Id { get; set; }
        public string Name { get; set; }    
    }

    public class BillTypeStore : DbContext
    {
        public DbSet<BillType> BillTypes { get; set; }

        public BillTypeStore(DbContextOptions<BillTypeStore> options) : base(options) {}
    }
}