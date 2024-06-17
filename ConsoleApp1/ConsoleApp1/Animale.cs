internal class Animale
{
    string razza;
    string tipo;
    string colore;

    public string Razza { get { return razza; } set { razza = value; } }
    public string Tipo { get { return tipo; } set { tipo = value; } }
    public string Colore { get { return colore; } set { colore = value; } }

    public void Descriviti()
    {
        Console.WriteLine("Questo è un  " + tipo + "di razza " + razza + ", è di colore " + colore );
    }
}