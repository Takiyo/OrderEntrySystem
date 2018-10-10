using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class OrderLineEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CategoryEventArgs class.
        /// </summary>
        /// <param name="order">THe category being created.</param>
        public OrderLineEventArgs(OrderLine line)
        {
            this.Line = line;
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public OrderLine Line { get; set; }
    }
}
