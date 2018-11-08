using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class PagingViewModel : ViewModel
    {
        private int itemCount;

        private int currentPage;

        private int pageSize;

        public PagingViewModel(int itemCount) : base("")
        {
            
            Contract.Requires(itemCount >= 0);
            Contract.Requires(this.pageSize > 0);

            this.itemCount = itemCount;
            this.pageSize = 5;

            if (this.itemCount == 0)
            {
                this.CurrentPage = 0;
            }
            else
            {
                this.CurrentPage = 1;
            }

            this.GoToFirstPageCommand = new DelegateCommand(p => this.CurrentPage = 1, p => this.ItemCount > 0 && this.CurrentPage > 1);
            this.GoToPreviousPageCommand = new DelegateCommand(p => this.CurrentPage -= 1, p=> this.ItemCount > 0 && this.CurrentPage > 1);
            this.GoToNextPageCommand = new DelegateCommand(p => this.CurrentPage += 1, p=> this.ItemCount > 0 && this.CurrentPage < this.PageCount);
            this.GoToLastPageCommand = new DelegateCommand(p => this.CurrentPage = PageCount, p => this.ItemCount > 0 && this.CurrentPage < this.PageCount);
        }


        public EventHandler<CurrentPageChangeEventArgs> CurrentPageChanged;

        public DelegateCommand GoToFirstPageCommand { get; set; }

        public DelegateCommand GoToPreviousPageCommand { get; set; }

        public DelegateCommand GoToNextPageCommand { get; set; }

        public DelegateCommand GoToLastPageCommand { get; set; }

        public int ItemCount
        {
            get
            {
                return this.itemCount;
            }
            set
            {
                this.OnPropertyChanged("PageSize");
                this.OnPropertyChanged("ItemCount");
                this.OnPropertyChanged("PageCount");
            }
        }

        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.OnPropertyChanged("PageSize");
                this.OnPropertyChanged("ItemCount");
                this.OnPropertyChanged("PageCount");
            }
        }
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling((double)this.itemCount / this.pageSize);
            }
        }

        public int CurrentPage
        {
            get
            {
                return this.currentPage;
            }
            set
            {
                this.OnPropertyChanged("CurrentPage");
                var e = this.CurrentPageChanged;
                if (e != null)
                {
                    e(this, new CurrentPageChangeEventArgs(this.CurrentPageStartIndex, this.PageSize));
                }
            }
        }

        public int CurrentPageStartIndex
        {
            get
            {
                return this.PageCount == 0 ? -1 : (this.CurrentPage - 1) * this.PageSize;
            }
        }
    }
}
