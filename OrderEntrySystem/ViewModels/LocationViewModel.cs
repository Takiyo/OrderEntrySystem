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
    /// <summary>
    /// Class used to represent a location view model.
    /// </summary>
    public class LocationViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The vm's location field.
        /// </summary>
        private Location location;

        /// <summary>
        /// The vm's repository field.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// The vm's saveCommand functionality.
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// Indicates whether the item is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the LocationViewModel class.
        /// </summary>
        /// <param name="location">Location object to be shown in the vm.</param>
        /// <param name="repository">Repository that locations are saved and loaded to.</param>
        public LocationViewModel(Location location, Repository repository) : base("Location")
        {
            this.location = location;
            this.repository = repository;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is selected.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the location's name.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the location's description.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the location's city.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the location's State.
        /// </summary>
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

        /// <summary>
        /// Gets the save command field.
        /// </summary>
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

        /// <summary>
        /// Overrides the work space view model's create commands method.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Saves the selected location to the repository's list of locations.
        /// </summary>
        public void Save()
        {
            this.repository.AddLocation(this.location);
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
