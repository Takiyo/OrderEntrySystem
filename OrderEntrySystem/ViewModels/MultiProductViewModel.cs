using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class MultiProductViewModel
    {
        public MultiProductViewModel()
        {
            AllProducts = new ObservableCollection<ProductViewModel>();
            ProductViewModel pvm1 = new ProductViewModel();
        }

        public ObservableCollection<ProductViewModel> AllProducts
        {
            get
            {

            }
            private set
            {

            }
        }

        protected void CreateCommands()
        {

        }
    }
}
