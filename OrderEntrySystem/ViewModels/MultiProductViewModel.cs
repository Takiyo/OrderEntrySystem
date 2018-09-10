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


            //ProductViewModel pvm1 = new ProductViewModel(new Product(), this.repository);
            //ProductViewModel pvm2 = new ProductViewModel(new Product(), this.repository);
            //ProductViewModel pvm3 = new ProductViewModel(new Product(), this.repository);
            //AllProducts.Add(pvm1);
            //AllProducts.Add(pvm2);
            //AllProducts.Add(pvm3);

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
    }
}
