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
    public class MultiOrderViewModel : WorkspaceViewModel
    {
        private Repository repository;

        public MultiOrderViewModel(Repository repository) : base("Orders")
        {
            this.repository = repository;
            this.repository.OrderAdded += OnOrderAdded;

            List<OrderViewModel> orders=
    (from o in this.repository.GetOrders()
     select new OrderViewModel(o, this.repository)).ToList();

            orders.ForEach(ovm => ovm.PropertyChanged += this.OnOrderViewModelPropertyChanged);
            this.AllOrders = new ObservableCollection<OrderViewModel>(orders);

        }

        public ObservableCollection<OrderViewModel> AllOrders { get; set; }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllOrders.Count(vm => vm.IsSelected);
            }
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewOrderExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditOrderExecute())));
        }

        private void OnOrderAdded(object sender, OrderEventArgs e)
        {
            OrderViewModel viewModel = new OrderViewModel(e.Order, this.repository);
            viewModel.PropertyChanged += OnOrderViewModelPropertyChanged;
            this.AllOrders.Add(viewModel);
        }

        private void OnOrderViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        private void CreateNewOrderExecute()
        {
            OrderViewModel viewModel = new OrderViewModel(new Order(), this.repository);

            ShowOrder(viewModel);
        }

        private void EditOrderExecute()
        {
            OrderViewModel viewModel = this.AllOrders.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                ShowOrder(viewModel);
            }
            else
            {
                MessageBox.Show("Select only one order to edit it.");
            }
        }

        private void ShowOrder(OrderViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            OrderView view = new OrderView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }
    }
}
