using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class Producer : Window
    {
        public Producer()
        {
            InitializeComponent();
        }

        public Producer(IProducer producer)
        {
            InitializeComponent();
            producerName.Text = producer.Name;
            producerAddress.Text = producer.Address;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            producerName.SelectAll();
            producerName.Focus();
        }
        public string ProducerName
        {
            get { return producerName.Text; }
        }
        public string ProducerAddress
        {
            get { return producerAddress.Text; }
        }
    }
}
