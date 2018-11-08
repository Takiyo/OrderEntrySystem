using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using OrderEntryDataAccess;
using OrderEntryEngine;
using Condition = OrderEntryEngine.Condition;

namespace OrderEntrySystem
{
    public class MultiProductViewModel : WorkspaceViewModel
    {
        private Repository repository;

        private ObservableCollection<ProductViewModel> displayedProjects;

        private CollectionViewSource productViewSource;

        private string sortColumnName;

        private ListSortDirection sortDirection;

        private List<ProductViewModel> allProductsSorted;

        public MultiProductViewModel(Repository repository)
            : base("All products")
        {
            this.repository = repository;

            List<ProductViewModel> products =
                (from item in this.repository.GetProducts()
                select new ProductViewModel(item, this.repository)).ToList();

            this.AddPropertyChangedEvent(products);

            this.AllProducts = new ObservableCollection<ProductViewModel>(products);

            this.repository.ProductAdded += this.OnProductAdded;
            this.repository.ProductRemoved += this.OnProductRemoved;

            this.displayedProjects = new ObservableCollection<ProductViewModel>();
            this.Pager = new PagingViewModel(this.AllProducts.Count);

            this.Pager.CurrentPageChanged += this.OnPageChanged;

            this.RebuildPageData();

            this.productViewSource = new CollectionViewSource();
            this.SortCommand = new DelegateCommand(this.Sort);

            this.allProductsSorted = new List<ProductViewModel>(products);
        }

        public ListCollectionView SortedProducts
        {
            get
            {
                return this.productViewSource.View as ListCollectionView;
            }
        }

        public ICommand SortCommand { get; private set; }

        public void Sort(object parameter)
        {
            CollectionViewSource sortCollection = new CollectionViewSource();
            sortCollection.Source = this.AllProducts;

            var p = (string)parameter;

            if (sortColumnName == p)
            {
                if (this.sortDirection == ListSortDirection.Ascending)
                {
                    this.sortDirection = ListSortDirection.Descending;
                }
                else
                {
                    this.sortDirection = ListSortDirection.Ascending;
                }
            }

            sortCollection.GroupDescriptions.Clear();
            sortCollection.SortDescriptions.Add(new SortDescription(sortColumnName, sortDirection));

            var sc = sortCollection.View.Cast<ProductViewModel>().ToList();
            this.allProductsSorted.Clear();
           
            foreach (ProductViewModel pvm in sc)
            {
                allProductsSorted.Add(pvm);
            }

            this.RebuildPageData();
        }

        public ObservableCollection<ProductViewModel> AllProducts { get; set; }

        public ObservableCollection<ProductViewModel> DisplayedProducts
        {
            get
            {
                return this.displayedProjects;
            }
            set
            {
                this.displayedProjects = value;
                this.productViewSource = new CollectionViewSource();
                this.productViewSource.Source = this.DisplayedProducts;
            }
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllProducts.Count(vm => vm.IsSelected);
            }
        }

        public void AddPropertyChangedEvent(List<ProductViewModel> products)
        {
            products.ForEach(pvm => pvm.PropertyChanged += this.OnProductViewModelPropertyChanged);
        }

        public PagingViewModel Pager { get; private set; }

        public ObservableCollection<CommandViewModel> FilterCommands
        {
            get; private set;
        } = new ObservableCollection<CommandViewModel>();

        public IEnumerable<Condition> Conditions
        {
            get
            {
                return Enum.GetValues(typeof(Condition)) as IEnumerable<Condition>;
            }
        }

        public Condition FilterCondition { get; set; }

        public string SearchText { get; set; }

        public void RebuildPageData()
        {
            this.DisplayedProducts.Clear();

            int i = Pager.PageSize * (Pager.CurrentPage - 1);
            Pager.ItemCount = this.AllProducts.Count;
            IEnumerable<ProductViewModel> products = this.allProductsSorted.Skip(i);
            products = this.allProductsSorted.Take(this.Pager.PageSize);

            foreach (ProductViewModel p in products)
            {
                DisplayedProducts.Add(p);
            }
        }

        
        private void OnPageChanged(object sender, CurrentPageChangeEventArgs e)
        {
            this.RebuildPageData();
        }

