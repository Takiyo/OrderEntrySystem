using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryDataAccess
{
    public class OrderEntryInitializer : DropCreateDatabaseAlways<OrderEntryContext>
    {
        protected override void Seed(OrderEntryContext context)
        {
            base.Seed(context);

            // Create & add default products
            var products = new List<Product>
            {
                new Product { Name = "Stuff" },
                new Product { Name = "Things" },
                new Product { Name = "Malarky" }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            // Create & add default customers.
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Guy" },
                new Customer { FirstName = "Dude" },
                new Customer { FirstName = "M'Boy" }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();


            // Create & add default locations.
            var locations = new List<Location>
            {
                new Location {City = "Stevens Point" },
                new Location {State = "WI" }
            };

            context.Locations.AddRange(locations);
            context.SaveChanges();
        }
    }
}
