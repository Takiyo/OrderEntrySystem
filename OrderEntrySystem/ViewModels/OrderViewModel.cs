using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class OrderViewModel : WorkspaceViewModel
    {
        private Order order;

        private Repository repository;

        private bool isSelected;

        public OrderViewModel(Order order, Repository repository) : base("OrderViewModel")
        {
            this.order = order;
            this.repository = repository;
        }

        public OrderStatus Status
        {
            get
            {
                return this.order.Status;
            }

            set
            {
                this.order.Status = value;
                this.OnPropertyChanged("Status");
            }
        }

        public IEnumerable<OrderStatus> OrderStatuses { get; }


        public Customer Customer
        {
            get
            {
                return this.order.Customer;
            }
            set
            {
                this.order.Customer = value;
                this.OnPropertyChanged("Customer");
            }
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        public int Id
        {
            get
            {
                return this.order.Id;
            }

            set
            {
                this.order.Id = value;
                this.OnPropertyChanged("Id");
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


        private void Save()
        {
            this.repository.AddOrder(this.order);
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
