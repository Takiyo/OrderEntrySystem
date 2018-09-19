using OrderEntryEngine;
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

        // --- Product Methods
        
        /// <summary>
        /// Adds a product to the list.
        /// </summary>
        /// <param name="product">To be added.</param>
        public void AddProduct(Product product)
        {
            if (this.ContainsProduct(product) == false)
            {
                this.context.Products.Add(product);
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
            return this.GetProduct(product.Id) != null;
        }

        /// <summary>
        /// Wrapper method to retrieve field of products.
        /// </summary>
        /// <returns>The found list.</returns>
        public List<Product> GetProducts()
        {
            return this.context.Products.ToList();
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
                this.context.Customers.Add(customer); ;
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
            return this.GetCustomer(customer.Id) != null;
        }

        /// <summary>
        /// Wrapper method to retrieve field of customers.
        /// </summary>
        /// <returns>The found list.</returns>
        public List<Customer> GetCustomers()
        {
            return this.context.Customers.ToList();
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
                this.context.Locations.Add(location);
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
            return this.GetLocation(location.Id) != null;
        }

        /// <summary>
        /// Wrapper method to retrieve field of locations.
        /// </summary>
        /// <returns>The found list.</returns>
        public List<Location> GetLocations()
        {
            return this.context.Locations.ToList();
        }

        public void SaveToDatabase()
        {
            context.SaveChanges();
        }

        private Product GetProduct(int id)
        {
            return this.context.Products.Find(id);
        }

        private Customer GetCustomer(int id)
        {
            return this.context.Customers.Find(id);
        }

        private Location GetLocation(int id)
        {
            return this.context.Locations.Find(id);
        }
    }
}