        private void Search()
        {
            this.AllProducts = new ObservableCollection<ProductViewModel>
            (
                (from p in this.repository.GetProducts()
                 where p.Name.ToUpper().Contains(SearchText.ToUpper()) || p.Description.ToUpper().Contains(SearchText.ToUpper())
                 select new ProductViewModel(p, this.repository)).ToList()
            );
            this.OnPropertyChanged("AllProducts");

            this.allProductsSorted = new List<ProductViewModel>(this.AllProducts);
        }

        private void Filter()
        {
            this.AllProducts = new ObservableCollection<ProductViewModel>
                (
                (from p in this.repository.GetProducts()
                 where p.Condition == this.FilterCondition
                 select new ProductViewModel(p, this.repository)).ToList()
                 );
            this.OnPropertyChanged("AllProducts");
        }

        private void ClearFilters()
        {
            this.FilterCondition = Condition.Poor;
            this.OnPropertyChanged("FilterCondition");
            this.SearchText = "";
            this.OnPropertyChanged("SearchText");

            List<ProductViewModel> products =
    (from item in this.repository.GetProducts()
     select new ProductViewModel(item, this.repository)).ToList();

            this.AddPropertyChangedEvent(products);

            this.AllProducts = new ObservableCollection<ProductViewModel>(products);;

            this.allProductsSorted = new List<ProductViewModel>(products);
        }


        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(param => this.CreateNewProductExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(param => this.EditProductExecute(), p => this.NumberOfItemsSelected == 1)));
            this.Commands.Add(new CommandViewModel("Delete", new DelegateCommand(param => this.DeleteProductExecute(), p => this.NumberOfItemsSelected == 1)));
            this.FilterCommands.Add(new CommandViewModel("Filter", new DelegateCommand(param => this.Filter())));
            this.FilterCommands.Add(new CommandViewModel("Clear", new DelegateCommand(param => this.ClearFilters())));
            this.FilterCommands.Add(new CommandViewModel("Search", new DelegateCommand(param => this.Search())));
        }

        private void OnProductAdded(object sender, ProductEventArgs e)
        {
            ProductViewModel vm = new ProductViewModel(e.Product, this.repository);
            vm.PropertyChanged += this.OnProductViewModelPropertyChanged;

            this.AllProducts.Add(vm);
        }

        /// <summary>
        /// A handler which responds when a product view model's property changes.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnProductViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string isSelected = "IsSelected";

            if (e.PropertyName == isSelected)
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        private void CreateNewProductExecute()
        {
            Product product = new Product();

            ProductViewModel viewModel = new ProductViewModel(product, this.repository);

            this.ShowProduct(viewModel);
        }

        private void EditProductExecute()
        {
            ProductViewModel viewModel = this.GetOnlySelectedViewModel();

            if (viewModel != null)
            {
                this.ShowProduct(viewModel);

                this.repository.SaveToDatabase();
            }
            else
            {
                MessageBox.Show("Please select only one product.");
            }
        }

        private void DeleteProductExecute()
        {
            ProductViewModel viewModel = this.GetOnlySelectedViewModel();

            if (viewModel != null)
            {
                if (MessageBox.Show("Do you really want to delete the selected Item?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.repository.RemoveProduct(viewModel.Product);
                    this.repository.SaveToDatabase();
                }
            }
            else
            {
                MessageBox.Show("Please select only one Item");
            }
        }

        private ProductViewModel GetOnlySelectedViewModel()
        {
            ProductViewModel result;

            try
            {
                result = this.AllProducts.Single(vm => vm.IsSelected);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Creates a new window to edit a car.
        /// </summary>
        /// <param name="viewModel">The view model for the product to be edited.</param>
        private void ShowProduct(WorkspaceViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            viewModel.CloseAction = b => window.DialogResult = b;
            window.Title = viewModel.DisplayName;

            ProductView view = new ProductView();

            view.DataContext = viewModel;

            window.Content = view;

            window.ShowDialog();
        }

        private void OnProductRemoved(object sender, ProductEventArgs e)
        {
            ProductViewModel viewModel = this.GetOnlySelectedViewModel();
            if (viewModel != null)
            {
                if (viewModel.Product == e.Product)
                {
                    this.AllProducts.Remove(viewModel);
                }
            }
        }
    }
}