using System;
using System.Collections.Generic;

namespace ConsoleAppMenu
{
    public class Cibo
    {
        public string Nome { get; set; }
        public decimal Prezzo { get; set; }

        public Cibo(string nome, decimal prezzo)
        {
            Nome = nome;
            Prezzo = prezzo;
        }

        public override string ToString()
        {
            return $"{Nome} (€ {Prezzo:F2})";
        }
    }

    public class Program
    {
        static List<Cibo> menu = new List<Cibo>
        {
            new Cibo("Coca Cola 150 ml", 2.50m),
            new Cibo("Insalata di pollo", 5.20m),
            new Cibo("Pizza Margherita", 10.00m),
            new Cibo("Pizza 4 Formaggi", 12.50m),
            new Cibo("Pz patatine fritte", 3.50m),
            new Cibo("Insalata di riso", 8.00m),
            new Cibo("Frutta di stagione", 5.00m),
            new Cibo("Pizza fritta", 5.00m),
            new Cibo("Piadina vegetariana", 6.00m),
            new Cibo("Panino Hamburger", 7.90m)
        };

        static List<Cibo> ordine = new List<Cibo>();
        static decimal servizio = 3.00m;

        static void Main(string[] args)
        {
            bool uscita = false;

            while (!uscita)
            {
                StampaMenu();
                int scelta = LeggiScelta();

                if (scelta == 11)
                {
                    StampaContoFinale();
                    uscita = true;
                }
                else if (scelta >= 1 && scelta <= 10)
                {
                    ordine.Add(menu[scelta - 1]);
                    Console.WriteLine($"{menu[scelta - 1].Nome} aggiunto all'ordine.");
                }
                else
                {
                    Console.WriteLine("Scelta non valida. Riprova.");
                }
            }
        }

        static void StampaMenu()
        {
            Console.Clear();
            Console.WriteLine("==============MENU==============");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {menu[i]}");
            }
            Console.WriteLine("11: Stampa conto finale e conferma");
            Console.WriteLine("==============MENU==============");
        }

        static int LeggiScelta()
        {
            Console.Write("Seleziona un'opzione: ");
            if (int.TryParse(Console.ReadLine(), out int scelta))
            {
                return scelta;
            }
            else
            {
                return -1;
            }
        }

        static void StampaContoFinale()
        {
            Console.Clear();
            Console.WriteLine("==============CONTO==============");
            decimal totale = 0;
            foreach (var cibo in ordine)
            {
                Console.WriteLine($"{cibo.Nome} - € {cibo.Prezzo:F2}");
                totale += cibo.Prezzo;
            }
            totale += servizio;
            Console.WriteLine("=================================");
            Console.WriteLine($"Servizio al tavolo: € {servizio:F2}");
            Console.WriteLine($"Totale: € {totale:F2}");
            Console.WriteLine("=================================");
            Console.WriteLine("Grazie per aver ordinato!");
        }
    }
}
