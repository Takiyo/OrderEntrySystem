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

        public event EventHandler<ProductEventArgs> ProductAdded;

        public Repository()
        {
            var products = new List<Product>
            {
                new Product { Name = "Stuff" },
                new Product { Name = "Things" },
                new Product { Name = "Malarky" }
            };

            this.products.AddRange(products);

            this.ProductAdded(this, new ProductEventArgs(products));

            var customers = new List<Customer>
            {
                new Customer { FirstName = "Guy" },
                new Customer { FirstName = "Dude" },
                new Customer { FirstName = "M'Boy" }
            };

            this.products.AddRange(products);
        }

        //products
        
        public void AddProduct(Product product)
        {
            if (this.ContainsProduct(product) == false)
            {
                products.Add(product);
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

        //customers

        public void AddCustomer(Customer customer)
        {
            if (this.ContainsCustomer(customer) == false)
            {
                customers.Add(customer);
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
    }
}
