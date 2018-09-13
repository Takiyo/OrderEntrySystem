using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    /// <summary>
    /// The class representing event arguments for the Product.
    /// </summary>
    public class ProductEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ProductEventArgs class.
        /// </summary>
        /// <param name="product">Product to be created.</param>
        public ProductEventArgs(Product product)
        {
            this.Product = product;
        }

        /// <summary>
        /// A product property.
        /// </summary>
        public Product Product { get; private set; }
    }
}
