using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class ReportViewModel : WorkspaceViewModel
    {
        private Repository repository;

        public ReportViewModel(Repository repository): base("Customer report")
        {

        }

        public object CustomerOrders { get; set; }

        protected override void CreateCommands()
        {

        }
        private void LoadReport()
        {
            //from p in this.repository.GetProducts()
            //where p.Condition == this.FilterCondition
            //select new ProductViewModel(p, this.repository)).ToList()
            IEnumerable<Customer> customers = (
            from c in this.repository.GetCustomers()
            where c.IsArchived != true
            select new Customer
            {
                FirstName = c.FirstName,
                Orders = c.Orders,
                TotalToBeSpent = c.Orders.
            });
        }
    }
}
