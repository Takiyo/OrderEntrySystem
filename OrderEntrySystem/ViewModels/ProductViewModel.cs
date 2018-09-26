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

        private bool isSelected;

        public ProductViewModel(Product product, Repository repository) : base("Product")
        {
            this.product = product;
            this.repository = repository;
        }

        public Condition Condition
        {
            get
            {
                return this.product.Condition;
            }
            set
            {
                this.product.Condition = value;
                this.OnPropertyChanged("Condition");
            }
        }

        public IEnumerable<Condition> Conditions
        {
            get
            {
                return Enum.GetValues(typeof(Condition)) as IEnumerable<Condition>;
            }
        }

        public IEnumerable<Location> Locations
        {
            get
            {
                return this.repository.GetLocations();
            }
        }

        public Location Location
        {
            get
            {
                return this.product.Location;
            }
            set
            {
                this.product.Location = value;
                this.OnPropertyChanged("Location");
            }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                return this.repository.GetCategories();
            }
        }

        public Category Category
        {
            get
            {
                return this.product.Category;
            }
            set
            {
                this.product.Category = value;
                this.OnPropertyChanged("Category");
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
                this.OnPropertyChanged("Name");
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
                this.OnPropertyChanged("Description");
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
                this.OnPropertyChanged("Price");
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
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
            this.repository.SaveToDatabase();
        }

        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}
