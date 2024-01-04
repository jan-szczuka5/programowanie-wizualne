using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.UI.ViewModels
{
    public class ProducerViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IProducer producer;

        public ProducerViewModel(IProducer producer)
        {
            this.producer = producer;
        }

        public string ProducerID
        {
            get => producer.ID.ToString();
            set
            {
                int.TryParse(value, out int parsedID);
                producer.ID = parsedID;
                RaisePropertyChanged(nameof(ProducerID));
            }
        }

        public string ProducerName
        {
            get => producer.Name;
            set
            {
                producer.Name = value;
                RaisePropertyChanged(nameof(ProducerName));
            }
        }
        public string ProducerAddress
        {
            get => producer.Address;
            set
            {
                producer.Address = value;
                RaisePropertyChanged(nameof(ProducerAddress));
            }
        }
        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
