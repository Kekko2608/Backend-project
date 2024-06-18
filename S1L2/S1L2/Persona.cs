using System.Runtime.CompilerServices;

namespace S1L2
{
    internal class Persona
    {
        string nome;
        string cognome;
        int eta;

        public string Nome { get { return nome; } set { nome = value; } }
        public string Cognome { get { return cognome; } set { cognome = value; } }
        public int Eta { get { return eta; } set { eta = value; } }


        public string GetNome()
        {
            return nome;
        }

        public string GetCognome()
        {
            return cognome;
        }

        public int GetEta()
        {
            return eta;
        }

        public string GetDettagli()
        {
            return nome + cognome + eta;
        }
    }
}