using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piłkarze
{
    class Zawodnik
    {
        private string imie, nazwisko;
        private int wiek;
        private double waga;

        public Zawodnik(string _imie, string _nazwisko, int _wiek, double _waga)
        {
            imie = _imie;
            nazwisko = _nazwisko;
            wiek = _wiek;
            waga = _waga;
        }

        public void Modyfikuj(string name, string lastName, int age, double weight)
        {
            imie = name;
            nazwisko = lastName;
            wiek = age;
            waga = weight;
        }

        public string Imie { get { return imie; } }
        public string Nazwisko { get { return nazwisko; } }
        public int Wiek { get { return wiek; } }
        public double Waga { get { return waga; } }

        public string FullInfo { get { return imie + " " + nazwisko + " - Wiek: " + wiek + ", Waga: " + waga; } }
    }
}
