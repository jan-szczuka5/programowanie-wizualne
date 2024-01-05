using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.UI.ViewModels
{
    public class PhoneViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IPhone phone;

        public PhoneViewModel(IPhone phone)
        {
            this.phone = phone;
        }

        public int PhoneID
        {
            get => phone.ID;
            set
            {
                phone.ID = value;
                RaisePropertyChanged(nameof(PhoneID));
            }
        }

        public string PhoneName
        {
            get => phone.Name;
            set
            {
                phone.Name= value;
                RaisePropertyChanged(nameof(PhoneName));
            }
        }

        public int PhonePrice
        {
            get => phone.Price;
            set
            {
                phone.Price = value;
                RaisePropertyChanged(nameof(PhonePrice));
            }
        }

        public Core.SoftwareType SoftwareType
        {
            get => phone.SoftwareType;
            set
            {
                phone.SoftwareType = value;
                RaisePropertyChanged(nameof(SoftwareType));
            }
        }

        public int PhoneYearOfProduction { 
            get => phone.YearOfProduction;
            set
            {
                phone.YearOfProduction = value;
                RaisePropertyChanged(nameof(PhoneYearOfProduction));
            }
        }
        public int PhoneAlreadySold { 
            get => phone.AlreadySold;
            set 
            {
                phone.AlreadySold = value;
                RaisePropertyChanged(nameof(PhoneAlreadySold));
            } }

        public string PhoneProducer
        {
            get => phone.Producer.Name;
            set
            {
                phone.Producer.Name = value;
                RaisePropertyChanged(nameof(PhoneProducer));
            }
        }

        public string PhoneProducerAddress
        {
            get => phone.Producer.Address;
            set
            {
                phone.Producer.Address = value;
                RaisePropertyChanged(nameof(PhoneProducerAddress));
            }
        }

        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
