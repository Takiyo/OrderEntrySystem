using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine.EventArgs
{
    public class CategoryEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CategoryEventArgs class.
        /// </summary>
        /// <param name="customer">The category to be created.</param>
        public CategoryEventArgs(Category category)
        {
            this.Category = category;
        }

        public Category Category { get; private set; }
    }
}
