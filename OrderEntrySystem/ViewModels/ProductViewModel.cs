using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class ProductViewModel : WorkspaceViewModel
    {
        private Product product;

        private Repository repository;

        private ICommand saveCommand;

        public ProductViewModel(Product product, Repository repository) : base("Product")
        {
            this.product = product;
            this.repository = repository;
        }

        public string Location
        {
            get
            {
                return this.product.Location;
            }
            set
            {
                this.product.Location = value;
            }
        }

        public string Name
        {
            get
            {
                return this.product.Name;
            }

            set
            {
                this.product.Name = value;
            }
        }

        public string Description
        {
            get
            {
                return this.product.Description;
            }

            set
            {
                this.product.Description = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.product.Price;
            }

            set
            {
                this.product.Price = value;
            }
        }

        protected override void CreateCommands()
        {

        }

        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new DelegateCommand(p => this.Save());
                }

                return this.saveCommand;
            }
        }

        public void Save()
        {
            this.repository.AddProduct(this.product);
        }
    }
}
