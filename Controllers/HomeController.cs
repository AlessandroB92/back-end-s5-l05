using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace back_end_s5_l05.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly string _connectionString = "Server=MSI\\SQLEXPRESS; Initial Catalog=GestioneContravvenzioni; Integrated Security=true; TrustServerCertificate=True";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'apertura della connessione al database");
            }

            return View();
        }
    }
}
