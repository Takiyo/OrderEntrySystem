using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            List<CustomerViewModel> customers =
    (from c in this.repository.GetCustomers()
     select new CustomerViewModel(c, this.repository)).ToList();

            customers.ForEach(cvm => cvm.PropertyChanged += this.OnCustomerViewModelPropertyChanged);
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
            viewModel.PropertyChanged += OnCustomerViewModelPropertyChanged;
            this.AllCustomers.Add(viewModel);
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllCustomers.Count(vm => vm.IsSelected);
            }
        }

        private void OnCustomerViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }
    }
}
