

public class Program
{
    public static void Main(string[] args)
    {
        Atleta a = new Atleta();
        a.Nome = "Mario";
        a.Cognome = "Rossi";
        a.Sport = "Baseball";
        a.Descriviti();

        Dipendente b = new Dipendente();
        b.Nome = "Luca";
        b.Cognome = "Rossi";
        b.Età = 32;
        b.Incarico = "Direttore";
        b.Descriviti();

        Animale c = new Animale();
        c.Razza="Carlino";
        c.Tipo= "Cane";
        c.Colore="Grigio";
        c.Descriviti();

        Veicolo d = new Veicolo();
        d.Marca = "Ferrari";
        d.Modello = "California";
        d.Colore = "Rosso";
        d.Prezzo = 200000;
        d.Descriviti();
    }
}