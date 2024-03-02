using Microsoft.AspNetCore.Mvc;
using POLIZIA_MUNICIPALE.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace POLIZIA_MUNICIPALE.Controllers
{
    public class Anagrafe : Controller
    {
        private string connString = "Server=PCGIANLUIGI\\SQLEXPRESS; Initial Catalog=compito settimanale 1; Integrated Security=true; TrustServerCertificate=True";

        [HttpGet]
        public IActionResult Index()
        {
            // Aprire la connessione
            using (var conn = new SqlConnection(connString))
            {
                List<Anagrafia> anagrafie = new List<Anagrafia>();

                try
                {
                    conn.Open();
                    // Creare il comando
                    var command = new SqlCommand("SELECT TOP (1000) [IDAnagrafica], [Cognome], [Nome], [Indirizzo], [Città], [Cap], [Cod_Fisc] FROM [compito settimanale 1].[dbo].[ANAGRAFIA]", conn);

                    // Eseguire il comando
                    var reader = command.ExecuteReader();

                    // Usare i dati
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var anagrafia = new Anagrafia()
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString(),
                                Città = reader["Città"].ToString(),
                                Cap = reader["Cap"].ToString(),
                                Cod_Fisc = reader["Cod_Fisc"].ToString()
                            };
                            anagrafie.Add(anagrafia);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log dell'errore o gestione dell'errore appropriata
                    return View("Error");
                }

                return View(anagrafie);
            }
        }
        [HttpPost]
        public IActionResult Add(Anagrafia anagrafia)
        {
            var error = true;
            var conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                using (var command = new SqlCommand(@"
            INSERT INTO ANAGRAFIA
            (Cognome, Nome, Indirizzo, Città, Cap, Cod_Fisc) VALUES
            (@cognome, @nome, @indirizzo, @citta, @cap, @codFisc)", conn))
                {
                    command.Parameters.AddWithValue("@cognome", anagrafia.Cognome);
                    command.Parameters.AddWithValue("@nome", anagrafia.Nome);
                    command.Parameters.AddWithValue("@indirizzo", anagrafia.Indirizzo);
                    command.Parameters.AddWithValue("@citta", anagrafia.Città);
                    command.Parameters.AddWithValue("@cap", anagrafia.Cap);
                    command.Parameters.AddWithValue("@codFisc", anagrafia.Cod_Fisc);

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

            return View(anagrafia); // Torna alla vista con il modello per mostrare eventuali messaggi di errore.
        }
        public IActionResult Add()
        {
            return View();
        }

    }
}