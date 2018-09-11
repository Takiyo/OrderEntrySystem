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
    public class MultiProductViewModel : WorkspaceViewModel
    {

        private Repository repository;

        public MultiProductViewModel(Repository repository) : base("Products") //placeholder
        {
            this.repository = repository;
            this.repository.ProductAdded += OnProductAdded;

            IEnumerable<ProductViewModel> products =
    from p in this.repository.GetProducts()
    select new ProductViewModel(p, this.repository);

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
            this.AllProducts.Add(viewModel);
        }
    }
}
