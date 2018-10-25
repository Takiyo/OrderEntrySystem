using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderEntryEngine
{
    public class Order
    {
        private decimal shippingAmount;

        private decimal productTotal;

        private decimal taxTotal;

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

        public decimal ShippingAmount
        {
            get
            {
                return this.shippingAmount;
            }
            set
            {
                this.shippingAmount = Math.Round(value);
            }
        }

        public decimal ProductTotal
        {
            get
            {
                return this.productTotal;
            }
            set
            {
                this.productTotal = Math.Round(value);
            }
        }

        public decimal TaxTotal
        {
            get
            {
                return this.taxTotal;
            }
            set
            {
                this.taxTotal = Math.Round(value);
            }
        }

        public decimal Total
        {
            get
            {
                return this.ProductTotal + this.TaxTotal + this.ShippingAmount;
            }
        }

        public void CalculateTotals()
        {
            this.ProductTotal = this.Lines.Where(l => !l.IsArchived).Sum(l => l.ExtendedProductAmount);
            this.TaxTotal = this.Lines.Where(l => !l.IsArchived).Sum(l => l.ExtendedTax);
        }

        public void Post()
        {
            if (this.Status == OrderStatus.Processing)
            {
                this.Status = OrderStatus.Shipped;
                this.CalculateTotals();
            }
        }
    }
}