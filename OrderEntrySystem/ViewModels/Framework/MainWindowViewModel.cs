using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OrderEntrySystem
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        private ObservableCollection<WorkspaceViewModel> viewModels;

        private Repository repository;

        public MainWindowViewModel() : base("Order Entry System - Brytowski")
        {
            this.repository = new Repository();
        }

        public ObservableCollection<WorkspaceViewModel> ViewModels
        {
            get
            {
                if (this.viewModels == null)
                {
                    this.viewModels = new ObservableCollection<WorkspaceViewModel>();
                }

                return this.viewModels;
            }
        }

        public void CreateNewProduct()
        {
            Product product = new Product { Location = "Main Warehouse" };
            ProductViewModel pvm = new ProductViewModel(product, this.repository);

            pvm.RequestClose += OnWorkspaceRequestClose;

            viewModels.Add(pvm);
            this.ActivateViewModel(pvm);
        }

        public void CreateNewCustomer()
        {
            Customer customer = new Customer();
            CustomerViewModel cvm = new CustomerViewModel(customer, this.repository);
            cvm.RequestClose += this.OnWorkspaceRequestClose;

            viewModels.Add(cvm);
            this.ActivateViewModel(cvm);
        }

        public void CreateNewLocation()
        {
            Location location = new Location();
            LocationViewModel lvm = new LocationViewModel(location, this.repository);
            lvm.RequestClose += this.OnWorkspaceRequestClose;

            viewModels.Add(lvm);
            this.ActivateViewModel(lvm);
        }

        private void ActivateViewModel(WorkspaceViewModel viewModel)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.viewModels);

            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(viewModel);
            }
        }

        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            this.ViewModels.Remove(sender as WorkspaceViewModel);
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New Product", new DelegateCommand(p => this.CreateNewProduct())));
            this.Commands.Add(new CommandViewModel("New Customer", new DelegateCommand(p => this.CreateNewCustomer())));
            this.Commands.Add(new CommandViewModel("New Location", new DelegateCommand(p => this.CreateNewLocation())));
            this.Commands.Add(new CommandViewModel("View All Products", new DelegateCommand(p => this.ShowAllProducts())));
            this.Commands.Add(new CommandViewModel("View All Customers", new DelegateCommand(p => this.ShowAllCustomers())));
            this.Commands.Add(new CommandViewModel("View All Locations", new DelegateCommand(p => this.ShowAllLocations())));
        }

        public void ShowAllProducts()
        {
            MultiProductViewModel viewModel = this.ViewModels.FirstOrDefault
                (vm => vm is MultiProductViewModel) as MultiProductViewModel;
            if (viewModel == null)
            {
                viewModel = new MultiProductViewModel(this.repository);
                viewModel.RequestClose += OnWorkspaceRequestClose;
            }

            this.viewModels.Add(viewModel);
            this.ActivateViewModel(viewModel);
        }

        public void ShowAllCustomers()
        {
            MultiCustomerViewModel viewModel = this.ViewModels.FirstOrDefault
                (vm => vm is MultiCustomerViewModel) as MultiCustomerViewModel;
            if (viewModel == null)
            {
                viewModel = new MultiCustomerViewModel(this.repository);
                viewModel.RequestClose += OnWorkspaceRequestClose;
            }

            this.viewModels.Add(viewModel);
            this.ActivateViewModel(viewModel);
        }

        public void ShowAllLocations()
        {
            MultiLocationViewModel viewModel = this.ViewModels.FirstOrDefault
                (vm => vm is MultiLocationViewModel) as MultiLocationViewModel;
            if (viewModel == null)
            {
                viewModel = new MultiLocationViewModel(this.repository);
                viewModel.RequestClose += OnWorkspaceRequestClose;
            }

            this.viewModels.Add(viewModel);
            this.ActivateViewModel(viewModel);
        }
    }
}
