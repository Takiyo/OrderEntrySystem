using System.Collections.Generic;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class OrderLineViewModel : WorkspaceViewModel
    {
        private OrderLine line;

        /// <summary>
        /// The order line view model's database repository.
        /// </summary>
        private Repository repository;

        private bool isSelected;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="line">The line to be shown.</param>
        /// <param name="repository">The order line repository.</param>
        public OrderLineViewModel(OrderLine line, Repository repository)
            : base("New order line")
        {
            this.line = line;
            this.repository = repository;
        }

        public OrderLine Line
        {
            get
            {
                return this.line;
            }
        }

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

        public int Quantity
        {
            get
            {
                return this.line.Quantity;
            }
            set
            {
                this.line.Quantity = value;
                this.OnPropertyChanged("Quantity");
            }
        }

        public decimal ProductPrice
        {
            get
            {
                return this.line.Product.Price;
            }
        }

        public string ProductDescription
        {
            get
            {
                return this.line.Product.Description;
            }
        }

        /// <summary>
        /// Creates the commands needed for the order line view model.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Saves the view model's order line to the repository.
        /// </summary>
        private void Save()
        {
            // This is a hack, and will add once for each line, but EF doesn't allow the same instance to be added multiple times.
            this.repository.AddOrder(this.line.Order);

            // Add line to repository.
            this.repository.AddLine(this.line);

            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// Saves the order line and closes the new order line window.
        /// </summary>
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Closes the new order line window without saving.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}