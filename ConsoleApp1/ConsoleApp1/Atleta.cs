internal class Atleta
{
    string nome;
    string cognome;
    string sport;

    public string Nome { get { return nome; } set { nome = value; } }
    public string Cognome { get { return cognome; } set { cognome = value; } }
    public string Sport { get { return sport; } set { sport = value; } }

    public void Descriviti()
    {
        Console.WriteLine("Mi chiamo " + nome + " " + cognome + " e pratico " + sport);
    }
}