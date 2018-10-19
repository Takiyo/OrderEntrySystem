using System.Collections.Generic;

namespace OrderEntryEngine
{
    public class Order
    {
        public Order()
        {
            this.Lines = new List<OrderLine>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderLine> Lines { get; set; }

        public OrderStatus Status { get; set; }

        public bool IsArchived { get; set; }
    }
}