using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    public class LocationViewModel : WorkspaceViewModel
    {
        private Location location;

        private Repository repository;

        private ICommand saveCommand;

        private bool isSelected;

        public LocationViewModel(Location location, Repository repository) : base("Location")
        {
            this.location = location;
            this.repository = repository;
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

        public string Name
        {
            get
            {
                return this.location.Name;
            }

            set
            {
                this.location.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return this.location.Description;
            }

            set
            {
                this.location.Description = value;
                this.OnPropertyChanged("Description");
            }
        }

        public string City
        {
            get
            {
                return location.City;
            }
            set
            {
                this.location.City = value;
                this.OnPropertyChanged("City");
            }
        }

        public string State
        {
            get
            {
                return location.State;
            }
            set
            {
                this.location.State = value;
                this.OnPropertyChanged("State");
            }
        }

        protected override void CreateCommands()
        {

        }



        public void Save()
        {
            this.repository.AddLocation(this.location);
        }
    }
}
