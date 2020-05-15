using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Piłkarze
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Zawodnik> team = new List<Zawodnik>();
        private readonly int minWiek = 15;
        private readonly int maxWiek = 55;
        private readonly string domyslneImie = "Podaj imię";
        private readonly string domyslneNazwisko = "Podaj nazwisko";
        private readonly string PlikDruzyny = Path.Combine(Environment.CurrentDirectory, @"team.txt");

        public MainWindow()
        {
            InitializeComponent();
            TworzComboBox();
            CheckSave();
            Wczytaj();
        }

        private void TworzComboBox()
        {
            for (int i = minWiek; i <= maxWiek; i++)
                Wiek_cb.Items.Add(i);
        }

        private void CheckSave()
        {
            if (!File.Exists(PlikDruzyny))
            {
                FileStream fs = File.Create(PlikDruzyny);
                fs.Close();
            }
        }

        private void Wczytaj()
        {
            string[] ZawodnikData = File.ReadAllLines(PlikDruzyny);
            if (ZawodnikData.Length < 1)
                DodajDomyslnychZawodnikow();
            else
                WczytajZPliku(ref ZawodnikData);
            AktualizujDruzyne();
        }

        private void DodajDomyslnychZawodnikow()
        {
            team.Add(new Zawodnik("Pablo", "Escobar", 49, 66.6));
            team.Add(new Zawodnik("Pablo", "Dżanusz", 21, 55.5));
            team.Add(new Zawodnik("Pablo", "Picasso", 19, 31.4));
        }

        private void WczytajZPliku(ref string[] data)
        {
            for (int i = 0; i < data.Length; i += 4)
            {
                string imie = data[i];
                string nazwisko = data[i + 1];
                bool PoprawnyWiek = Int32.TryParse(data[i + 2], out int age);
                bool PoprawnaWaga = Double.TryParse(data[i + 3], out double weight);
                if (imie != String.Empty && nazwisko != String.Empty && PoprawnyWiek && PoprawnaWaga)
                    team.Add(new Zawodnik(imie, nazwisko, age, weight));
            }
        }

        private void Imie_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == domyslneImie)
                ClearTextBox(ref tb);
        }

        private void Imie_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == String.Empty)
                ResetTextBox(ref tb, domyslneImie);
        }

        private void Nazwisko_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == domyslneNazwisko)
                ClearTextBox(ref tb);
        }

        private void Nazwisko_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == String.Empty)
                ResetTextBox(ref tb, domyslneNazwisko);
        }

        private void ClearTextBox(ref TextBox textBox)
        {
            textBox.Text = String.Empty;
            textBox.Foreground = Brushes.Black;
        }

        private void ResetTextBox(ref TextBox textBox, string defaultValue)
        {
            textBox.Text = defaultValue;
            textBox.Foreground = Brushes.Gray;
        }

        private void SetTextBox(ref TextBox textBox, string text)
        {
            textBox.Text = text;
            textBox.Foreground = Brushes.Black;
        }

        private void Waga_s_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Waga_s.IsLoaded)
                Waga_l.Content = e.NewValue.ToString("0.0", CultureInfo.GetCultureInfo("pl-PL")) + " kg";
        }

        private void ResetSlider(ref Slider slider)
        {
            slider.Value = slider.Minimum;
        }

        private void SetSlider(ref Slider slider, double value)
        {
            if (slider.Minimum > value || value > slider.Maximum) return;
            slider.Value = value;
        }

        private void ResetComboBox(ref ComboBox comboBox)
        {
            comboBox.SelectedIndex = 0;
        }

        private void SetComboBox(ref ComboBox comboBox, int index)
        {
            if (index < 0 || index > comboBox.Items.Count) return;
            comboBox.SelectedIndex = index;
        }

        private void Dodaj_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Imie_tb.Text == String.Empty || Imie_tb.Text == domyslneImie)
            {
                Imie_tb.BorderThickness = new Thickness(3, 3, 3, 3);
                Imie_tb.BorderBrush = Brushes.Red;
                Imie_tb.ToolTip = "Wprowadź imię!";
            }
            if (Nazwisko_tb.Text == String.Empty || Nazwisko_tb.Text == domyslneNazwisko)
            {
                Nazwisko_tb.BorderThickness = new Thickness(3, 3, 3, 3);
                Nazwisko_tb.BorderBrush = Brushes.Red;
                Nazwisko_tb.ToolTip = "Wprowadź nazwisko!";
            }
            if (Imie_tb.Text == String.Empty || Imie_tb.Text == domyslneImie || Nazwisko_tb.Text == String.Empty || Nazwisko_tb.Text == domyslneNazwisko) return;
            DodajZawodnika(Imie_tb.Text, Nazwisko_tb.Text, Wiek_cb.SelectedIndex + minWiek, Waga_s.Value);
            AktualizujDruzyne();
            ResetControls();
        }

        private void Modifikuj_btn_Click(object sender, RoutedEventArgs e)
        {
            team[Druzyna_lb.SelectedIndex].Modyfikuj(Imie_tb.Text, Nazwisko_tb.Text, minWiek + Wiek_cb.SelectedIndex, Waga_s.Value);
            Modifikuj_btn.IsEnabled = false;
            AktualizujDruzyne();
            ResetControls();
        }

        private void Usun_btn_Click(object sender, RoutedEventArgs e)
        {
            UsunZawodnika(Druzyna_lb.SelectedIndex);
            AktualizujDruzyne();
            ResetControls();
        }

        private void DodajZawodnika(string name, string lastName, int age, double weight)
        {
            team.Add(new Zawodnik(name, lastName, age, weight));
        }

        private void SetControls(Zawodnik Zawodnik)
        {
            SetTextBox(ref Imie_tb, Zawodnik.Imie);
            SetTextBox(ref Nazwisko_tb, Zawodnik.Nazwisko);
            SetComboBox(ref Wiek_cb, Zawodnik.Wiek - minWiek);
            SetSlider(ref Waga_s, Zawodnik.Waga);
        }

        private void UsunZawodnika(int index)
        {
            team.RemoveAt(index);
            for (int i = index + 1; i < team.Count; i++)
            {
                team[i - 1] = team[i];
            }
        }

        private void AktualizujDruzyne()
        {
            Druzyna_lb.ItemsSource = null;
            Druzyna_lb.ItemsSource = team;
        }

        private void ResetControls()
        {
            ResetTextBox(ref Imie_tb, domyslneImie);
            ResetTextBox(ref Nazwisko_tb, domyslneNazwisko);
            ResetSlider(ref Waga_s);
            ResetComboBox(ref Wiek_cb);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            using (StreamWriter file = new StreamWriter(PlikDruzyny))
            {
                foreach (Zawodnik Zawodnik in team)
                {
                    file.WriteLine(Zawodnik.Imie);
                    file.WriteLine(Zawodnik.Nazwisko);
                    file.WriteLine(Zawodnik.Wiek);
                    file.WriteLine(Zawodnik.Waga);
                }
            }
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int index = Druzyna_lb.SelectedIndex;
            if (index == -1) return;
            SetControls(team[index]);
            Modifikuj_btn.IsEnabled = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.BorderThickness = new Thickness(1, 1, 1, 1);
            tb.BorderBrush = Brushes.Black;
            tb.ToolTip = null;
        }
    }
}
