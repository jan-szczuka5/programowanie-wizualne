using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _148075._148159.PhonesCatalog.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ViewModels.PhoneListViewModel PhoneLVM { get; } = new ViewModels.PhoneListViewModel();
        private ViewModels.PhoneViewModel selectedPhone = null;

        public ViewModels.ProducerListViewModel ProducerLVM { get; } = new ViewModels.ProducerListViewModel();
        private ViewModels.ProducerViewModel selectedProducer = null;

        private readonly BLC.BLC blc;

        private string selectedDAOMock = "148075.148159.PhonesCatalog.DAOMock1.dll";


        public MainWindow()
        {
            blc = new BLC.BLC(selectedDAOMock);
            ProducerLVM.RefreshList(blc.GetProducers().Distinct());
            PhoneLVM.RefreshList(blc.GetPhones());
            InitializeComponent();
        }
        private void ApplyNewDataSource(object sender, RoutedEventArgs e)
        {
            try
            {
                blc.LoadDatasource(datasource.Text);
                ProducerLVM.RefreshList(blc.GetProducers());
                PhoneLVM.RefreshList(blc.GetPhones());
                selectedDAOMock = datasource.Text;
            }
            catch
            {
                MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                blc.LoadDatasource(selectedDAOMock);
            }
        }

        #region Filters
        private void ApplyAircraftSearch(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = searchTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }

            // Retrieve the filter value entered by the user.
            string filterValue = aircraftSearchField.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {
                case "service hours":
                    FilterByServiceHours(filterValue);
                    break;
                case "model name":
                    FilterByModelName(filterValue);
                    break;
                case "aircraft type":
                    FilterByAircraftType(filterValue);
                    break;
                case "airbase name":
                    FilterByAirbases(filterValue);
                    break;
                case "airbase location":
                    FilterByAirbasesLocation(filterValue);
                    break;
                default:
                    // Handle unexpected filter type, if necessary.
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }

            if (AircraftList.Items.Count > 0)
            {
                AircraftList.SelectedItem = AircraftList.Items[0];

            }
        }

        private void ApplyAirbaseSearch(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = airbasesearchTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                AirbaseLVM.RefreshList(blc.GetAllAirbases());
            }

            // Retrieve the filter value entered by the user.
            string filterValue = airbaseSearchField.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                AirbaseLVM.RefreshList(blc.GetAllAirbases());
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {
                case "airbase name":
                    FilterAirbaseByName(filterValue);
                    break;
                case "airbase location":
                    FilterAirbaseByLocation(filterValue);
                    break;
                default:
                    // Handle unexpected filter type, if necessary.
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }

            if (AirbaseList.Items.Count > 0)
            {
                AirbaseList.SelectedItem = AirbaseList.Items[0];

            }
        }

        private void AirbaseApplyFilter(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = airbasefilterTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                AirbaseLVM.RefreshList(blc.GetAllAirbases());
            }

            // Retrieve the filter value entered by the user.
            string filterValue = airbasefilterValueTextBox.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                AirbaseLVM.RefreshList(blc.GetAllAirbases());
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {

                case "airbase name":
                    FilterAirbaseByName(filterValue);
                    break;
                case "airbase location":
                    FilterAirbaseByLocation(filterValue);
                    break;
                default:
                    // Handle unexpected filter type, if necessary.
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = filterTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }

            // Retrieve the filter value entered by the user.
            string filterValue = filterValueTextBox.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {
                case "service hours":
                    FilterByServiceHours(filterValue);
                    break;
                case "model name":
                    FilterByModelName(filterValue);
                    break;
                case "aircraft type":
                    FilterByAircraftType(filterValue);
                    break;
                case "airbase name":
                    FilterByAirbases(filterValue);
                    break;
                case "airbase location":
                    FilterByAirbasesLocation(filterValue);
                    break;
                default:
                    // Handle unexpected filter type, if necessary.
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }
        }

        private void FilterByServiceHours(string hours)
        {
            if (hours == "")
            {
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }
            else
            {
                AircraftLVM.RefreshList(blc.GetAircraft(Convert.ToInt32(hours)));
            }
        }

        private void FilterByModelName(string modelName)
        {
            if (modelName == "")
            {
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }
            else
            {
                AircraftLVM.RefreshList(blc.GetAircraftByModel(modelName));
            }
        }

        private void FilterByAircraftType(string modelType)
        {
            if (modelType == "")
            {
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }
            else
            {
                AircraftType type;
                Enum.TryParse<AircraftType>(modelType, out type);
                AircraftLVM.RefreshList(blc.GetAircraft(type));
            }
        }

        private void FilterByAirbases(string airbase)
        {
            if (airbase == "")
            {
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }
            else
            {
                AircraftLVM.RefreshList(blc.GetAircraftByBaseName(airbase));
            }
        }

        private void FilterByAirbasesLocation(string airbaseLocation)
        {
            if (airbaseLocation == "")
            {
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }
            else
            {
                AircraftLVM.RefreshList(blc.GetAircraftByBaseLocation(airbaseLocation));
            }
        }

        private void FilterAirbaseByName(string airbase)
        {
            if (airbase == "")
            {
                AirbaseLVM.RefreshList(blc.GetAllAirbases());
            }
            else
            {
                AirbaseLVM.RefreshList(blc.GetAirbaseByName(airbase));
            }
        }

        private void FilterAirbaseByLocation(string airbaseLocation)
        {
            if (airbaseLocation == "")
            {
                AirbaseLVM.RefreshList(blc.GetAllAirbases());
            }
            else
            {
                AirbaseLVM.RefreshList(blc.GetAirbaseByLocation(airbaseLocation));
            }
        }
        #endregion

        #region Aircraft operations

        private void AircraftList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedAircraft((ViewModels.AircraftViewModel)e.AddedItems[0]);
            }
        }

        private void EditAircraft(object sender, RoutedEventArgs e)
        {
            if (selectedAircraft != null)
            {
                AircraftDialog aircraftEditDialog = new(
                    blc.GetAllAirbasesNames(),
                    blc.GetAircraft(selectedAircraft.AircraftGUID).First()
                );

                if (aircraftEditDialog.ShowDialog() == true)
                {
                    blc.CreateOrUpdateAircraft(new AircraftDBMock()
                    {
                        GUID = selectedAircraft.AircraftGUID,
                        Model = aircraftEditDialog.AircraftModel,
                        Type = aircraftEditDialog.AirType,
                        Airbase = blc.GetAirbaseByName(aircraftEditDialog.Airbase).First(),
                        ServiceHours = aircraftEditDialog.AircraftServiceHours
                    });

                    AircraftLVM.RefreshList(blc.GetAllAircrafts());
                    ChangeSelectedAircraft(null);
                }
            }
            else
            {
                MessageBox.Show("Aircraft is not selected!");
            }
        }

        private void RemoveAircraft(object sender, RoutedEventArgs e)
        {
            if (selectedAircraft != null)
            {
                blc.RemoveAircraft(selectedAircraft.AircraftGUID);
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
                selectedAircraft = null;
            }
            else
            {
                MessageBox.Show("Aircraft is not selected!");
            }
        }

        private void AddAircraft(object sender, RoutedEventArgs e)
        {
            var allAirbasesNames = blc.GetAllAirbasesNames();
            AircraftDialog aircraftInputDialog = new(allAirbasesNames);

            if (aircraftInputDialog.ShowDialog() == true)
            {
                DBMock.AircraftDBMock aircraft;
                try
                {
                    aircraft = new AircraftDBMock()
                    {
                        Model = aircraftInputDialog.AircraftModel,
                        Airbase = blc.GetAirbaseByName(aircraftInputDialog.Airbase).First(),
                        ServiceHours = aircraftInputDialog.AircraftServiceHours,
                        Type = aircraftInputDialog.AirType
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                blc.CreateOrUpdateAircraft(aircraft);
                AircraftLVM.RefreshList(blc.GetAllAircrafts());
            }
        }

        private void ChangeSelectedAircraft(ViewModels.AircraftViewModel aircraftViewModel)
        {
            selectedAircraft = aircraftViewModel;
            DataContext = selectedAircraft;
        }

        #endregion

        #region Airbase operations
        private void ChangeSelectedAirbase(ViewModels.AirbaseViewModel airbaseViewModel)
        {
            selectedAirbase = airbaseViewModel;
            DataContext = selectedAirbase;
        }

        private void AirbaseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedAirbase((ViewModels.AirbaseViewModel)e.AddedItems[0]);
            }
        }



        private void AddAirbase(object sender, RoutedEventArgs e)
        {
            var allAirbasesNames = blc.GetAllAirbasesNames();
            AirbaseDialog airbaseDialog = new();

            if (airbaseDialog.ShowDialog() == true)
            {
                DBMock.AirbaseDBMock airbase;
                try
                {
                    airbase = new AirbaseDBMock()
                    {
                        Name = airbaseDialog.AirbaseName,
                        Location = airbaseDialog.AirbaseLocation
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                blc.CreateOrUpdateAirbase(airbase);
                AirbaseLVM.RefreshList(blc.GetAllAirbases());
            }
        }

        private void RemoveAirbase(object sender, RoutedEventArgs e)
        {
            if (selectedAirbase != null)
            {
                blc.RemoveAirbase(selectedAirbase.AirbaseGUID);
                AirbaseLVM.RefreshList(blc.GetAllAirbases());
                selectedAirbase = null;
            }
            else
            {
                MessageBox.Show("Airbase is not selected!");
            }
        }

        private void EditAirbase(object sender, RoutedEventArgs e)
        {
            if (selectedAirbase != null)
            {
                AirbaseDialog airbaseDialog = new(
                    blc.GetAirbase(selectedAirbase.AirbaseGUID).First()
                );

                if (airbaseDialog.ShowDialog() == true)
                {
                    blc.CreateOrUpdateAirbase(new AirbaseDBMock()
                    {
                        GUID = selectedAirbase.AirbaseGUID,
                        Name = airbaseDialog.AirbaseName,
                        Location = airbaseDialog.AirbaseLocation
                    });

                    AirbaseLVM.RefreshList(blc.GetAllAirbases());
                    ChangeSelectedAirbase(null);
                }
            }
            else
            {
                MessageBox.Show("Airbase is not selected!");
            }
        }
        #endregion
    }
}