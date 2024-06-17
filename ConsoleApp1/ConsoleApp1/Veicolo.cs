internal class Veicolo
{
    string marca;
    string modello;
    string colore;
    int prezzo;

    public string Marca { get { return marca; } set { marca = value; } }
    public string Modello { get { return modello; } set { modello = value; } }
    public string Colore { get { return colore; } set { colore = value; } }
    public int Prezzo { get { return prezzo; } set { prezzo = value; } }

    public void Descriviti()
    {
        Console.WriteLine("Questa è una " + marca +" "+ modello +" "+ "colore " + colore + " che ha un prezzo di " + prezzo +"€");
    }
}