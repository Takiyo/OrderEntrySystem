using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryDataAccess
{
    public class Repository
    {
        private List<Product> products = new List<Product>();

        private List<Customer> customers = new List<Customer>();

        private List<Location> locations = new List<Location>();

        public event EventHandler<ProductEventArgs> ProductAdded;

        public event EventHandler<CustomerEventArgs> CustomerAdded;

        public event EventHandler<LocationEventArgs> LocationAdded;

        public Repository()
        {
            // Create & add default products
            var products = new List<Product>
            {
                new Product { Name = "Stuff" },
                new Product { Name = "Things" },
                new Product { Name = "Malarky" }
            };
            
            this.products.AddRange(products);

            // Run ProductAdded event for each product in products list.
            if (this.ProductAdded != null)
            {
                foreach (Product p in products)
                {
                    this.ProductAdded(this, new ProductEventArgs(p));
                }
            }

            // Create & add default customers.
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Guy" },
                new Customer { FirstName = "Dude" },
                new Customer { FirstName = "M'Boy" }
            };

            this.customers.AddRange(customers);

            // Run CustomerAdded event for each customer in customers list.
            if (this.CustomerAdded != null)
            {
                foreach (Customer c in customers)
                {
                    this.CustomerAdded(this, new CustomerEventArgs(c));
                }
            }

            // Create & add default locations.
            var locations = new List<Location>
            {
                new Location {City = "Stevens Point" },
                new Location {State = "WI" }
            };

            this.locations.AddRange(locations);

            // Run LocationAdded event for each location in locations list.
            if (this.LocationAdded != null)
            {
                foreach (Location l in locations)
                {
                    this.LocationAdded(this, new LocationEventArgs(l));
                }
            }
        }

        // product
        
        public void AddProduct(Product product)
        {
            if (this.ContainsProduct(product) == false)
            {
                this.products.Add(product);
            }
        }

        private bool ContainsProduct(Product product)
        {
            return this.products.Contains(product);
        }

        public List<Product> GetProducts()
        {
            return this.products;
        }

        // customer

        public void AddCustomer(Customer customer)
        {
            if (this.ContainsCustomer(customer) == false)
            {
                this.customers.Add(customer);
            }
        }

        private bool ContainsCustomer(Customer customer)
        {
            return this.customers.Contains(customer);
        }

        public List<Customer> GetCustomers()
        {
            return this.customers;
        }

        // location
        public void AddLocation(Location location)
        {
            if (this.ContainsLocation(location) == false)
            {
                this.locations.Add(location);
            }
        }

        private bool ContainsLocation(Location location)
        {
            return this.locations.Contains(location);
        }

        public List<Location> GetLocations()
        {
            return this.locations;
        }
    }
}
