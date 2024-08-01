
// aggiunta all' ordine + quantità
$(document).ready(function () {
    function aggiungiProdotto(prodottoId) {
        var quantita = $('#quantita_' + prodottoId).val();

        if (quantita <= 0) {
            alert('La quantità deve essere maggiore di zero.');
            return;
        }

        $.ajax({
            url: '/Ordine/AggiungiProdotto',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ prodottoId: prodottoId, quantita: quantita }),
            success: function (data) {
                alert('Prodotto aggiunto all\'ordine.');
            },
            error: function (xhr, status, error) {
                console.error('Errore:', error);
                alert('Si è verificato un problema: ' + error.message);
            }
        });
    }

    // Attacca l'handler click ai pulsanti
    $('.btn-add').click(function () {
        var prodottoId = $(this).data('prodotto-id');
        aggiungiProdotto(prodottoId);
    });
});



$(document).ready(function () {
    // Gestione del click sul bottone "Totale Ordini Evasi"
    $('#btnTotaleOrdiniEvasi').click(function () {
        $.ajax({
            url: '/Ordine/NumeroOrdiniEvasi',  // Assicurati che l'URL corrisponda all'azione del tuo controller
            type: 'GET',
            success: function (data) {
                alert("Numero totale di ordini evasi: " + data);
            },
            error: function () {
                alert("Errore nel recupero del numero di ordini evasi.");
            }
        });
    });



    // Gestione del click sul bottone "Totale Incassato"
    $('#btnTotaleIncassato').click(function () {
        // Richiedi la data dal server
        let dataGiorno = prompt("Inserisci la data (YYYY-MM-DD):");

        if (dataGiorno) {
            $.ajax({
                url: '/Ordine/TotaleIncassatoPerGiornata',  // Assicurati che l'URL corrisponda all'azione del tuo controller
                type: 'GET',
                data: { giorno: dataGiorno },  // Invia la data al server
                success: function (data) {
                    alert("Totale incassato il " + dataGiorno + ": " + data + " €");
                },
                error: function () {
                    alert("Errore nel recupero del totale incassato.");
                }
            });
        }
    });
});
