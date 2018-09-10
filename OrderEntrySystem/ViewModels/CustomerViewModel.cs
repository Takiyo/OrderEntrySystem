using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class CustomerViewModel : WorkspaceViewModel
    {

        private Customer customer;

        public CustomerViewModel(Customer customer) : base("Customer")
        {
            this.customer = customer;
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
    }
}
