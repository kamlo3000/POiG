namespace PiłkarzeMVVM.ViewModels
{
    using BaseClass;
    using Models;
    using System.Windows.Input;
    internal partial class MainVM : ViewModelBase
    {
        private ICommand dodaj = null;
        private ICommand modyfikuj = null;
        private ICommand usun = null;
        private ICommand wyczyscImie = null;
        private ICommand wyczyscNazwisko = null;
        private ICommand ustawDomyslneImie = null;
        private ICommand ustawDomyslneNazwisko = null;
        private ICommand wczytaj = null;
        public ICommand Dodaj
        {
            get
            {
                if (dodaj == null)
                {
                    dodaj = new RelayCommand(
                        arg => {
                            Zawodnicy.Add(new ZawodnikVM(new Zawodnik(AktualneImie.Trim(), AktualneNazwisko.Trim(), AktualnyWiek, AktualnaWaga)));
                            AktualneImie = domyslneImie;
                            AktualneNazwisko = domyslneNazwisko;
                            AktualnyWiek = domyslnyWiek;
                            AktualnaWaga = domyslnaWaga;
                            AktualnyIndeks = domyslnyIndeks;
                            OnPropertyChanged(nameof(AktualneImie), nameof(AktualneNazwisko), nameof(AktualnyWiek), nameof(AktualnaWaga), nameof(AktualnyIndeks));
                        },
                        arg => !(string.IsNullOrEmpty(AktualneImie) ||
                                AktualneImie == domyslneImie ||
                                string.IsNullOrEmpty(AktualneNazwisko) ||
                                AktualneNazwisko == domyslneNazwisko ||
                                CzyIstnieje(AktualneImie, AktualneNazwisko, AktualnyWiek, AktualnaWaga)));
                }
                return dodaj;
            }
        }

        public ICommand Modyfikuj
        {
            get
            {
                if (modyfikuj == null)
                {
                    modyfikuj = new RelayCommand(
                        arg =>
                        {
                            ZawodnikVM player = zawodnicy[AktualnyIndeks];
                            player.Imie = AktualneImie;
                            player.Nazwisko = AktualneNazwisko;
                            player.Wiek = AktualnyWiek;
                            player.Waga = AktualnaWaga;

                            AktualneImie = domyslneImie;
                            AktualneNazwisko = domyslneNazwisko;
                            AktualnyWiek = domyslnyWiek;
                            AktualnaWaga = domyslnaWaga;
                            AktualnyIndeks = domyslnyIndeks;
                            OnPropertyChanged(nameof(AktualneImie), nameof(AktualneNazwisko), nameof(AktualnyWiek), nameof(AktualnaWaga), nameof(AktualnyIndeks));
                        },
                        arg => AktualnyIndeks != -1);
                }
                return modyfikuj;
            }
        }

        public ICommand Usun
        {
            get
            {
                if (usun == null)
                {
                    usun = new RelayCommand(
                        arg =>
                        {
                            zawodnicy.RemoveAt(AktualnyIndeks);
                            AktualneImie = domyslneImie;
                            AktualneNazwisko = domyslneNazwisko;
                            AktualnyWiek = domyslnyWiek;
                            AktualnaWaga = domyslnaWaga;
                            AktualnyIndeks = domyslnyIndeks;
                            OnPropertyChanged(nameof(zawodnicy), nameof(AktualneImie), nameof(AktualneNazwisko), nameof(AktualnyWiek), nameof(AktualnaWaga), nameof(AktualnyIndeks));
                        },
                        arg => AktualnyIndeks != -1);
                }
                return usun;
            }
        }

        public ICommand WyczyscImie
        {
            get
            {
                if (wyczyscImie == null)
                {
                    wyczyscImie = new RelayCommand(
                        arg =>
                        {
                            AktualneImie = null;
                            OnPropertyChanged(nameof(AktualneImie));
                        },
                        arg => AktualneImie == domyslneImie);
                }
                return wyczyscImie;
            }
        }

        public ICommand WyczyscNazwisko
        {
            get
            {
                if (wyczyscNazwisko == null)
                {
                    wyczyscNazwisko = new RelayCommand(
                        arg =>
                        {
                            AktualneNazwisko = null;
                            OnPropertyChanged(nameof(AktualneNazwisko));
                        },
                        arg => AktualneNazwisko == domyslneNazwisko);
                }
                return wyczyscNazwisko;
            }
        }

        public ICommand UstawDomyslneImie
        {
            get
            {
                if (ustawDomyslneImie == null)
                {
                    ustawDomyslneImie = new RelayCommand(
                        arg =>
                        {
                            AktualneImie = domyslneImie;
                            OnPropertyChanged(nameof(AktualneImie));
                        },
                        arg => string.IsNullOrEmpty(AktualneImie));
                }
                return ustawDomyslneImie;
            }
        }

        public ICommand UstawDomyslneNazwisko
        {
            get
            {
                if (ustawDomyslneNazwisko == null)
                {
                    ustawDomyslneNazwisko = new RelayCommand(
                        arg =>
                        {
                            AktualneNazwisko = domyslneNazwisko;
                            OnPropertyChanged(nameof(AktualneNazwisko));
                        },
                        arg => string.IsNullOrEmpty(AktualneNazwisko));
                }
                return ustawDomyslneNazwisko;
            }
        }

        public ICommand Wczytaj
        {
            get
            {
                if (wczytaj == null)
                {
                    wczytaj = new RelayCommand
                    (
                        arg =>
                        {
                            ZawodnikVM player = zawodnicy[AktualnyIndeks];
                            AktualneImie = player.Imie;
                            AktualneNazwisko = player.Nazwisko;
                            AktualnyWiek = player.Wiek;
                            AktualnaWaga = player.Waga;
                            OnPropertyChanged(nameof(AktualneImie), nameof(AktualneNazwisko), nameof(AktualnyWiek), nameof(AktualnaWaga));
                        },
                        arg => AktualnyIndeks != -1);
                }
                return wczytaj;
            }
        }

        public ICommand Zapisz
        {
            get
            {
                return new RelayCommand(
                    arg => ZapiszZawodnikow(),
                    arg => true);
            }
        }
    }
}