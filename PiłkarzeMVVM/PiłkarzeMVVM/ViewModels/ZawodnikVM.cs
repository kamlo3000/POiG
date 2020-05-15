namespace PiłkarzeMVVM.ViewModels
{
    using BaseClass;
    using Models;
    class ZawodnikVM : ViewModelBase
    {
        Zawodnik zawodnik = null;

        public ZawodnikVM(Zawodnik _zawodnik)
        {
            zawodnik = _zawodnik;
        }

        public Zawodnik AktualnyZawodnik
        {
            get
            {
                return zawodnik;
            }
        }
        public string Imie
        {
            get
            {
                return zawodnik.Imie;
            }
            set
            {
                zawodnik.Imie = value;
                OnPropertyChanged(nameof(Imie));
            }
        }
        public string Nazwisko
        {
            get
            {
                return zawodnik.Nazwisko;
            }
            set
            {
                zawodnik.Nazwisko = value;
                OnPropertyChanged(nameof(Nazwisko));
            }
        }
        public ushort Wiek
        {
            get
            {
                return zawodnik.Wiek;
            }
            set
            {
                zawodnik.Wiek = value;
                OnPropertyChanged(nameof(Wiek));
            }
        }
        public double Waga
        {
            get
            {
                return zawodnik.Waga;
            }
            set
            {
                zawodnik.Waga = value;
                OnPropertyChanged(nameof(Waga));
            }
        }
    }
}
