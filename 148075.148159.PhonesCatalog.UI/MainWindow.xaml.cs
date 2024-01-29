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
using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.Interfaces;
using _148075._148159.PhonesCatalog.DAOMock1.BO;
using _148075._148159.PhonesCatalog.DAOMock2;
using _148075._148159.PhonesCatalog.BLC;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



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

        private string selectedDAOMock = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"]!;


        public MainWindow()
        {
            blc = new BLC.BLC(selectedDAOMock);
            ProducerLVM.RefreshList(blc.GetProducers().Distinct());
            PhoneLVM.RefreshList(blc.GetPhones());
            InitializeComponent();
            producerFilterValueComboBox.ItemsSource = GetAddresses();
            foreach (var phone in PhoneLVM.Phones)
            {
                PhoneList.Items.Add(phone);
            }
            foreach (var producer in ProducerLVM.Producers)
            {
                ProducerList.Items.Add(producer);
            }
        }
        /*        private void ApplyNewDataSource(object sender, RoutedEventArgs e)
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
                }*/

        #region Filters
        private void ApplyPhoneSearch(object sender, RoutedEventArgs e)
        {

            string filterValue = phoneSearchField.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                PhoneLVM.RefreshList(blc.GetPhones());
                return;
            }

            PhoneLVM.RefreshList(blc.SearchPhoneByName(filterValue));
