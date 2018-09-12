using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    public class CustomerViewModel : WorkspaceViewModel
    {

        private Customer customer;

        private Repository repository;

        private ICommand saveCommand;

        private bool isSelected;

        public CustomerViewModel(Customer customer, Repository repository) : base("Customer")
        {
            this.customer = customer;
            this.repository = repository;
        }

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

        protected override void CreateCommands()
        {
        }

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

        public void Save()
        {
            this.repository.AddCustomer(this.customer);
        }
    }
}
