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
    public class MultiProductViewModel : WorkspaceViewModel
    {

        private Repository repository;

        public MultiProductViewModel(Repository repository) : base("Products")
        {
            this.repository = repository;
            this.repository.ProductAdded += OnProductAdded;
           

            List<ProductViewModel> products =
                (from p in this.repository.GetProducts()
                select new ProductViewModel(p, this.repository)).ToList();

            products.ForEach(pvm => pvm.PropertyChanged += this.OnProductViewModelPropertyChanged);
            this.AllProducts = new ObservableCollection<ProductViewModel>(products);
        }

        public ObservableCollection<ProductViewModel> AllProducts
        {
            get; private set;
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewProductExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditProductExecute())));
        }

        private void CreateNewProductExecute()
        {
            ProductViewModel viewModel = new ProductViewModel(new Product(), this.repository);

            ShowProduct(viewModel);
        }

        private static void ShowProduct(ProductViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            ProductView view = new ProductView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        private void EditProductExecute()
        {
            ProductViewModel viewModel = this.AllProducts.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                ShowProduct(viewModel);
            }
            else
            {
                MessageBox.Show("Select only one product to edit it.");
            }
        }

        private void OnProductAdded(object sender, ProductEventArgs e)
        {

            ProductViewModel viewModel = new ProductViewModel(e.Product, this.repository);
            viewModel.PropertyChanged += OnProductViewModelPropertyChanged;
            this.AllProducts.Add(viewModel);
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllProducts.Count(vm => vm.IsSelected);
            }
        }

        private void OnProductViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }
    }
}
