using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    /// <summary>
    /// The class representing event arguments for the customer.
    /// </summary>
    public class CustomerEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CustomerEventArgs class.
        /// </summary>
        /// <param name="customer">The customer to be created.</param>
        public CustomerEventArgs(Customer customer)
        {
            this.Customer = customer;
        }

        /// <summary>
        /// A customer property.
        /// </summary>
        public Customer Customer{ get; private set; }
    }
}
