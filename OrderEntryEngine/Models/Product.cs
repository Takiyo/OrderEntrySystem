using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    /// <summary>
    /// Class used to represent a product.
    /// </summary>
    public class Product
    {
        public Condition Condition { get; set; }

        public int Id { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        [Required]
        [MaxLength(101)]
        public string Name { get; set; }

        [MaxLength(501)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
