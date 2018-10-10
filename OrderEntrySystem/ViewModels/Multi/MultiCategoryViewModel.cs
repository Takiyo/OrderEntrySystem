using OrderEntryDataAccess;
using OrderEntryEngine;
using OrderEntryEngine.EventArgs;
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
    public class MultiCategoryViewModel : WorkspaceViewModel
    {
        private Repository repository;

        public MultiCategoryViewModel(Repository repository) : base("Categories")
        {
            this.repository = repository;
            this.repository.CategoryAdded += OnCategoryAdded;

            List<CategoryViewModel> categories =
    (from c in this.repository.GetCategories()
     select new CategoryViewModel(c, this.repository)).ToList();

            categories.ForEach(cvm => cvm.PropertyChanged += this.OnCategoryViewModelPropertyChanged);
            this.AllCategories = new ObservableCollection<CategoryViewModel>(categories);
        }

        public ObservableCollection<CategoryViewModel> AllCategories
        {
            get; private set;
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewCategoryExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditCategoryExecute())));
        }

        private void CreateNewCategoryExecute()
        {
            CategoryViewModel viewModel = new CategoryViewModel(new Category(), this.repository);

            ShowCategory(viewModel);
        }

        private static void ShowCategory(CategoryViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            CategoryView view = new CategoryView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        private void EditCategoryExecute()
        {
            CategoryViewModel viewModel = this.AllCategories.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                ShowCategory(viewModel);
            }
            else
            {
                MessageBox.Show("Select only one category to edit it.");
            }
        }

        private void OnCategoryAdded(object sender, CategoryEventArgs e)
        {
            CategoryViewModel viewModel = new CategoryViewModel(e.Category, this.repository);
            viewModel.PropertyChanged += OnCategoryViewModelPropertyChanged;
            this.AllCategories.Add(viewModel);
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllCategories.Count(vm => vm.IsSelected);
            }
        }

        private void OnCategoryViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

    }
}
