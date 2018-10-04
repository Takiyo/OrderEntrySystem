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
        private Order Order;

        private Repository repository;

        private bool isSelected;

        public OrderViewModel(Order order, Repository repository)
        {

        }

        public bool IsSelected { get; set; }

        public OrderStatus Status { get; set; }

        public Customer Customer { get; set; }

        public IEnumerable<OrderStatus> OrderStatuses { get; set; }

        protected override void CreateCommands()
        {

        }

        private void Save()
        {

        }

        private void OkExecute()
        {

        }

        private void CancelExecute()
        {

        }
    }
}
