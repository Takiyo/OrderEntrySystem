using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class OrderViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The order being shown.
        /// </summary>
        private Order order;

        /// <summary>
        /// The order view model's database repository.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// An indicator of whether or not an order is selected.
        /// </summary>
        private bool isSelected;

        private MultiOrderLineViewModel filteredLineViewModel;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="order">The order to be shown.</param>
        /// <param name="repository">The order repository.</param>
        public OrderViewModel(Order order, Repository repository)
            : base("New order")
        {
            this.order = order;
            this.repository = repository;
            this.filteredLineViewModel = new MultiOrderLineViewModel(this.repository, this.order);
            this.filteredLineViewModel.AllLines = this.FilteredLines;
        }

        public Order Order
        {
            get
            {
                return this.order;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this order is selected in the UI.
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

        public ObservableCollection<OrderLineViewModel> FilteredLines
        {
            get
            {
                List<OrderLineViewModel> lines = null;

                if (this.order.Lines != null)
                {
                    lines =
                        (from l in this.order.Lines
                         where !l.IsArchived
                         select new OrderLineViewModel(l, this.repository)).ToList();
                }

                this.FilteredLineViewModel.AddPropertyChangedEvent(lines);

                return new ObservableCollection<OrderLineViewModel>(lines);
            }
        }

        public MultiOrderLineViewModel FilteredLineViewModel
        {
            get
            {
                return this.filteredLineViewModel;
            }
        }

        public OrderStatus Status
        {
            get
            {
                return this.order.Status;
            }
            set
            {
                this.order.Status = value;
            }
        }

        public Customer Customer
        {
            get
            {
                return this.order.Customer;
            }
            set
            {
                this.order.Customer = value;
            }
        }

        public IEnumerable<OrderStatus> OrderStatuses
        {
            get
            {
                return Enum.GetValues(typeof(OrderStatus)) as IEnumerable<OrderStatus>;
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return this.repository.GetProducts();
            }
        }

        /// <summary>
        /// Creates the commands needed for the order view model.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Saves the order view model's order to the repository.
        /// </summary>
        private void Save()
        {
            // Add order to repository.
            this.repository.AddOrder(this.order);

            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// Saves the order and closes the new order window.
        /// </summary>
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Closes the new order window without saving.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}