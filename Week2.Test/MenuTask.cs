using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2.Test
{
    class MenuTask
    {
        public static List<Task> tasks = new List<Task>();

        // Reitera la stampa del menu e l'operazione richiesta finchè non viene inserito
        // un valore opportuno
        public static void Menu()
        {
            int scelta;
            do
            {
                scelta = StampaMenu();

                AnalizzaScelta(scelta);

            } while (scelta != 5);
        }


        // Stampa il menu e richiede di inserire la scelta, controllandola finchè non 
        // viene inserita una opportuna, restituendola.
        public static int StampaMenu()
        {
            int scelta;
            bool succ;

            Console.WriteLine("\n----- Menu -----\n" +
                "0 - Importa le task da file\n" +
                "1 - Visualizza le task inserite\n" +
                "2 - Aggiungi una nuova task\n" +
                "3 - Elimina una task\n" +
                "4 - Filtra le task per importanza\n" +
                "5 - Esci\n");

            do
            {
                succ = Int32.TryParse(Console.ReadLine(), out scelta);
                if (!succ || scelta < 0 || scelta > 5)
                    Console.Write("Errore. Inserire un valore opportuno: ");
            } while (!succ || scelta < 0 || scelta > 5);
            
            return scelta;
        }

        // Analizza la scelta dell'utente eseguendo il comando richiesto
        public static void AnalizzaScelta(int scelta)
        {
            switch (scelta)
            {
                case 0:
                    GestioneFile.ImportaTaskDaFile(out tasks);
                    break;
                case 1:
                    VediTask();
                    break;
                case 2:
                    AggiungiTask();
                    break;
                case 3:
                    EliminaTask();
                    break;
                case 4:
                    FiltraTask();
                    break;
            }
        }

        
        //Richiede all'utente di inserire una nuova task, controllando i valori inseriti.
        //Quindi la inserisce
        public static void AggiungiTask()
        {
            Task t = new Task();
            bool succ;
            DateTime date;
            int imp;

            Console.WriteLine("\nInserisci i dati della task che vuoi inserire:\n");
            // Descrizione task:
            Console.Write("Descrizione: ");
            t.Descrizione = Console.ReadLine();

            // Data di scadenza task:
            Console.Write("Data di scadenza: ");
            do
            {
                succ = DateTime.TryParse(Console.ReadLine(), out date);
                if (!succ || date.CompareTo(DateTime.Now) <= 0 )
                    Console.Write("Errore. Inserire una data opportuna: ");
            } while (!succ || date.CompareTo(DateTime.Now) <= 0 );
            t.DataScadenza = date;

            // Livello di importanza task:
            Console.WriteLine("Livello di importanza: ");
            imp = VisualizzaScegliImportanza();
            t.LivelloImportanza = (Importanza)Enum.ToObject(typeof(Importanza),imp);

            tasks.Add(t);
        }

        // Mostra a video tutte le task (notificando in caso non ne sia presente alcuna),
        // quindi crea un file su Desktop di nome 'taskStampate.txt' contenente tutte le task
        // con opportuna descrizione (file vuoto in caso non ne sia presente alcuna)
        public static void VediTask()
        {
            if (tasks.Count == 0)
                Console.WriteLine("\nNon è presente alcuna Task.\n");
            foreach (Task t in tasks)
                Console.WriteLine("\n" + t.ToString() );

            GestioneFile.StampaTaskSuFile(tasks);
        }

        // Mostra a video tutte le task inserite, numerandole. Richiede quindi
        // la scelta di un indice, controllandolo, e elimina la task richiesta.
        // In caso non siano presenti task lo dichiara a video e termina.
        public static void EliminaTask()
        {
            int i = 1;
            bool succ;

            if (tasks.Count == 0)
            {
                Console.WriteLine("\nNon è presente alcuna task.\n");
                return;
            }
                
            Console.WriteLine("Tutte le task sono:\n");
            foreach (Task t in tasks)
            {
                Console.WriteLine(i + ")\n" + t);
                i++;
            }

            Console.Write("Seleziona la task che vuoi eliminare: ");
            do
            {
                succ = Int32.TryParse(Console.ReadLine(), out i);
                if (!succ || i < 1 || i > tasks.Count() )
                    Console.Write("Errore. Inserire una task opportuna: ");
            } while (!succ || i < 1 || i > tasks.Count() );

            tasks.RemoveAt(i - 1);
            Console.WriteLine("\nEliminazione avvenuta con successo.\n");
        }

        // In caso non sia presente alcuna task, termina avvisando l'utente.
        // Richiede ad utente di selezionare il livello di importanza con cui filtrare
        // stampando a video tutte le importanze, quindi stampa a video tutte le task
        // con l'importanza richiesta. In caso non ne sia presente alcuna, avvisa l'utente.
        public static void FiltraTask()
        {
            int impInt, i = 0;
            Importanza impScelta;

            if(tasks.Count == 0 )
            {
                Console.WriteLine("\nNon è presente alcuna task.\n");
                return;
            }

            Console.WriteLine("\nSelezionare il livello di importanza con cui filtrare le task:\n");
            impInt = VisualizzaScegliImportanza();
            impScelta = (Importanza)Enum.ToObject(typeof(Importanza), impInt);

            Console.WriteLine("\nTutte le task con livello di importanza {0} sono:\n",impScelta);
            foreach(Task t in tasks)
            {
                if (t.LivelloImportanza == impScelta)
                {
                    i++;
                    Console.WriteLine(t + "\n");
                }
            }

            if (i == 0)
                Console.WriteLine("Non è presente alcuna task con livello di importanza {0}", impScelta);
        }

        // Stampa a video i livelli di importanza e ne richiede la scelta finchè non 
        // ottiene un valore opportuno, restituendolo.
        public static int VisualizzaScegliImportanza ()
        {
            int[] values = { 0, 1, 2 };
            bool succ;
            int imp;

            foreach (int v in values)
                Console.WriteLine(v + " - " + Enum.GetName(typeof(Importanza), v));
            do
            {
                succ = Int32.TryParse(Console.ReadLine(), out imp);
                if (!succ || imp < 0 || imp > 2)
                    Console.Write("Errore. Inserire un valore opportuno: ");
            } while (!succ || imp < 0 || imp > 2);

            return imp;
        }

    }
}
