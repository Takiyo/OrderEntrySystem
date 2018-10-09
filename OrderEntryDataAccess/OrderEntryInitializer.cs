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

            // Create & add default categories
            var categories = new List<Category>
            {
                new Category{ Name = "Cool Junk", Id=1},
                new Category{ Name = "Jank Junk", Id=2}
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Create & add default products
            var products = new List<Product>
            {
                new Product { Name = "Thing", LocationId=1, Condition=Condition.Poor, Description="It's stuff.", Price=1, Id=1, CategoryId=1 },
                new Product { Name = "Item?", LocationId=1, Condition=Condition.Poor, Description="It's stuff.", Price=1, Id=2, CategoryId=2 }
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

            // Create and add default orders.
            var orders = new List<Order>
            {
                new Order { CustomerId = 1 },
                new Order { CustomerId = 2 },
                new Order { CustomerId = 3 }
            };
        }
    }
}
