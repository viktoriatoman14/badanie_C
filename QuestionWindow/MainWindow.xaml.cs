using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using ExperimentProject;

namespace ExperimentProject
{
    public partial class QuestionWindow : Window
    {
        private Participant _participant;
        // WYMAGANIE 1: Kolekcja List
        private List<Question> _questions;
        private int _index = 0;
        private DispatcherTimer _timer;
        private int _seconds;

        public QuestionWindow(Participant p)
        {
            InitializeComponent();
            _participant = p;

            // Przykładowe pytania
            _questions = new List<Question> {
                new RiskQuestion(1, "Wolisz na 80% zyskać 700zł czy na 100% zyskać 500zł?", 10,
                    new QuestionOption("A", "na 80% zyskać 700", 700),
                    new QuestionOption("B", "na 100% zyskać 500", 500)),
                new RiskQuestion(2, "Wolisz na 80% stracić 1500zł (20% szans, że nie stracisz nic) czy na 100% stracić 1000zł?", 10,
                    new QuestionOption("A", "na 80% stracić 1 500 (20% szans na stratę 0)", 0),
                    new QuestionOption("B", "na 100% stracić 1 000", -1000)),
                new RiskQuestion(3, "Wolisz na 80% zyskać 1500zł czy na 100% zyskać 1000zł?",10,
                    new QuestionOption("A", "Tracę 200", -200),
                    new QuestionOption("B", "Ryzykuję stratę 500", -500)),
                new RiskQuestion(4, "Wolisz na 80% stracić 4000zł (20% szans, że nie stracisz nic) czy na 100% stracić 3000zł?",10,
                        new QuestionOption("A", "na 80% stracić 4 000 (20% szans na stratę 0)", 0),
                        new QuestionOption("B", "na 100% stracić 3 000", -3000)),
                new RiskQuestion(5,  "Wolisz na 80% zyskać 2000zł czy na 100% zyskać 1500zł?",10,
                        new QuestionOption("A", "na 80% zyskać 2 000", 2000),
                        new QuestionOption("B", "na 100% zyskać 1 500", 1500)),
                new RiskQuestion(6,  "Wolisz na 80% stracić 7000zł (20% szans, że nie stracisz nic) czy na 100% stracić 5000zł?",10,
                        new QuestionOption("A", "na 80% stracić 7 000 (20% szans na stratę 0)", -7000),
                        new QuestionOption("B", "na 100% stracić 5 000", -5000)),
                new RiskQuestion(7,  "Wolisz na 80% zyskać 2500zł czy na 100% zyskać 2000zł?",10,
                        new QuestionOption("A", "na 80% zyskać 2 500", 2500),
                        new QuestionOption("B", "na 100% zyskać 2 000", 2000)),
                new RiskQuestion(8,  "Wolisz na 10% zyskać 10 000zł czy na 1% zyskać 100 000zł?",10,
                        new QuestionOption("A", "na 10% zyskać 10 000", 10000),
                        new QuestionOption("B", "na 1% zyskać 100 000", 100000)),
                new RiskQuestion(9,   "Wolisz na 50% zyskać 2000zł czy na 100% zyskać 1 000zł?",10,
                        new QuestionOption("A","na 50% zyskać 2 000", 2000),
                        new QuestionOption("B", "na 100% zyskać 1 000", 1000)),
                new RiskQuestion(10,   "Wolisz na 20% zyskać 10 000zł czy na 100% zyskać 2000zł?",10,
                        new QuestionOption("A","na 20% zyskać 10 000", 0),
                        new QuestionOption("B", "na 100% zyskać 2 000", 2000)),
                new RiskQuestion(11,   "Wolisz na 90% stracić 3000zł czy na 45% stracić 6000zł?",10,
                        new QuestionOption("A","na 90% stracić 3 000", -3000),
                        new QuestionOption("B","na 45% stracić 6 000", -6000)),
                new RiskQuestion(12,    "Wolisz na 90% zyskać 3000zł czy na 45% zyskać 6000zł?",10,
                        new QuestionOption("A", "na 90% zyskać 3 000", 3000),
                        new QuestionOption("B", "na 45% zyskać 6 000", 6000)),
                new RiskQuestion(13,     "Wolisz szanse 50/50 na zyskanie lub stracenie 5 000zł czy 2 000zł?",10,
                        new QuestionOption("A", "50/50 ±5 000", -5000),
                        new QuestionOption("B", "50/50 ±2 000", -2000)),

            };

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => {
                _seconds--;
                lblStatus.Text = $"Czas: {_seconds}s | Kapitał: {_participant.Capital}";
                if (_seconds <= 0) NextQuestion("TIMEOUT");
            };

            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (_index >= _questions.Count)
            {
                Storage.Save(_participant); // Zapisz wynik
                MessageBox.Show("Koniec! Wynik zapisano do wynik.json");
                Application.Current.Shutdown();
                return;
            }

            var q = _questions[_index];
            lblType.Text = q.GetQuestionType(); // Wywołanie metody wirtualnej
            lblText.Text = q.Text;
            btnA.Content = q.Options[0].Label;
            btnB.Content = q.Options[1].Label;
            _seconds = q.TimeLimit;
            _timer.Start();
        }

        private void AnswerClick(object sender, RoutedEventArgs e)
        {
            string choice = (sender as System.Windows.Controls.Button).Tag.ToString();
            NextQuestion(choice);
        }

        private void NextQuestion(string answer)
        {
            _timer.Stop();
            if (answer == "TIMEOUT") _participant.Capital -= 100;
            else _questions[_index].ApplyEffect(_participant, answer); // Polimorfizm w akcji

            _index++;
            ShowQuestion();
        }
    }
}