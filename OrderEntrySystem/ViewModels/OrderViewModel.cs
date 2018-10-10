using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class OrderViewModel : WorkspaceViewModel
    {
        private Order order;

        private Repository repository;

        private bool isSelected;

        private MultiOrderLineViewModel filteredLineViewModel;

        public OrderViewModel(Order order, Repository repository)
            : base("Order")
        {
            this.repository = repository;
            this.order = order;
            this.filteredLineViewModel = new MultiOrderLineViewModel(this.repository, this.order);
            this.filteredLineViewModel.AllLines = this.FilteredLines;
        }

        public MultiOrderLineViewModel FilteredLineViewModel
        {
            get
            {
                return this.filteredLineViewModel;
            }
        }

        public ObservableCollection<OrderLineViewModel> FilteredLines
        {
            get
            {
                var lines =
                  (from o in this.order.Lines
                   select new OrderLineViewModel(o, this.repository)).ToList();

                this.FilteredLineViewModel.AddPropertyChangedEvent(lines);

                return new ObservableCollection<OrderLineViewModel>(lines);
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


        public OrderStatus Status
        {
            get
            {
                return this.order.Status;
            }

            set
            {
                this.order.Status = value;
                this.OnPropertyChanged("Status");
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
                this.OnPropertyChanged("Customer");
            }
        }

        public IEnumerable<OrderStatus> OrderStatuses
        {
            get
            {
                return Enum.GetValues(typeof(OrderStatus)) as IEnumerable<OrderStatus>;
            }
        }

        /// <summary>
        /// Saves the customers.
        /// </summary>
        private void Save()
        {
            this.repository.AddOrder(this.order);

            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// This is a protected overridden method.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
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
