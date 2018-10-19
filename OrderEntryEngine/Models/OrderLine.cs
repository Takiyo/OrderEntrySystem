using System.ComponentModel.DataAnnotations;

namespace OrderEntryEngine
{
    public class OrderLine
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public bool IsArchived { get; set; }
    }
}