using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.API.Entities;

namespace CustomerService.API.DBContext
{
    public class BankMgmtDBContext:DbContext
    {
        public BankMgmtDBContext(DbContextOptions<BankMgmtDBContext> options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<testme> testmes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
