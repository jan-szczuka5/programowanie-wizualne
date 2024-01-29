using _148075._148159.PhonesCatalog.Core;
using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _148075._148159.PhonesCatalog.UI
{

    public partial class Phone : Window
    {
        public Phone(IEnumerable<string> producers)
        {
            InitializeComponent();
            producers.ToList().ForEach(p => producer.Items.Add(p));
            if(producer.Items.Count > 0)
            {
                producer.SelectedIndex = 0;
            }
            softwareType.ItemsSource = Enum.GetNames(typeof(SoftwareType));
            if (softwareType.Items.Count > 0)
            {
                softwareType.SelectedIndex = 0;
            }
        }

        public Phone(IEnumerable<string> producers, IPhone phone)
        {
            InitializeComponent();
            producers.ToList().ForEach(p => producer.Items.Add(p));

            for (int i = 0; i < producers.Count(); i++)
            {
                if (producers.ElementAt(i).Equals(phone.Producer.Name))
                {
                    producer.SelectedIndex = i;
                    break;
                }
            }
            softwareType.ItemsSource = Enum.GetNames(typeof(SoftwareType));
            if (softwareType.Items.Count > 0)
            {
                softwareType.SelectedIndex = 0;
            }
            phoneName.Text = phone.Name;
            softwareType.SelectedIndex = (int)phone.SoftwareType;
            phonePrice.Text = phone.Price.ToString();

        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            phoneName.SelectAll();
            phoneName.Focus();
        }

        public string PhoneName
        {
            get { return phoneName.Text; }
        }

        public SoftwareType SoftType
        {
            get
            {
                return (SoftwareType)softwareType.SelectedIndex;
            }
        }

        public int PhonePrice
        {
            get
            {
                return int.Parse(phonePrice.Text);
            }
        }

        public string Producer
        {
            get
            {
                return producer.Text;
            }
        }

        public int PhoneYearOfProduction
        {
            get
            {
                return int.Parse(phoneYearOfProduction.Text);
            }
        }

        public int PhoneAlreadySold
        {
            get
            {
                return int.Parse(phoneAlreadySold.Text);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
