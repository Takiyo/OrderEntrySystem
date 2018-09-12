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
    public class MultiProductViewModel : WorkspaceViewModel
    {

        private Repository repository;

        public MultiProductViewModel(Repository repository) : base("Products") //placeholder
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
