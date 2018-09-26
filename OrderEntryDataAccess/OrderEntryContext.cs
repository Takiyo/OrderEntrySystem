using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryDataAccess
{
    /// <summary>
    /// Class used to represent database context for our order entry data.
    /// </summary>
    public class OrderEntryContext : DbContext
    {
        public OrderEntryContext() : base("OrderEntryContext")
        {
            Database.Initialize(true);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
