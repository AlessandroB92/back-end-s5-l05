using back_end_s5_l05.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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

                    string query = "SELECT Idanagrafica, COUNT(*) AS TotaleVerbali FROM VERBALE GROUP BY Idanagrafica";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StatisticheVerbale statistica = new StatisticheVerbale
                        {
                            IdAnagrafica = reader.GetInt32(0),
                            TotaleVerbali = reader.GetInt32(1)
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
