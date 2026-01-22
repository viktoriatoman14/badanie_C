using System;
using System.Windows;
using ExperimentProject;
using MainWindow;

namespace ExperimentProject
{
    public partial class MainWindow : Window
    {
        public MainWindow() { InitializeComponent(); }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Participant p = new Participant
                {
                    Age = int.Parse(txtAge.Text),
                    Gender = txtGender.Text,
                    Stres = txtStres.Text,
                    Kierunek = txtKierunek.Text,
                    Zamoznosc = txtZamoznosc.Text,
                    
                };
                // Przechodzimy do okna pytania
                new QuestionWindow(p).Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }
    }
}