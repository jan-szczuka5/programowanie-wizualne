using _148075._148159.PhonesCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.UI.ViewModels
{
    public class PhoneListViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<PhoneViewModel> Phones { get; set; } = new ObservableCollection<PhoneViewModel>();

        public void RefreshList(IEnumerable<IPhone> phones)
        {
            Phones.Clear();

            foreach (var phone in phones)
            {
                Phones.Add(new PhoneViewModel(phone));
            }

            RaisePropertyChanged(nameof(Phones));
        }

        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
