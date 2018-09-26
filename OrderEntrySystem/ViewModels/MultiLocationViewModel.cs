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
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewLocationExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditLocationExecute())));
        }

        private void CreateNewLocationExecute()
        {
            LocationViewModel viewModel = new LocationViewModel(new Location(), this.repository);

            ShowLocation(viewModel);
        }

        private static void ShowLocation(LocationViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            LocationView view = new LocationView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        private void EditLocationExecute()
        {
            LocationViewModel viewModel = this.AllLocations.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                ShowLocation(viewModel);
            }
            else
            {
                MessageBox.Show("Select only one location to edit it.");
            }
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
