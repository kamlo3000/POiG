namespace PiłkarzeMVVM.Models
{
    class Zawodnik
    {
        string imie;
        string nazwisko;
        ushort wiek;
        double waga;

        public Zawodnik()
        {
            Imie = "Gamil";
            Nazwisko = "Galoch";
            Wiek = 23;
            Waga = 87.5;
        }

        public Zawodnik(string _imie, string _nazwisko, ushort _wiek, double _waga)
        {
            Imie = _imie;
            Nazwisko = _nazwisko;
            Wiek = _wiek;
            Waga = _waga;
        }

        public Zawodnik(Zawodnik zawowdnik)
        {
            Imie = zawowdnik.Imie;
            Nazwisko = zawowdnik.Nazwisko;
            Wiek = zawowdnik.Wiek;
            Waga = zawowdnik.Waga;
        }
        public string Imie
        {
            get
            {
                return imie;
            }
            set
            {
                imie = value;
            }
        }
        public string Nazwisko
        {
            get
            {
                return nazwisko;
            }
            set
            {
                nazwisko = value;
            }
        }
        public ushort Wiek
        {
            get
            {
                return wiek;
            }
            set
            {
                wiek = value;
            }
        }
        public double Waga
        {
            get
            {
                return waga;
            }
            set
            {
                waga = value;
            }
        }
    }
}
