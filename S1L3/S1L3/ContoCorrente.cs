using System.Runtime.CompilerServices;

namespace S1L3
{
    public class ContoCorrente
    {
        private string _nomeCorrentista;
        public string NomeCorrentista { get { return _nomeCorrentista; } set { _nomeCorrentista = value; } }

        private string _cognomeCorrentista;
        public string CognomeCorrentista { get { return _cognomeCorrentista; } set { _cognomeCorrentista = value; } }

        private decimal _saldo=0;
        public decimal Saldo { get { return _saldo; } set { _saldo = value; } }

        private bool _contoAperto = false;
        public bool ContoAperto { get { return _contoAperto; } set { _contoAperto = value; } }

        public void MenuIniziale()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("     B A N C O  DI  S A R D E G N A       ");
            Console.WriteLine("==========================================");

            Console.WriteLine("\n Scegli l'operazione da effettuare:");
            Console.WriteLine("1. APRI NUOVO CONTO CORRENTE");
            Console.WriteLine("2. EFFETTUA UN VERSAMENTO");
            Console.WriteLine("3. EFFETTUA UN PRELEVAMENTO");
            Console.WriteLine("4. SALDO DISPONIBILE");
            Console.WriteLine("5. ESCI");

            int Scelta = int.Parse(Console.ReadLine());
            if (Scelta == 1)
            {
                ApriContoCorrente();
            } else if (Scelta == 2)
            {
                EffettuaVersamento();
            } else if (Scelta == 3)
            {
                EffettuaPrelevamento();
            } else if (Scelta == 4) {

                SaldoAlMomento();
            } else if (Scelta == 5)
            {
                Console.WriteLine("Uscita in corso");
            } else
            {
                Console.WriteLine("Hai selezionato una voce non valida.");
                MenuIniziale();
            }
        }
            public void ApriContoCorrente()
            {
                Console.WriteLine("Nome Correntista : ");
                string Nome = Console.ReadLine();

                Console.WriteLine("Cognome Correntista : ");
                string Cognome = Console.ReadLine();

                ContoCorrente a = new ContoCorrente();
                _nomeCorrentista = Nome;
                _cognomeCorrentista = Cognome;
                _saldo = 0;
                _contoAperto = true;

                Console.WriteLine($"Conto corrente nr. 2665412 intestato a:  {_nomeCorrentista} {_cognomeCorrentista} aperto correttamente");
                MenuIniziale();

            }

            public void EffettuaVersamento()
            {

                if (ContoAperto == false)
                {
                    Console.WriteLine("E' necessario aprire un conto corrente");
                } else
                {
                    Console.WriteLine("Scrivere importo da voler versare : ");
                    decimal ImportoVersato = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("Versamento effettuato");
                    _saldo += ImportoVersato;


                }
                MenuIniziale();
            }

        public void SaldoAlMomento ()
        {
            Console.WriteLine($"Il saldo disponibile è {_saldo}");
            MenuIniziale();
        }

            public void EffettuaPrelevamento()
            {
                if (ContoAperto == false)
                {
                    Console.WriteLine("E' necessario aprire un conto corrente per poter prelevare");

                } else
                {
                    Console.WriteLine("Scrivere importo da prelevare : ");
                    decimal importoPrelevato = Decimal.Parse(Console.ReadLine());

                    if(importoPrelevato > _saldo)
                    {
                        Console.WriteLine("Importo non disponibile");
                    } else
                    {
                        _saldo -= importoPrelevato;
                        Console.WriteLine("Prelievo effettuato.");
                        
                    }
                }
                MenuIniziale();
            }


    }
}