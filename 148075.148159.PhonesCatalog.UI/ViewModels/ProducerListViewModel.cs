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
    public class ProducerListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();
    
        public void RefreshList(IEnumerable<IProducer> producers)
        {
            Producers.Clear();

            foreach (var item in producers)
            {
                Producers.Add(new ProducerViewModel(item));
            }

            RaisePropertyChanged(nameof(Producers));
        }

        private void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
