using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryDataAccess
{
    public class OrderEntryInitializer : DropCreateDatabaseIfModelChanges<OrderEntryContext>
    {
        protected override void Seed(OrderEntryContext context)
        {
            base.Seed(context);

            // Create & add default locations.
            var locations = new List<Location>
            {
                new Location {City = "Stevens Point", Description="ITS POINT", Name="Home", State="WI", Id=1},
                new Location {City = "Waupaca", Description="ITS NOT POINT", Name="Other", State="WI", Id=2}
            };

            context.Locations.AddRange(locations);
            context.SaveChanges();

            // Create & add default products
            var products = new List<Product>
            {
                new Product { Name = "Stuff", LocationId=1, Condition=Condition.Poor, Description="It's stuff.", Price=1, Id=1 },
                new Product { Name = "dasdsa", LocationId=1, Condition=Condition.Poor, Description="It's stuff.", Price=1, Id=2 }
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
        }
    }
}
