namespace S1L2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Persona a = new Persona();
            a.Nome = "Mario";
            a.Cognome = "Rossi";
            a.Eta = 30;
            Console.WriteLine(a.GetNome());
            Console.WriteLine(a.GetCognome());
            Console.WriteLine(a.GetEta());
            Console.WriteLine(a.GetDettagli());
        }
    }
}
