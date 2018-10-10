using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    /// <summary>
    /// Class used to represent a customer view model.
    /// </summary>
    public class CustomerViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The vm's customer field.
        /// </summary>
        private Customer customer;

        /// <summary>
        /// The vm's repository field.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// The save command functionality.
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// Indicates whether the item is selected.
        /// </summary>
        private bool isSelected;

        private MultiOrderViewModel filteredOrderViewModel;

        /// <summary>
        /// Initializes a new instance of the CustomerViewModel class.
        /// </summary>
        /// <param name="customer">Customer object to be shown in the vm.</param>
        /// <param name="repository">The repository that customers are saved and loaded to.</param>
        public CustomerViewModel(Customer customer, Repository repository) : base("Customer")
        {
            this.customer = customer;
            this.repository = repository;

            this.filteredOrderViewModel = new MultiOrderViewModel(this.repository, this.customer);
            this.filteredOrderViewModel.AllOrders = FilteredOrders;
        }

        public MultiOrderViewModel FilteredOrderViewModel
        {
            get
            {
                return this.filteredOrderViewModel;
            }
        }

        public ObservableCollection<OrderViewModel> FilteredOrders
        {
            get
            {
                var orders =
                  (from o in this.customer.Orders
                   select new OrderViewModel(o, this.repository)).ToList();

                this.FilteredOrderViewModel.AddPropertyChangedEvent(orders);

                return new ObservableCollection<OrderViewModel>(orders);
            }

        }
    

        /// <summary>
        /// Gets or sets the customer's FirstName field.
        /// </summary>
        public string FirstName
        {
            get
            {
                return customer.FirstName;
            }
            set
            {
                customer.FirstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Gets or sets the customer's LastName field.
        /// </summary>
        public string LastName
        {
            get
            {
                return customer.LastName;
            }
            set
            {
                customer.LastName = value;
                this.OnPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Gets or sets the customer's Phone field.
        /// </summary>
        public string Phone
        {
            get
            {
                return customer.Phone;
            }
            set
            {
                customer.Phone = value;
                this.OnPropertyChanged("Phone");
            }
        }

        /// <summary>
        /// Gets or sets the customer's Email field.
        /// </summary>
        public string Email
        {
            get
            {
                return customer.Email;
            }
            set
            {
                customer.Email = value;
                this.OnPropertyChanged("Email");
            }
        }

        /// <summary>
        /// Gets or sets the customer's Address field.
        /// </summary>
        public string Address
        {
            get
            {
                return customer.Address;
            }
            set
            {
                customer.Address = value;
                this.OnPropertyChanged("Address");
            }
        }

        /// <summary>
        /// Gets or sets the customer's City field.
        /// </summary>
        public string City
        {
            get
            {
                return customer.City;
            }
            set
            {
                customer.City = value;
                this.OnPropertyChanged("City");
            }
        }

        /// <summary>
        /// Gets or sets the customer's State field.
        /// </summary>
        public string State
        {
            get
            {
                return customer.State;
            }
            set
            {
                customer.State = value;
                this.OnPropertyChanged("State");
            }
        }

        /// <summary>
        /// Gets or sets the customer's IsSelected field.
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

        /// <summary>
        /// Overrides the WorkSpaceViewModel's create commands method.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Gets customer view model's save command.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new DelegateCommand(p => this.Save());
                }

                return this.saveCommand;
            }
        }

        /// <summary>
        /// Adds the selected customer object to the repository's saved list of customers.
        /// </summary>
        public void Save()
        {
            this.repository.AddCustomer(this.customer);
            this.repository.SaveToDatabase();
        }
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}
