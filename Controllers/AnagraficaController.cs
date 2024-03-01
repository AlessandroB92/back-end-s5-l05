using back_end_s5_l05.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace back_end_s5_l05.Controllers
{
    public class AnagraficaController : Controller
    {
        private readonly ILogger<AnagraficaController> _logger;
        private readonly string _connectionString = "Server=MSI\\SQLEXPRESS; Initial Catalog=GestioneContravvenzioni; Integrated Security=true; TrustServerCertificate=True";

        public AnagraficaController(ILogger<AnagraficaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Anagrafica> anagrafiche = new List<Anagrafica>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT Idanagrafica, Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc FROM ANAGRAFICA";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Anagrafica anagrafica = new Anagrafica
                        {
                            Idanagrafica = reader.GetInt32(0),
                            Cognome = reader.GetString(1),
                            Nome = reader.GetString(2),
                            Indirizzo = reader.GetString(3),
                            Città = reader.GetString(4),
                            CAP = reader.GetString(5),
                            Cod_Fisc = reader.GetString(6)
                        };

                        anagrafiche.Add(anagrafica);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle anagrafiche dal database");
            }

            return View(anagrafiche);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Anagrafica anagrafica)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO ANAGRAFICA (Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) VALUES (@Cognome, @Nome, @Indirizzo, @Città, @CAP, @Cod_Fisc)";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                    command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                    command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                    command.Parameters.AddWithValue("@Città", anagrafica.Città);
                    command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                    command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);

                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'inserimento dell'anagrafica nel database");
                return View(anagrafica);
            }
        }
    }
}
