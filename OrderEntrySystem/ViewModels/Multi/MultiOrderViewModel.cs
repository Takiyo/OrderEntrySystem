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

        private Customer customer;

        public MultiOrderViewModel(Repository repository, Customer customer) : base("Orders")
        {
            this.customer = customer;
            this.repository = repository;
            this.repository.OrderAdded += OnOrderAdded;

            this.Commands.Clear();
            this.CreateCommands();


            List<OrderViewModel> orders=
                (from o in this.repository.GetOrders()
                 select new OrderViewModel(o, this.repository)).ToList();

            AddPropertyChangedEvent(orders);

            this.AllOrders = new ObservableCollection<OrderViewModel>(orders);

            repository.OrderAdded += this.OnOrderAdded;
        }

        public void AddPropertyChangedEvent (List<OrderViewModel> orders)
        {
            orders.ForEach(ovm => ovm.PropertyChanged += this.OnOrderViewModelPropertyChanged);
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
            if (customer != null)
            {
                this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewOrderExecute())));
                this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditOrderExecute())));
            }
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
            OrderViewModel viewModel = new OrderViewModel(new Order { Customer = this.customer }, this.repository);

            ShowOrder(viewModel);
        }

        private void EditOrderExecute()
        {
            OrderViewModel viewModel = this.AllOrders.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                ShowOrder(viewModel);

                this.repository.SaveToDatabase();
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
