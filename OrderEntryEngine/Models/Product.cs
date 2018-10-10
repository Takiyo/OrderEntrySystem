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

        public virtual ICollection<OrderLine> Orders { get; set; }

        [Key]
        public int Id { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
