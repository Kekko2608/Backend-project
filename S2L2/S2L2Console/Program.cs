namespace S2L2Console
{

   public class CV
    {
        public InformazioniPersonali InformazioniPersonali { get; set; }
        public List<Studi> StudiEffettuati { get; set; }
        public List<Impiego> Impieghi { get; set; }

    }

   public class InformazioniPersonali
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }


    public class Studi
    {
        public string Qualifica { get; set; }
        public string Istituto { get; set; }
        public string Tipo { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
    }

   public class Impiego
    {
        public Esperienza Esperienza { get; set; }

    }

   public class Esperienza
    {
        public string Azienda { get; set; }
        public string JobTitle { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public string Descrizione { get; set; }
        public List<string> Compiti { get; set; }
    }





    internal class Program
    {
        static void Main(string[] args)
        {
            CV mioCV = new CV
            {
                InformazioniPersonali = new InformazioniPersonali
                {
                    Nome = "Mario",
                    Cognome = "Rossi",
                    Telefono = "3489765456",
                    Email = "mariorossi@gmail.com"
                },

                StudiEffettuati = new List<Studi>
                {
                    new Studi
                    {
                        Qualifica = "Programmatore",
                        Istituto = "EPICODE",
                        Tipo = "Full-stack",
                        Dal = new DateTime(2024, 11, 1),
                        Al = new DateTime(2024, 5, 1)
                    }
                },

                Impieghi = new List<Impiego>
                {
                    new Impiego
                    {
                        Esperienza = new Esperienza
                        {
                            Azienda = "Tech Solutions",
                            JobTitle = "Sviluppatore Web",
                            Dal = new DateTime(2019, 1, 1),
                            Al = new DateTime(2022, 1, 1),
                            Descrizione = "Sviluppo di applicazioni web",
                            Compiti = new List<string>
                            {
                                "Progettazione di architetture software",
                                "Scrittura di codice pulito e manutenibile",
                                "Collaborazione con il team di sviluppo"
                            }
                        }

                    }
                },
            };

            StampaCV(mioCV);

        }

        static void StampaCV (CV cv )
        {
            Console.WriteLine($"CV di {cv.InformazioniPersonali.Nome} {cv.InformazioniPersonali.Cognome} ");
            Console.WriteLine();
            Console.WriteLine("+++++ INIZIO Informazioni Personali: +++++");
            Console.WriteLine($"Nome: {cv.InformazioniPersonali.Nome}");
            Console.WriteLine($"Cognome: {cv.InformazioniPersonali.Cognome}");
            Console.WriteLine($"Email: {cv.InformazioniPersonali.Email}");
            Console.WriteLine($"Cognome: {cv.InformazioniPersonali.Cognome}");
            Console.WriteLine($"Telefono: {cv.InformazioniPersonali.Telefono}");
            Console.WriteLine("+++++ FINE Informazioni Personali: +++++");
            Console.WriteLine();
            Console.WriteLine("+++++ INIZIO Studi e Formazione: +++++");
            foreach (var studi in cv.StudiEffettuati)
            {
                Console.WriteLine($"Istituto: {studi.Istituto}");
                Console.WriteLine($"Qualifica: {studi.Qualifica}");
                Console.WriteLine($"Tipo: {studi.Tipo}");
                Console.WriteLine($"Dal {studi.Dal} {studi.Al}");
                Console.WriteLine();
            }
            Console.WriteLine("+++++ FINE Studi e Formazione: +++++");

            Console.WriteLine("+++++ INIZIO Esperienze Professionali: +++++");
            foreach (var impiego in cv.Impieghi)
            {
                var esperienza = impiego.Esperienza;
                {
                    Console.WriteLine($"Presso: {esperienza.Azienda}");
                    Console.WriteLine($"Tipo di lavoro: {esperienza.JobTitle}");
                    Console.WriteLine("Compiti:");
                    foreach (var compito in esperienza.Compiti)
                    {
                        Console.WriteLine($"{compito}");
                    }
                }
            }
            Console.WriteLine("+++++ FINE Esperienze Professionali: +++++");
        }

    }
}
