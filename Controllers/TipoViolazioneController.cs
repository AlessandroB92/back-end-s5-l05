using back_end_s5_l05.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace back_end_s5_l05.Controllers
{
    public class TipoViolazioneController : Controller
    {
        private readonly ILogger<TipoViolazioneController> _logger;
        private readonly string _connectionString = "Server=MSI\\SQLEXPRESS; Initial Catalog=GestioneContravvenzioni; Integrated Security=true; TrustServerCertificate=True";

        public TipoViolazioneController(ILogger<TipoViolazioneController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<TipoViolazione> tipiViolazione = new List<TipoViolazione>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT Idviolazione, Descrizione FROM TIPO_VIOLAZIONE";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TipoViolazione tipoViolazione = new TipoViolazione
                        {
                            Idviolazione = reader.GetInt32(0),
                            Descrizione = reader.GetString(1)
                        };

                        tipiViolazione.Add(tipoViolazione);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei tipi di violazione dal database");
            }

            return View(tipiViolazione);
        }

    }
}
