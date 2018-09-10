using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class ProductViewModel : WorkspaceViewModel
    {
        private Product product;

        public ProductViewModel(Product product) : base("Product")
        {
            this.product = product;
        }

        public string Location
        {
            get
            {
                return this.product.Location;
            }
            set
            {
                this.product.Location = value;
            }
        }

        public string Name
        {
            get
            {
                return this.product.Name;
            }

            set
            {
                this.product.Name = value;
            }
        }

        public string Description
        {
            get
            {
                return this.product.Description;
            }

            set
            {
                this.product.Description = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.product.Price;
            }

            set
            {
                this.product.Price = value;
            }
        }

        protected override void CreateCommands()
        {
        }
    }
}
