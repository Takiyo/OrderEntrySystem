using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    /// <summary>
    /// Class used to represent a location.
    /// </summary>
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
