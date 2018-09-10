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

        public MainWindowViewModel() : base("Order Entry System - Brytowski")
        {

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
            ProductViewModel pvm = new ProductViewModel(product);

            pvm.RequestClose += OnWorkspaceRequestClose;

            viewModels.Add(pvm);
            this.ActivateViewModel(pvm);
        }

        public void CreateNewCustomer()
        {
            Customer customer = new Customer();
            CustomerViewModel cvm = new CustomerViewModel(customer);
            cvm.RequestClose += this.OnWorkspaceRequestClose;

            viewModels.Add(cvm);
            this.ActivateViewModel(cvm);
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
        }
    }
}
