using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class OrderLineViewModel : WorkspaceViewModel
    {
        private OrderLine line;

        private Repository repository;

        /// <summary>
        /// The is selected field. 
        /// </summary>
        private bool isSelected;

        public OrderLineViewModel(OrderLine line, Repository repository)
            : base("Order Line")
        {
            this.line = line;
            this.repository = repository;
        }

        public Order Order
        {
            get
            {
                return this.line.Order;
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether its selected or not.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        public Product Product
        {
            get
            {
                return this.line.Product;
            }

            set
            {
                this.line.Product = value;
                this.OnPropertyChanged("Product");
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return this.repository.GetProducts();
            }
        }

        public int Quantity { get; set; }


        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Saves the order lines.
        /// </summary>
        public void Save()
        {
            this.repository.AddOrder(this.Order);
            this.repository.AddOrderLine(this.line);
            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// This is the OK execute.
        /// </summary>
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// This is the cancel execute.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}
