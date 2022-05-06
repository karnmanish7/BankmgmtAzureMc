using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.DBContext
{
    public class BankMgmtDBContext:DbContext
    {
        public BankMgmtDBContext(DbContextOptions<BankMgmtDBContext> options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