/*
            if (PhoneList.Items.Count > 0)
            {
                PhoneList.SelectedItem = PhoneList.Items[0];

            }*/

            PhoneList.Items.Clear();
            foreach (var phone in PhoneLVM.Phones)
            {
                PhoneList.Items.Add(phone);
            }
        }

        private void ApplyProducerSearch(object sender, RoutedEventArgs e)
        {
            
            string filterValue = producerSearchField.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
               ProducerLVM.RefreshList(blc.GetProducers());
                return;
            }

            ProducerLVM.RefreshList(blc.SearchProducerByName(filterValue));

            /*            if (ProducerList.Items.Count > 0)
                        {
                            ProducerList.SelectedItem = ProducerList.Items[0];

                        }*/

            ProducerList.Items.Clear();
            foreach (var producer in ProducerLVM.Producers)
            {
                ProducerList.Items.Add(producer);
            }

        }

        private void ProducerApplyFilter(object sender, RoutedEventArgs e)
        {



            // Retrieve the filter value entered by the user.
            string filterValue = producerFilterValueComboBox.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
                ProducerLVM.RefreshList(blc.GetProducers());
            }
            else
            {
                FilterProducerByAddress(filterValue);
            }


            

            
        }


        private void PhoneApplyFilter(object sender, RoutedEventArgs e)
        {
            // First, determine the selected filter type from the ComboBox.
            var selectedFilter = filterTypeComboBox.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                // Handle the case where no filter is selected, if necessary.
                PhoneLVM.RefreshList(blc.GetPhones());
                return;
            }

            // Retrieve the filter value entered by the user.
            var filterValue = filterValueComboBox.SelectedItem as string;

            if (filterValue != null || string.IsNullOrWhiteSpace(filterValue))
            {
                // Handle the case where the filter value is empty, if necessary.
            }

            // Apply the filter based on the selected filter type.
            switch (selectedFilter.Content.ToString())
            {
                case "software type":
                    FilterPhoneBySoftwareType(filterValue);
                    break;
                case "producer name":
                    FilterPhoneByProducer(filterValue);
                    break;
                case "year of production":
                    FilterPhoneByYear(filterValue);
                    break;
                default:
                    MessageBox.Show("Unknown filter type selected.");
                    break;
            }
        }

        private void FilterPhoneBySoftwareType(string softwareType)
        {
            if (softwareType == "")
            {
                PhoneLVM.RefreshList(blc.GetPhones());
                PhoneList.Items.Clear();
                foreach (var phone in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phone);
                }
            }
            else
            {
                SoftwareType type;
                Enum.TryParse<SoftwareType>(softwareType, out type);
                PhoneLVM.RefreshList(blc.FilterPhoneBySoftwareType(type));
                PhoneList.Items.Clear();
                foreach (var phone in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phone);
                }
            }
        }

        private void FilterPhoneByProducer(string producer)
        {
            if (producer == "")
            {
                PhoneLVM.RefreshList(blc.GetPhones());
                PhoneList.Items.Clear();
                foreach (var phone in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phone);
                }
            }
            else
            {
                PhoneLVM.RefreshList(blc.FilterPhoneByProducer(producer));
                PhoneList.Items.Clear();
                foreach (var phone in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phone);
                }
            }
        }

        private void FilterProducerByAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                ProducerLVM.RefreshList(blc.GetProducers());
                ProducerList.Items.Clear();
                foreach (var producer in ProducerLVM.Producers)
                {
                    ProducerList.Items.Add(producer);
                }
            }
            else
            {
                ProducerLVM.RefreshList(blc.FilterProducerByAddress(address));
                ProducerList.Items.Clear();
                foreach (var producer in ProducerLVM.Producers)
                {
                    ProducerList.Items.Add(producer);
                }
            }
        }

        private void FilterPhoneByYear(string yearOfProduction)
        {
            if (yearOfProduction == "")
            {
                PhoneLVM.RefreshList(blc.GetPhones());
                PhoneList.Items.Clear();
                foreach (var phone in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phone);
                }
            }
            else
            {
                int.TryParse(yearOfProduction, out int year);
                PhoneLVM.RefreshList(blc.FilterPhoneByYear(year));
                PhoneList.Items.Clear();
                foreach (var phone in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phone);
                }
            }
        }

        private void RemoveFiltersPhone(object sender, RoutedEventArgs e)
        {
            PhoneLVM.RefreshList(blc.GetPhones());
           
            PhoneList.Items.Clear();
            foreach (var phone in PhoneLVM.Phones)
            {
                PhoneList.Items.Add(phone);
            }
        }

        private void RemoveFiltersProducer(object sender, RoutedEventArgs e)
        {
            ProducerLVM.RefreshList(blc.GetProducers());

            ProducerList.Items.Clear();
            foreach (var producer in ProducerLVM.Producers)
            {
                ProducerList.Items.Add(producer);
            }
        }
        #endregion

        #region Phone operations
        private void FilterTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Get the selected filter type
            string selectedFilter = (e.AddedItems[0] as ComboBoxItem)?.Content.ToString();

            // Dynamically populate ComboBox based on the selected filter type
            switch (selectedFilter)
            {
                case "producer name":
                    filterValueComboBox.ItemsSource = GetProducerNames();
                    break;
                case "year of production":
                    filterValueComboBox.ItemsSource = GetYearsOfProduction();
                    break;
                case "software type":
                    filterValueComboBox.ItemsSource = GetSoftwares();
                    break;
                default:
                    filterValueComboBox.ItemsSource = null;
                    break;
            }

            // Reset the selected item
            filterValueComboBox.SelectedItem = null;
        }


        private IEnumerable<string> GetProducerNames()
        {
            // Implement your logic to get a list of producer names
            return blc.GetAllProducersNames();
        }

        private IEnumerable<string> GetYearsOfProduction()
        {
            // Implement your logic to get a list of years of production
            return blc.GetPhones().Select(p => p.YearOfProduction.ToString()).Distinct();
        }

        private IEnumerable<string> GetSoftwares()
        {
            // Implement your logic to get a list of years of production
            return blc.GetPhones().Select(p => p.SoftwareType.ToString()).Distinct();
        }

        private IEnumerable<string> GetAddresses()
        {
            return blc.GetProducers().Select(p => p.Address.ToString()).Distinct();
        }

        private void PhoneList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedPhone((ViewModels.PhoneViewModel)e.AddedItems[0]);
            }
        }

        private void EditPhone(object sender, RoutedEventArgs e)
        {
            if (selectedPhone != null)
            {
                Phone phoneEditDialog = new(
                    blc.GetAllPhonesNames(),
                    blc.GetPhoneById(selectedPhone.PhoneID)
                );

                if (phoneEditDialog.ShowDialog() == true)
                {
                    blc.UpdatePhone(new DAOMock1.BO.Phone()
                    {
                        ID = selectedPhone.PhoneID,
                        Name = phoneEditDialog.PhoneName,
                        Producer = blc.SearchProducerByName(phoneEditDialog.Producer).First(),
                        SoftwareType = phoneEditDialog.SoftType,
                        Price = phoneEditDialog.PhonePrice,
                        AlreadySold = phoneEditDialog.PhoneAlreadySold,
                        YearOfProduction = phoneEditDialog.PhoneYearOfProduction
                    });

                    PhoneLVM.RefreshList(blc.GetPhones());
                    ChangeSelectedPhone(null);
                    PhoneList.Items.Clear();
                    foreach (var phone in PhoneLVM.Phones)
                    {
                        PhoneList.Items.Add(phone);
                    }
                }
            }
            else
            {
                MessageBox.Show("Phone is not selected!");
            }
        }

        private void RemovePhone(object sender, RoutedEventArgs e)
        {
            if (selectedPhone != null)
            {
                blc.DeletePhone(selectedPhone.PhoneID);
                PhoneLVM.RefreshList(blc.GetPhones());
                PhoneList.Items.Clear();
                foreach (var phone in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phone);
                }
                selectedPhone = null;
            }
            else
            {
                MessageBox.Show("Phone is not selected!");
            }
        }
        private int GetMaxPhoneId()
        {
            return PhoneLVM.Phones.Max(p => p.PhoneID);
        }
        private void AddPhone(object sender, RoutedEventArgs e)
        {
            var allPhonesNames = blc.GetAllPhonesNames();
            Phone phoneInputDialog = new(allPhonesNames);

            if (phoneInputDialog.ShowDialog() == true)
            {
                DAOMock1.BO.Phone phone;
                try
                {
                    int newId = GetMaxPhoneId() + 1;

                    phone = new DAOMock1.BO.Phone()
                    {
                        ID = newId,
                        Name = phoneInputDialog.PhoneName,
                        Price = phoneInputDialog.PhonePrice,
                        SoftwareType = phoneInputDialog.SoftType,
                        Producer = blc.SearchProducerByName(phoneInputDialog.Producer).First(),
                        YearOfProduction = phoneInputDialog.PhoneYearOfProduction,
                        AlreadySold = phoneInputDialog.PhoneAlreadySold
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                blc.CreatePhone(phone);
                PhoneLVM.RefreshList(blc.GetPhones());
                PhoneList.Items.Clear();
                foreach (var phoneitem in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phoneitem);
                }// Refresh the PhoneLVM after adding a new phone
            }
        }

        private void ChangeSelectedPhone(ViewModels.PhoneViewModel phoneViewModel)
        {
            selectedPhone = phoneViewModel;
            DataContext = selectedPhone;
        }

        #endregion

        #region Producer operations
        private void ChangeSelectedProducer(ViewModels.ProducerViewModel producerViewModel)
        {
            selectedProducer = producerViewModel;
            DataContext = selectedProducer;
        }

        private void ProducerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedProducer((ViewModels.ProducerViewModel)e.AddedItems[0]);
            }
        }

        private int GetMaxProducerId()
        {
            return ProducerLVM.Producers.Max(p => p.ProducerID);
        }

        private void AddProducer(object sender, RoutedEventArgs e)
        {
            var allProducersNames = blc.GetAllProducersNames();
            Producer producerDialog = new();

            if (producerDialog.ShowDialog() == true)
            {
                DAOMock1.BO.Producer producer;
                try
                {
                    int newId = GetMaxProducerId() + 1;
                    producer = new DAOMock1.BO.Producer()
                    {
                        ID = newId,
                        Name = producerDialog.ProducerName,
                        Address = producerDialog.ProducerAddress
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                blc.CreateProducer(producer);
                ProducerLVM.RefreshList(blc.GetProducers());
                ProducerList.Items.Clear();
                foreach (var produceritem in ProducerLVM.Producers)
                {
                    ProducerList.Items.Add(produceritem);
                }
                producerFilterValueComboBox.ItemsSource = GetAddresses();
            }
        }

        private void RemoveProducer(object sender, RoutedEventArgs e)
        {
            if (selectedProducer != null)
            {
                blc.DeleteProducer(selectedProducer.ProducerID);
                ProducerLVM.RefreshList(blc.GetProducers());
                PhoneLVM.RefreshList(blc.GetPhones());
                selectedProducer = null;
                ProducerList.Items.Clear();
                foreach (var produceritem in ProducerLVM.Producers)
                {
                    ProducerList.Items.Add(produceritem);
                }
                PhoneList.Items.Clear();
                foreach (var phoneitem in PhoneLVM.Phones)
                {
                    PhoneList.Items.Add(phoneitem);
                }
                producerFilterValueComboBox.ItemsSource = GetAddresses();
            }
            else
            {
                MessageBox.Show("Producer is not selected!");
            }
        }

        private void EditProducer(object sender, RoutedEventArgs e)
        {
            if (selectedProducer != null)
            {
                Producer producerDialog = new(
                    blc.GetProducerById(selectedProducer.ProducerID)
                );

                if (producerDialog.ShowDialog() == true)
                {
                    blc.UpdateProducer(new DAOMock1.BO.Producer()
                    {
                        ID = selectedProducer.ProducerID,
                        Name = producerDialog.ProducerName,
                        Address = producerDialog.ProducerAddress
                    });

                    ProducerLVM.RefreshList(blc.GetProducers());
                    ChangeSelectedProducer(null);
                    ProducerList.Items.Clear();
                foreach (var produceritem in ProducerLVM.Producers)
                {
                    ProducerList.Items.Add(produceritem);
                }
                }
                producerFilterValueComboBox.ItemsSource = GetAddresses();
            }
            else
            {
                MessageBox.Show("Producer is not selected!");
            }

        }
        #endregion
    }
}