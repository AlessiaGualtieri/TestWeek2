using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2.Test
{
    public enum Importanza
    {
        Basso,
        Medio,
        Alto
    }

    public class Task
    {
        public String Descrizione { get; set; }
        public DateTime DataScadenza { get; set; }
        public Importanza LivelloImportanza { get; set; }


        public override string ToString()
        {
            return $"Descrizione: {Descrizione}\n" +
                $"Data di scadenza: {DataScadenza}\n" +
                $"Livello di importanza: {LivelloImportanza}\n";
        }
    }
}
