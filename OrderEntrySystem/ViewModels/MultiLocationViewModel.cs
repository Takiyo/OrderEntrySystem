using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class MultiLocationViewModel : WorkspaceViewModel
    {
        private Repository repository;

        public MultiLocationViewModel(Repository repository) : base("Locations")
        {
            this.repository = repository;
            this.repository.LocationAdded += OnLocationAdded;

            List<LocationViewModel> locations =
    (from l in this.repository.GetLocations()
     select new LocationViewModel(l, this.repository)).ToList();

            locations.ForEach(lvm => lvm.PropertyChanged += this.OnLocationViewModelPropertyChanged);
            this.AllLocations = new ObservableCollection<LocationViewModel>(locations);
        }

        public ObservableCollection<LocationViewModel> AllLocations
        {
            get; private set;
        }

        protected override void CreateCommands()
        {

        }

        private void OnLocationAdded(object sender, LocationEventArgs e)
        {
            LocationViewModel viewModel = new LocationViewModel(e.Location, this.repository);
            viewModel.PropertyChanged += OnLocationViewModelPropertyChanged;
            this.AllLocations.Add(viewModel);
        }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllLocations.Count(vm => vm.IsSelected);
            }
        }

        private void OnLocationViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }
    }
}
