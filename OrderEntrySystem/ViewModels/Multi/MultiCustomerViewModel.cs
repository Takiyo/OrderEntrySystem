using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrderEntrySystem
{
    /// <summary>
    /// Class used to represent seeing multiple customers at once in the view model.
    /// </summary>
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
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewCustomerExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditCustomerExecute())));
        }

        private void CreateNewCustomerExecute()
        {
            CustomerViewModel viewModel = new CustomerViewModel(new Customer(), this.repository);

            ShowCustomer(viewModel);
        }

        private static void ShowCustomer(CustomerViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            CustomerView view = new CustomerView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        private void EditCustomerExecute()
        {
            CustomerViewModel viewModel = this.AllCustomers.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                ShowCustomer(viewModel);
            }
            else
            {
                MessageBox.Show("Select only one customer to edit it.");
            }
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
