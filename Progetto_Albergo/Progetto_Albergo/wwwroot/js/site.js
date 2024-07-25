let basePath = '/API/ApiController';

$(() => {
    // Gestisce il click sul pulsante di ricerca delle prenotazioni
    $("#searchButton").on('click', () => {
        getPrenotazioniByCf();
    });

    // Gestisce il click sul pulsante per ottenere il totale delle prenotazioni per pensione completa
    $("#getTotalPensioneButton").on('click', () => {
        getTotalePensioneCompleta();
    });
});

// Funzione per ottenere le prenotazioni tramite il codice fiscale
function getPrenotazioniByCf() {
    let cf = $("#cf").val();
    $.ajax({
        url: `${basePath}/ByCf?cf=${cf}`,
        method: 'GET',
        success: (data) => {
            let ul = $("#prenotazioniContainer ul");
            ul.empty();
            if (data.length === 0) {
                ul.append('<li>Nessuna prenotazione trovata per il codice fiscale fornito.</li>');
            } else {
                $(data).each((_, prenotazione) => {
                    let textSpan = $('<span>').text(`Prenotazione ID: ${prenotazione.idPrenotazione}, Data: ${prenotazione.data}, Numero: ${prenotazione.numero}, Anno: ${prenotazione.anno}, Dal: ${prenotazione.dal}, Al: ${prenotazione.al}, Caparra: ${prenotazione.caparra}, Tariffa: ${prenotazione.tariffa}, Descrizione: ${prenotazione.descrizione}, IdCliente: ${prenotazione.fkCliente}, IdCamera: ${prenotazione.fkCamera}`);
                    let li = $('<li>');
                    textSpan.appendTo(li);
                    li.appendTo(ul);
                });
            }
        },
        error: (xhr, status, error) => {
            let ul = $("#prenotazioniContainer ul");
            ul.empty();
            ul.append('<li>Errore durante la ricerca delle prenotazioni: ' + error + '</li>');
        }
    });
}

// Funzione per ottenere il totale delle prenotazioni per pensione completa
function getTotalePensioneCompleta() {
    $.ajax({
        url: `${basePath}/TotalePensioneCompleta`,
        method: 'GET',
        success: (data) => {
            // Aggiorna il testo nella view con il totale delle prenotazioni
            $("#totalPensioneCompleta").text(`Numero totale di prenotazioni per soggiorni di tipo "Pensione Completa": ${data}`);
        },
        error: (xhr, status, error) => {
            // Gestisce gli errori
            $("#totalPensioneCompleta").text('Errore durante il recupero del totale delle prenotazioni: ' + error);
        }
    });
}
