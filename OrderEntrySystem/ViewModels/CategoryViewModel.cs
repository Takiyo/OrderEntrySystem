using OrderEntryDataAccess;
using OrderEntryEngine;
using OrderEntrySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    public class CategoryViewModel : WorkspaceViewModel
    {

        private Repository repository;

        private Category category;

        private ICommand saveCommand;

        private bool isSelected;

        public CategoryViewModel(Category category, Repository repository) : base("Category")
        {
            this.category = category;
            this.repository = repository;
        }

        public string Name
        {
            get
            {
                return this.category.Name;
            }

            set
            {
                this.category.Name = value;
                this.OnPropertyChanged("Name");
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
            this.repository.AddCategory(this.category);
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
