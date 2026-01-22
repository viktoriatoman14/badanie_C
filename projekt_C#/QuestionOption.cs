using System;
using System.Collections.Generic;
using System.Text;

namespace ExperimentProject
{
    public class QuestionOption
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public int CapitalChange { get; set; }
        public QuestionOption(string key, string label, int change)
        { Key = key; Label = label; CapitalChange = change; }
    }
}
