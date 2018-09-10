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

        public MultiCustomerViewModel(Repository repository) : base("Customers") //placeholder
        {
            //this.AllCustomers = new ObservableCollection<CustomerViewModel>();
            //CustomerViewModel cvm1 = new CustomerViewModel(new Customer());
            //CustomerViewModel cvm2 = new CustomerViewModel(new Customer());
            //CustomerViewModel cvm3 = new CustomerViewModel(new Customer());

            //AllCustomers.Add(cvm1);
            //AllCustomers.Add(cvm2);
            //AllCustomers.Add(cvm3);
            this.repository = repository;

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
    }
}
