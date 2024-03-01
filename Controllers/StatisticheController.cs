using back_end_s5_l05.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace back_end_s5_l05.Controllers
{
    public class StatisticheController : Controller
    {
        private readonly ILogger<StatisticheController> _logger;
        private readonly string _connectionString = "Server=MSI\\SQLEXPRESS; Initial Catalog=GestioneContravvenzioni; Integrated Security=true; TrustServerCertificate=True";

        public StatisticheController(ILogger<StatisticheController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<StatisticheVerbale> statistiche = new List<StatisticheVerbale>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = @"SELECT v.Idanagrafica, 
                                          a.Cognome, 
                                          a.Nome, 
                                          COUNT(v.Idverbale) AS TotaleVerbali,
                                          SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati
                                     FROM VERBALE v
                                     JOIN ANAGRAFICA a ON v.Idanagrafica = a.Idanagrafica
                                    GROUP BY v.Idanagrafica, a.Cognome, a.Nome";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StatisticheVerbale statistica = new StatisticheVerbale
                        {
                            IdAnagrafica = reader.GetInt32(0),
                            CognomeAnagrafica = reader.GetString(1),
                            NomeAnagrafica = reader.GetString(2),
                            TotaleVerbali = reader.GetInt32(3),
                            TotalePuntiDecurtati = reader.GetInt32(4)
                        };

                        statistiche.Add(statistica);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle statistiche dei verbali");
            }

            return View(statistiche);
        }
        public IActionResult ViolazioniSuperioriADieciPunti()
        {
            List<StatisticheVerbale> statistiche = new List<StatisticheVerbale>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = @"SELECT v.Idanagrafica, 
                                  a.Cognome, 
                                  a.Nome, 
                                  v.DataViolazione, 
                                  v.DecurtamentoPunti
                             FROM VERBALE v
                             JOIN ANAGRAFICA a ON v.Idanagrafica = a.Idanagrafica
                            WHERE v.DecurtamentoPunti > 10";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StatisticheVerbale statistica = new StatisticheVerbale
                        {
                            IdAnagrafica = reader.GetInt32(0),
                            CognomeAnagrafica = reader.GetString(1),
                            NomeAnagrafica = reader.GetString(2),
                            DataViolazione = reader.GetDateTime(3),
                            TotalePuntiDecurtati = reader.GetInt32(4)
                        };

                        statistiche.Add(statistica);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle statistiche delle violazioni superiori a 10 punti");
            }

            return View(statistiche);
        }
        public IActionResult Totale()
        {
            List<StatisticheVerbale> statistiche = new List<StatisticheVerbale>();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT v.Idanagrafica, a.Nome, a.Cognome, v.DataViolazione, COUNT(*) AS TotaleVerbali, SUM(v.Importo) AS TotaleImporto, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati " +
                                   "FROM VERBALE v " +
                                   "JOIN ANAGRAFICA a ON v.Idanagrafica = a.Idanagrafica " +
                                   "GROUP BY v.Idanagrafica, a.Nome, a.Cognome, v.DataViolazione";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StatisticheVerbale statistica = new StatisticheVerbale
                        {
                            IdAnagrafica = reader.GetInt32(0),
                            NomeAnagrafica = reader.GetString(1),
                            CognomeAnagrafica = reader.GetString(2),
                            DataViolazione = reader.GetDateTime(3),
                            TotaleVerbali = reader.GetInt32(4),
                            Importo = reader.GetDecimal(5), // Aggiunto l'importo
                            TotalePuntiDecurtati = reader.GetInt32(6)
                        };

                        statistiche.Add(statistica);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle statistiche dei verbali");
            }

            return View(statistiche);
        }

    }
}
