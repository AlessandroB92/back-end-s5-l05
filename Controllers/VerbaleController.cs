using back_end_s5_l05.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace back_end_s5_l05.Controllers
{
    public class VerbaleController : Controller
    {
        private readonly ILogger<VerbaleController> _logger;
        private readonly string _connectionString = "Server=MSI\\SQLEXPRESS; Initial Catalog=GestioneContravvenzioni; Integrated Security=true; TrustServerCertificate=True";

        public VerbaleController(ILogger<VerbaleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Create()
        {
            List<Anagrafica> anagrafiche = new List<Anagrafica>();
            List<TipoViolazione> tipiViolazione = new List<TipoViolazione>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string queryAnagrafiche = "SELECT Idanagrafica, Cognome, Nome FROM ANAGRAFICA";
                    SqlCommand commandAnagrafiche = new SqlCommand(queryAnagrafiche, conn);
                    SqlDataReader readerAnagrafiche = commandAnagrafiche.ExecuteReader();

                    while (readerAnagrafiche.Read())
                    {
                        Anagrafica anagrafica = new Anagrafica
                        {
                            Idanagrafica = readerAnagrafiche.GetInt32(0),
                            Cognome = readerAnagrafiche.GetString(1),
                            Nome = readerAnagrafiche.GetString(2)
                        };

                        anagrafiche.Add(anagrafica);
                    }

                    string queryTipiViolazione = "SELECT Idviolazione, descrizione FROM TIPO_VIOLAZIONE";
                    SqlCommand commandTipiViolazione = new SqlCommand(queryTipiViolazione, conn);
                    SqlDataReader readerTipiViolazione = commandTipiViolazione.ExecuteReader();

                    while (readerTipiViolazione.Read())
                    {
                        TipoViolazione tipoViolazione = new TipoViolazione
                        {
                            Idviolazione = readerTipiViolazione.GetInt32(0),
                            Descrizione = readerTipiViolazione.GetString(1)
                        };

                        tipiViolazione.Add(tipoViolazione);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle anagrafiche e dei tipi di violazione dal database");
            }

            ViewBag.Anagrafiche = anagrafiche;
            ViewBag.TipiViolazione = tipiViolazione;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Verbale verbale)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'inserimento del verbale nel database");
                return View(verbale);
            }
        }
    }
}
