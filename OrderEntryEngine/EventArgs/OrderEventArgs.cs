using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class OrderEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CategoryEventArgs class.
        /// </summary>
        /// <param name="customer">The category to be created.</param>
        public OrderEventArgs(Order order)
        {
            this.Order = order;
        }

        public Order Order { get; private set; }
    }
}
