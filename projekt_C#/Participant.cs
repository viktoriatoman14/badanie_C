using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentProject
{ 
    public class ParticipantException : Exception
    {
        public ParticipantException(string message) : base(message) { }
    }

    public class Participant
    {
        private int _age;
        // WYMAGANIE 2: Weryfikacja danych i modyfikatory (Properties)
        public int Age
        {
            get => _age;
            set
            {
                if (value < 18 || value > 120) throw new ParticipantException("Wiek musi być 18-120!");
                _age = value;
            }
        }
        public string Gender { get; set; }
        public string Kierunek { get; set; }
        public string Stres { get; set; }
        public string Zamoznosc { get; set; }
        public decimal Capital { get; set; } = 1000;
        public Guid Uuid { get; } = Guid.NewGuid();
    }
}
