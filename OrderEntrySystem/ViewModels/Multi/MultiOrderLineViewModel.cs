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
    public class MultiOrderLineViewModel : WorkspaceViewModel
    {
        private Repository repository;

        private Order order;

        public MultiOrderLineViewModel(Repository repo, Order order)
            : base("Order Lines")
        {
            this.repository = repo;
            this.order = order;
        }

        public ObservableCollection<OrderLineViewModel> AllLines { get; set; }

        /// <summary>
        /// Gets the number of items selected.
        /// </summary>
        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllLines.Count(vm => vm.IsSelected);
            }
        }

        public void AddPropertyChangedEvent(List<OrderLineViewModel> lines)
        {
            lines.ForEach(ovm => ovm.PropertyChanged += this.OnOrderLineViewModelPropertyChanged);
        }

        /// <summary>
        /// This creates commands.
        /// </summary>
        protected override void CreateCommands()
        {

            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewOrderLineExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditOrderLineExecute())));

        }

        /// <summary>
        /// When the customer gets added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The argument.</param>
        private void OnOrderLineAdded(object sender, OrderLineEventArgs e)
        {
            OrderLineViewModel viewModel = new OrderLineViewModel(e.Line, this.repository);

            viewModel.PropertyChanged += this.OnOrderLineViewModelPropertyChanged;

            this.AllLines.Add(viewModel);
        }

        /// <summary>
        /// When the property changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The argument.</param>
        private void OnOrderLineViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }


        /// <summary>
        /// Enables the edit product execute.
        /// </summary>
        private void EditOrderLineExecute()
        {
            try
            {
                OrderLineViewModel viewModel = this.AllLines.SingleOrDefault(vm => vm.IsSelected);

                ShowOrderLine(viewModel);

                this.repository.SaveToDatabase();
            }
            catch
            {
                MessageBox.Show("You can only select one line.");
            }
        }

        /// <summary>
        /// Creates a new product execute.
        /// </summary>
        private void CreateNewOrderLineExecute()
        {
            OrderLineViewModel viewModel = new OrderLineViewModel(new OrderLine { Order = this.order }, this.repository);

            ShowOrderLine(viewModel);
        }

        /// <summary>
        /// This shows the product.
        /// </summary>
        /// <param name="viewModel">The view model being shown.</param>
        private static void ShowOrderLine(OrderLineViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            OrderLineView view = new OrderLineView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }
    }
}
