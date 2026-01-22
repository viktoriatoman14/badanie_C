using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentProject
{
    public abstract class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TimeLimit { get; set; }
        public List<QuestionOption> Options { get; set; } = new List<QuestionOption>();

        protected Question(int id, string text, int time)
        { Id = id; Text = text; TimeLimit = time; }

        // WYMAGANIE 5: Metoda wirtualna
        public virtual string GetQuestionType() => "Podstawowe pytanie";

        // WYMAGANIE 3: Polimorfizm (metoda abstrakcyjna do nadpisania)
        public abstract void ApplyEffect(Participant p, string answer);
    }
}
