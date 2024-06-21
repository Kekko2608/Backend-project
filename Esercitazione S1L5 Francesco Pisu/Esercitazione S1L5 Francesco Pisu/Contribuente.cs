using System;

namespace Esercitazione_S1L5_Francesco_Pisu
{
    public class Contribuente
    {
        public enum Sesso
        {
            M,
            F
        }

        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public Sesso Sex { get; set; }
        public string ComuneResidenza { get; set; }
        public double RedditoAnnuale { get; set; }
        public double ImpostaDovuta { get; set; }

        public Contribuente MenuContribuente()
        {
            Console.WriteLine("Inserisci nome");
            string inputNome = Console.ReadLine();

            Console.WriteLine("Inserisci cognome");
            string inputCognome = Console.ReadLine();

            Console.WriteLine("Inserisci data di nascita");
            string inputData = Console.ReadLine();

            Console.WriteLine("Inserisci codice fiscale");
            string inputCF = Console.ReadLine().ToUpper();

            Sesso sessoEnum;
            while (true)
            {
                Console.WriteLine("Inserisci sesso (M o F)");
                string inputSesso = Console.ReadLine();

                if (Enum.TryParse(inputSesso, true, out sessoEnum) && (sessoEnum == Sesso.M || sessoEnum == Sesso.F))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Sesso non valido. Inserisci 'M' o 'F'.");
                }
            }

            Console.WriteLine("Inserisci comune di residenza");
            string inputResidenza = Console.ReadLine();

            Console.WriteLine("Inserisci reddito annuale");
            double inputReddito = double.Parse(Console.ReadLine());

            Contribuente nuovoContribuente = new Contribuente
            {
                Nome = inputNome,
                Cognome = inputCognome,
                DataNascita = inputData,
                CodiceFiscale = inputCF,
                Sex = sessoEnum,
                ComuneResidenza = inputResidenza,
                RedditoAnnuale = inputReddito
            };

            return nuovoContribuente;
        }

        public void AgenziaEntrate()
        {
            Console.WriteLine("         Agenzia   Entrate               ");
            Console.WriteLine("=========================================");
            Console.WriteLine();
            Console.WriteLine("Scegli un opzione:");
            Console.WriteLine();
            Console.Write("1| Nuovo contribuente      ");
            Console.Write("    2| Esci dal programma");

            int Scelta = int.Parse(Console.ReadLine());

            switch (Scelta)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("Opzione 1");
                    Contribuente nuovoContribuente = MenuContribuente();
                    if (nuovoContribuente != null)
                    {
                        CalcolaImposta(nuovoContribuente);
                    }
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("Opzione 2");
                    Console.WriteLine("Uscita dal programma");
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Opzione non valida, riprova");
                    Console.WriteLine();
                    break;
            }
        }

        public static void CalcolaImposta(Contribuente contribuente)
        {
            double reddito = contribuente.RedditoAnnuale;

            if (reddito <= 15000)
            {
                contribuente.ImpostaDovuta = reddito * 0.23;
            }
            else if (reddito <= 28000)
            {
                contribuente.ImpostaDovuta = 3450 + (reddito - 15000) * 0.27;
            }
            else if (reddito <= 55000)
            {
                contribuente.ImpostaDovuta = 6960 + (reddito - 28000) * 0.38;
            }
            else if (reddito <= 75000)
            {
                contribuente.ImpostaDovuta = 17220 + (reddito - 55000) * 0.41;
            }
            else
            {
                contribuente.ImpostaDovuta = 25420 + (reddito - 75000) * 0.43;
            }

            Console.WriteLine();
            Console.WriteLine($"CALCOLO DELL’IMPOSTA DA VERSARE:");
            Console.WriteLine();
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
            Console.WriteLine();
            Console.WriteLine($"Nato il {contribuente.DataNascita} ({contribuente.Sex}),");
            Console.WriteLine();
            Console.WriteLine($"Residente in {contribuente.ComuneResidenza},");
            Console.WriteLine();
            Console.WriteLine($"Codice fiscale: {contribuente.CodiceFiscale}");
            Console.WriteLine();
            Console.WriteLine($"Reddito dichiarato: EURO {contribuente.RedditoAnnuale}");
            Console.WriteLine();
            Console.WriteLine($"IMPOSTA DA VERSARE: EURO {contribuente.ImpostaDovuta}");
        }
    }

}
