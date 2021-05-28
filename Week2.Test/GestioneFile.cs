using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2.Test
{
    class GestioneFile
    {
        // Importa le task presenti sul file inserito da utente e le salva su una lista di Task.
        // Gestisce le eccezioni stampando in caso un messaggio di errore, e in caso di successo
        // notifica l'utente
        public static void ImportaTaskDaFile(out List<Task> tasks)
        {
            tasks = new List<Task>();

            string fileName;

            Console.Write("\nInserisci il nome del file sul Desktop da cui importare le task: ");
            fileName = Console.ReadLine();

            string path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop), fileName);
            try
            {
                using (StreamReader fileR = File.OpenText(path))
                {
                    string[] values;
                    string s;
                    Importanza imp;

                    while ((s = fileR.ReadLine()) != null)
                    {
                        Task t = new Task();
                        values = s.Split(";");
                        t.Descrizione = values[0];
                        t.DataScadenza = Convert.ToDateTime(values[1]);
                        imp = (Importanza)Enum.Parse(typeof(Importanza), values[2]);
                        t.LivelloImportanza = imp;
                        tasks.Add(t);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("\nTask importate con successo.\n");
        }

        // Crea una cartella sul desktop col nome inserito dall'utente su cui salva ciascuna
        // task presente sulla lista passata in ingresso (file vuoto in caso di lista vuota)
        public static void StampaTaskSuFile(List<Task> tasks)
        {
            string fileName;
            string path;

            Console.Write("\nInserisci il nome del file sul Desktop su cui salvare le task: ");
            fileName = Console.ReadLine();

            path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop), fileName);

            try
            {
                using (StreamWriter fileW = File.CreateText(path))
                {
                    foreach (Task t in tasks)
                        fileW.WriteLineAsync(t.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Task salvate con successo nel file: " + path);
        }
    }
}
