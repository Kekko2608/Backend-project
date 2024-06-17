internal class Dipendente
{
    string nome;
    string cognome;
    int età;
    string incarico;

    public string Nome { get { return nome; } set { nome = value; } }
    public string Cognome { get { return cognome; } set { cognome = value; } }
    public int Età { get { return età; } set { età = value; } }
    public string Incarico { get { return incarico; } set { incarico = value; } }

    public void Descriviti()
    {
        Console.WriteLine("Mi chiamo " + nome + " " + cognome + " ho " + età + "anni e sono un" + incarico );
    }
}