using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ExperimentProject
{
    public static class Storage
    {
        public static void Save(object data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("wynik.json", json);
        }
    }
}
