﻿using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryDataAccess
{
    /// <summary>
    /// The class that represents the repository.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// The list of products.
        /// </summary>
        private List<Product> products = new List<Product>();

        /// <summary>
        /// List of customers.
        /// </summary>
        private List<Customer> customers = new List<Customer>();

        /// <summary>
        /// List of locations.
        /// </summary>
        private List<Location> locations = new List<Location>();

        /// <summary>
        /// An event handler to be called when each product is added to the list.
        /// </summary>
        public event EventHandler<ProductEventArgs> ProductAdded;

        /// <summary>
        /// An event handler to be called when each customer is added to the list.
        /// </summary>
        public event EventHandler<CustomerEventArgs> CustomerAdded;

        /// <summary>
        /// An event handler to be called when each location is added to the list.
        /// </summary>
        public event EventHandler<LocationEventArgs> LocationAdded;

        /// <summary>
        /// The application's database context.
        /// </summary>
        public OrderEntryContext context = new OrderEntryContext();

        /// <summary>
        /// Initializes a new instance of the Repository class.
        /// </summary>
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

            // Create & add default customers.
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Guy" },
                new Customer { FirstName = "Dude" },
                new Customer { FirstName = "M'Boy" }
            };

            this.customers.AddRange(customers);

            // Create & add default locations.
            var locations = new List<Location>
            {
                new Location {City = "Stevens Point" },
                new Location {State = "WI" }
            };

            this.locations.AddRange(locations);
        }

        // --- Product Methods
        
        /// <summary>
        /// Adds a product to the list.
        /// </summary>
        /// <param name="product">To be added.</param>
        public void AddProduct(Product product)
        {
            if (this.ContainsProduct(product) == false)
            {
                this.products.Add(product);
                this.ProductAdded?.Invoke(this, new ProductEventArgs(product));
            }
        }

        /// <summary>
        /// Checks if field contains passed in product.
        /// </summary>
        /// <param name="product">To be checked for.</param>
        /// <returns>Bool indicating if list contains product or not.</returns>
        private bool ContainsProduct(Product product)
        {
            return this.products.Contains(product);
        }

        /// <summary>
        /// Wrapper method to retrieve field of products.
        /// </summary>
        /// <returns>The found list.</returns>
        public List<Product> GetProducts()
        {
            return this.products;
        }

        // --- Customer Methods

        /// <summary>
        /// Adds a customer to the list.
        /// </summary>
        /// <param name="customer">To be added.</param>
        public void AddCustomer(Customer customer)
        {
            if (!this.ContainsCustomer(customer))
            {
                this.customers.Add(customer);
                this.CustomerAdded?.Invoke(this, new CustomerEventArgs(customer));
            }
        }

        /// <summary>
        /// Checks if field contains passed in customer.
        /// </summary>
        /// <param name="customer">To be checked for.</param>
        /// <returns>Bool indicating if list contains customer or not.</returns>
        private bool ContainsCustomer(Customer customer)
        {
            return this.customers.Contains(customer);
        }

        /// <summary>
        /// Wrapper method to retrieve field of customers.
        /// </summary>
        /// <returns>The found list.</returns>
        public List<Customer> GetCustomers()
        {
            return this.customers;
        }

        // --- Location Methods


        /// <summary>
        /// Adds a customer to the list.
        /// </summary>
        /// <param name="location">To be added.</param>
        public void AddLocation(Location location)
        {
            if (this.ContainsLocation(location) == false)
            {
                this.locations.Add(location);
                this.LocationAdded?.Invoke(this, new LocationEventArgs(location));
            }
        }

        /// <summary>
        /// Checks if field contains passed in location.
        /// </summary>
        /// <param name="location">To be checked for.</param>
        /// <returns>Bool indicating if list contains location or not.</returns>
        private bool ContainsLocation(Location location)
        {
            return this.locations.Contains(location);
        }

        /// <summary>
        /// Wrapper method to retrieve field of locations.
        /// </summary>
        /// <returns>The found list.</returns>
        public List<Location> GetLocations()
        {
            return this.locations;
        }
    }
}
