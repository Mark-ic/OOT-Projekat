using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Projekat
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Klub> Klubovi { get; set; }
        public ObservableCollection<Klub> KluboviNaMapi { get; set; }

        private Klub _odabraniKlub;
        public Klub OdabraniKlub
        {
            get { return _odabraniKlub; }
            set
            {
                if (_odabraniKlub != value)
                {
                    _odabraniKlub = value;
                    NotifyPropertyChanged(nameof(OdabraniKlub));
                }
            }
        }

        public MainViewModel()
        {
            Klubovi = new ObservableCollection<Klub>();
            KluboviNaMapi = new ObservableCollection<Klub>();


        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
