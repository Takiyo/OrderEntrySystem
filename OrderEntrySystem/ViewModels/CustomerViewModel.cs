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
