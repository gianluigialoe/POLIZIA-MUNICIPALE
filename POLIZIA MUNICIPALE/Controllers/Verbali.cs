using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using POLIZIA_MUNICIPALE.Controllers;
using POLIZIA_MUNICIPALE.Models;

namespace POLIZIA_MUNICIPALE.Controllers
{
    public class Verbali : Controller
    {
        private string connString = "Server=PCGIANLUIGI\\SQLEXPRESS; Initial Catalog=compito settimanale 1; Integrated Security=true; TrustServerCertificate=True";

        [HttpGet]
        public IActionResult Index()
        {
            // Aprire la connessione
            using (var conn = new SqlConnection(connString))
            {
                List<Verbale> verbali = new List<Verbale>();

                try
                {
                    conn.Open();
                    // Creare il comando
                    var command = new SqlCommand(@"
                        SELECT TOP (1000) [ID VERBALE], [DataViolazione], [IndirizzoViolazione], [Nominativo_Agente], [DataTrascrizioneVerbale], [Importo], [DecurtamentoPunti], [IDAnagrafica], [ID Violazione]
                        FROM [compito settimanale 1].[dbo].[VERBALE]", conn);

                    // Eseguire il comando
                    var reader = command.ExecuteReader();

                    // Usare i dati
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var verbale = new Verbale()
                            {
                                IDVERBALE = (int)reader["ID VERBALE"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                                Nominativo_Agente = reader["Nominativo_Agente"].ToString(),
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                Importo = (decimal)reader["Importo"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                IDViolazione = (int)reader["ID Violazione"]
                            };
                            verbali.Add(verbale);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log dell'errore o gestione dell'errore appropriata
                    return View("Error");
                }

                return View(verbali);
            }
        }
        [HttpPost]
        public IActionResult Add(Verbale verbale)
        {
            var error = true;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                using (var command = new SqlCommand(@"
                INSERT INTO VERBALE
                (/*[ID VERBALE]*/,DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IDAnagrafica, [ID Violazione]) VALUES
                (/*@IdVerbale*/, @dataViolazione, @indirizzoViolazione, @nominativoAgente, @dataTrascrizioneVerbale, @importo, @decurtamentoPunti, @IDAnagrafica, @IDViolazione)", conn))
                {
                    //command.Parameters.AddWithValue("@IdVerbale", 432);
                   command.Parameters.AddWithValue("@dataViolazione", verbale.DataViolazione);
                    command.Parameters.AddWithValue("@indirizzoViolazione", verbale.IndirizzoViolazione);
                    command.Parameters.AddWithValue("@nominativoAgente", verbale.Nominativo_Agente);
                    command.Parameters.AddWithValue("@dataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                    command.Parameters.AddWithValue("@importo", verbale.Importo);
                    command.Parameters.AddWithValue("@decurtamentoPunti", verbale.DecurtamentoPunti);
                    command.Parameters.AddWithValue("@IDAnagrafica", verbale.IDAnagrafica);
                    command.Parameters.AddWithValue("@IDViolazione", verbale.IDViolazione);

                    var nRows = command.ExecuteNonQuery();
                    if (nRows > 0)
                    {
                        TempData["MessageSuccess"] = "L'operazione è stata completata con successo.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Nessuna riga è stata inserita nel database.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log dell'errore o gestione dell'errore appropriata
                ModelState.AddModelError(string.Empty, "Si è verificato un errore durante l'inserimento nel database.");
            }
            finally
            {
                conn.Close();
            }

            return View(); // Torna alla vista con il modello per mostrare eventuali messaggi di errore.
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult MetodoF()
        {
            // Aprire la connessione
            using (var conn = new SqlConnection(connString))
            {
                List<ViolazioneDettaglio> violazioni = new List<ViolazioneDettaglio>();

                try
                {
                    conn.Open();
                    // Creare il comando
                    var command = new SqlCommand(@"
                SELECT TOP (1000)
                    [ID VIOLAZIONE],
                    [DESCRIZIONE]
                FROM
                    [compito settimanale 1].[dbo].[VIOLAZIONI]
            ", conn);

                    // Eseguire il comando
                    var reader = command.ExecuteReader();

                    // Usare i dati
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var violazione = new ViolazioneDettaglio()
                            {
                                IDViolazione = (int)reader["ID VIOLAZIONE"],
                                Descrizione = reader["DESCRIZIONE"].ToString()
                            };
                            violazioni.Add(violazione);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log dell'errore o gestione dell'errore appropriata
                    return View("Error");
                }

                return View(violazioni);
            }
        }

    }
}



