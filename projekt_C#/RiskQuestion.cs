using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentProject
{
    public class RiskQuestion : Question
    {
        public RiskQuestion(int id, string text, int time, QuestionOption a, QuestionOption b)
            : base(id, text, time) { Options.Add(a); Options.Add(b); }

        public override string GetQuestionType() => "Pytanie o ryzyko";

        public override void ApplyEffect(Participant p, string answer)
        {
            var opt = Options.Find(o => o.Key == answer);
            if (opt != null) p.Capital += opt.CapitalChange;
        }
    }
}
