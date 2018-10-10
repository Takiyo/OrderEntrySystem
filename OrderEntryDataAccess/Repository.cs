using OrderEntryEngine;
using OrderEntryEngine.EventArgs;
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

        public event EventHandler<CategoryEventArgs> CategoryAdded;

        public event EventHandler<OrderEventArgs> OrderAdded;

        public event EventHandler<OrderLineEventArgs> OrderLineAdded;

        /// <summary>
        /// When you add order lines.
        /// </summary>
        /// <param name="line">The order line being added.</param>
        public void AddOrderLine(OrderLine line)
        {
            if (!this.ContainsLines(line))
            {
                this.context.OrderLines.Add(line);

                if (this.OrderLineAdded != null)
                {
                    this.OrderLineAdded(this, new OrderLineEventArgs(line));
                }
            }
        }

        /// <summary>
        /// Gets the list of customers.
        /// </summary>
        /// <returns>The list of customers.</returns>
        /// <param name="id">The id of the customer.</param>
        private OrderLine GetOrderLines(int id)
        {
            return this.context.OrderLines.Find(id);
        }


        /// <summary>
        /// Checks if contains a location.
        /// </summary>
        /// <param name="location">The location being checked.</param>
        /// <returns>True or false.</returns>
        private bool ContainsLines(OrderLine line)
        {
            return this.GetOrderLines(line.Id) != null;
        }



        /// <summary>
        /// The application's database context.
        /// </summary>
        public OrderEntryContext context = new OrderEntryContext();

        public void AddOrder(Order order)
        {
            if (this.ContainsOrder(order) == false)
            {
                this.context.Orders.Add(order);
                this.OrderAdded?.Invoke(this, new OrderEventArgs(order));
            }
        }

        private bool ContainsOrder(Order order)
        {
            return this.GetOrder(order.Id) != null;
        }

        public List<Order> GetOrders()
        {
            return this.context.Orders.ToList();
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

        // --- Category Methods

        public void AddCategory(Category category)
        {
            if (!this.ContainsCategory(category))
            {
                this.context.Categories.Add(category); ;
                this.CategoryAdded?.Invoke(this, new CategoryEventArgs(category));
            }
        }

        private bool ContainsCategory(Category category)
        {
            return this.GetCategory(category.Id) != null;
        }

        public List<Category> GetCategories()
        {
            return this.context.Categories.ToList();
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

        private Category GetCategory(int id)
        {
            return this.context.Categories.Find(id);
        }

        private Location GetLocation(int id)
        {
            return this.context.Locations.Find(id);
        }

        private Order GetOrder(int id)
        {
            return this.context.Orders.Find(id);
        }
    }
}
