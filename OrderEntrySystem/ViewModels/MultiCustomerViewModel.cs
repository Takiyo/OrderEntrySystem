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
    public class MultiCustomerViewModel : WorkspaceViewModel
    {
        private Repository repository;

        public MultiCustomerViewModel(Repository repository) : base("Customers")
        {
            this.repository = repository;
            this.repository.CustomerAdded += OnCustomerAdded;

            IEnumerable<CustomerViewModel> customers =
    from p in this.repository.GetCustomers()
    select new CustomerViewModel(p, this.repository);

            this.AllCustomers = new ObservableCollection<CustomerViewModel>(customers);

        }

        public ObservableCollection<CustomerViewModel> AllCustomers
        {
            get; private set;
        }

        protected override void CreateCommands()
        {

        }

        private void OnCustomerAdded(object sender, CustomerEventArgs e)
        {
            CustomerViewModel viewModel = new CustomerViewModel(e.Customer, this.repository);
            this.AllCustomers.Add(viewModel);
        }
    }
}
