using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using POLIZIA_MUNICIPALE.Models;

namespace POLIZIA_MUNICIPALE.Controllers
{
    public class Ricerche : Controller
    {



        public IActionResult Index()
        {
            return View();
        }
        private string connString = "Server=PCGIANLUIGI\\SQLEXPRESS; Initial Catalog=compito settimanale 1; Integrated Security=true; TrustServerCertificate=True";

        [HttpGet]
        public IActionResult MetodoA()
        {
            // Aprire la connessione
            using (var conn = new SqlConnection(connString))
            {
                List<VerbaliTrascritti> trascrizioni = new List<VerbaliTrascritti>();

                try
                {
                    conn.Open();
                    // Creare il comando
                    var command = new SqlCommand(@"
                        SELECT
                            A.[IDAnagrafica],
                            A.[Cognome],
                            A.[Nome],
                            COUNT(V.[ID VERBALE]) AS TotaleVerbaliTrascritti
                        FROM
                            [compito settimanale 1].[dbo].[ANAGRAFIA] A
                        JOIN
                            [compito settimanale 1].[dbo].[VERBALE] V ON A.[IDAnagrafica] = V.[IDAnagrafica]
                        GROUP BY
                            A.[IDAnagrafica],
                            A.[Cognome],
                            A.[Nome]", conn);

                    // Eseguire il comando
                    var reader = command.ExecuteReader();

                    // Usare i dati
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var VerbaliTrascritto = new VerbaliTrascritti()
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                TotaleVerbaliTrascritti = (int)reader["TotaleVerbaliTrascritti"]
                            };
                            trascrizioni.Add(VerbaliTrascritto);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log dell'errore o gestione dell'errore appropriata
                    return View("Error");
                }

                return View(trascrizioni);
            }
        }
        public IActionResult MetodoB()
        {
            // Aprire la connessione
            using (var conn = new SqlConnection(connString))
            {
                List<VerbaliTrascritti> trascrizioni = new List<VerbaliTrascritti>();

                try
                {
                    conn.Open();
                    // Creare il comando
                    var command = new SqlCommand(@"
                SELECT
                    A.[IDAnagrafica],
                    A.[Cognome],
                    A.[Nome],
                    SUM(V.[DecurtamentoPunti]) AS TotalePuntiDecurtati
                FROM
                    [compito settimanale 1].[dbo].[ANAGRAFIA] A
                JOIN
                    [compito settimanale 1].[dbo].[VERBALE] V ON A.[IDAnagrafica] = V.[IDAnagrafica]
                GROUP BY
                    A.[IDAnagrafica],
                    A.[Cognome],
                    A.[Nome]
            ", conn);

                    // Eseguire il comando
                    var reader = command.ExecuteReader();

                    // Usare i dati
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var VerbaliTrascritto = new VerbaliTrascritti()
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                TotaleVerbaliTrascritti = (int)reader["TotalePuntiDecurtati"]  // Corretto il nome della colonna
                            };
                            trascrizioni.Add(VerbaliTrascritto);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log dell'errore o gestione dell'errore appropriata
                    return View("Error");
                }

                return View(trascrizioni);
            }
        }
        public IActionResult MetodoC()
        {
            // Aprire la connessione
            using (var conn = new SqlConnection(connString))
            {
                List<VerbaleDettaglio> trascrizioni = new List<VerbaleDettaglio>();

                try
                {
                    conn.Open();
                    // Creare il comando
                    var command = new SqlCommand(@"
                SELECT
                    A.[IDAnagrafica],
                    A.[Cognome],
                    A.[Nome],
                    V.[Importo],
                    V.[DataViolazione],
                    V.[DecurtamentoPunti]
                FROM
                    [compito settimanale 1].[dbo].[ANAGRAFIA] A
                JOIN
                    [compito settimanale 1].[dbo].[VERBALE] V ON A.[IDAnagrafica] = V.[IDAnagrafica]
                WHERE
                    V.[DecurtamentoPunti] > 10
            ", conn);

                    // Eseguire il comando
                    var reader = command.ExecuteReader();

                    // Usare i dati
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var VerbaleDettaglio = new VerbaleDettaglio()
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Importo = (decimal)reader["Importo"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"]
                            };
                            trascrizioni.Add(VerbaleDettaglio);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log dell'errore o gestione dell'errore appropriata
                    return View("Error");
                }

                return View(trascrizioni);
            }
        }
        public IActionResult MetodoD()
        {
            // Aprire la connessione
            using (var conn = new SqlConnection(connString))
            {
                List<VerbaleDettaglio> trascrizioni = new List<VerbaleDettaglio>();

                try
                {
                    conn.Open();
                    // Creare il comando
                    var command = new SqlCommand(@"
                SELECT
                    A.[IDAnagrafica],
                    A.[Cognome],
                    A.[Nome],
                    V.[Importo],
                    V.[DataViolazione],
                    V.[DecurtamentoPunti]
                FROM
                    [compito settimanale 1].[dbo].[ANAGRAFIA] A
                JOIN
                    [compito settimanale 1].[dbo].[VERBALE] V ON A.[IDAnagrafica] = V.[IDAnagrafica]
                WHERE
                    V.[Importo] > 400
            ", conn);

                    // Eseguire il comando
                    var reader = command.ExecuteReader();

                    // Usare i dati
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var VerbaleDettaglio = new VerbaleDettaglio()
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Importo = (decimal)reader["Importo"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"]
                            };
                            trascrizioni.Add(VerbaleDettaglio);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log dell'errore o gestione dell'errore appropriata
                    return View("Error");
                }

                return View(trascrizioni);
            }
        }
    }
}

