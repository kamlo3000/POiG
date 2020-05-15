namespace PiłkarzeMVVM.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using Models;
    using Newtonsoft.Json;
    using BaseClass;
    using System.IO;

    internal partial class MainVM : ViewModelBase
    {
        ObservableCollection<ZawodnikVM> zawodnicy = null;
        readonly ushort[] lata = StworzTabliceLat();

        static readonly string domyslneImie = "Wprowadź imię gracza";
        static readonly string domyslneNazwisko = "Wprowadź nazwisko gracza";
        static readonly ushort domyslnyWiek = 18;
        static readonly double domyslnaWaga = 60;
        static readonly int domyslnyIndeks = -1;

        string aktualnyImie = domyslneImie;
        string aktualneNazwisko = domyslneNazwisko;
        ushort aktualnyWiek = domyslnyWiek;
        double aktualnaWaga = domyslnaWaga;
        int aktualnyIndeks = domyslnyIndeks;

        public ObservableCollection<ZawodnikVM> Zawodnicy
        {
            get
            {
                if (zawodnicy == null)
                {
                    zawodnicy = new ObservableCollection<ZawodnikVM>();
                    WczytajZawodnikow();
                }
                return zawodnicy;
            }
        }

        public ushort[] Lata
        {
            get => this.lata;
        }

        public string AktualneImie
        {
            get => this.aktualnyImie;
            set { this.aktualnyImie = value; }
        }

        public string AktualneNazwisko
        {
            get => this.aktualneNazwisko;
            set { this.aktualneNazwisko = value; }
        }

        public ushort AktualnyWiek
        {
            get => this.aktualnyWiek;
            set { this.aktualnyWiek = value; }
        }

        public double AktualnaWaga
        {
            get => this.aktualnaWaga;
            set { this.aktualnaWaga = value; }
        }

        public int AktualnyIndeks
        {
            get => this.aktualnyIndeks;
            set { this.aktualnyIndeks = value; }
        }

        static ushort[] StworzTabliceLat()
        {
            List<ushort> _lata = new List<ushort>();
            for (ushort i = 18; i <= 50; i++)
                _lata.Add(i);
            return _lata.ToArray();
        }

        void ZapiszZawodnikow()
        {
            List<Zawodnik> zawodnicyDoZapisu = new List<Zawodnik>();
            foreach (ZawodnikVM ZawodnikViewModel in zawodnicy)
                zawodnicyDoZapisu.Add(ZawodnikViewModel.AktualnyZawodnik);

            string zawodnicyJSON = JsonConvert.SerializeObject(zawodnicyDoZapisu);
            File.WriteAllText(@"Zawodnicy.json", zawodnicyJSON);
        }

        void WczytajZawodnikow()
        {
            if (!File.Exists("Zawodnicy.json"))
            {
                WczytajDomyslnychZawodnikow();
                return;
            }

            List<Zawodnik> wczytajZawodnikow = JsonConvert.DeserializeObject<List<Zawodnik>>(File.ReadAllText("Zawodnicy.json"));
            if (wczytajZawodnikow.Count > 0)
            {
                foreach (Zawodnik zawodnik in wczytajZawodnikow)
                    zawodnicy.Add(new ZawodnikVM(zawodnik));
                return;
            }
            WczytajDomyslnychZawodnikow();
        }

        void WczytajDomyslnychZawodnikow()
        {
            zawodnicy.Add(new ZawodnikVM(new Zawodnik("Pablo", "Picasso", 18, 40)));
            zawodnicy.Add(new ZawodnikVM(new Zawodnik("Pablo", "Dżanusz", 20, 55.0)));
            zawodnicy.Add(new ZawodnikVM(new Zawodnik("Pablo", "Escobar", 49, 75.9)));
        }

        bool CzyIstnieje(string _imie, string _nazwisko, ushort _wiek, double _waga)
        {
            foreach (ZawodnikVM ZawodnikViewModel in zawodnicy)
            {
                if (ZawodnikViewModel.AktualnyZawodnik.Imie == _imie &&
                    ZawodnikViewModel.AktualnyZawodnik.Nazwisko == _nazwisko &&
                    ZawodnikViewModel.AktualnyZawodnik.Wiek == _wiek &&
                    ZawodnikViewModel.AktualnyZawodnik.Waga == _waga) return true;
            }
            return false;
        }
    }
}
