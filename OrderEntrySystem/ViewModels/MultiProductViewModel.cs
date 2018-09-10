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
        public MultiProductViewModel() : base("Products") //placeholder
        {
            AllProducts = new ObservableCollection<ProductViewModel>();
            ProductViewModel pvm1 = new ProductViewModel(new Product());
            ProductViewModel pvm2 = new ProductViewModel(new Product());
            ProductViewModel pvm3 = new ProductViewModel(new Product());

            AllProducts.Add(pvm1);
            AllProducts.Add(pvm2);
            AllProducts.Add(pvm3);
        }

        public ObservableCollection<ProductViewModel> AllProducts
        {
            get
            {
                return null; //placeholder
            }
            private set
            {

            }
        }

        protected override void CreateCommands()
        {

        }
    }
}
