using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace back_end_s5_l05.Models
{
    public class DbContext
    {
        private static string connectionString = "Server=MSI\\SQLEXPRESS; Initial Catalog=GestioneContravvenzioni; Integrated Security=true; TrustServerCertificate=True";
        public static SqlConnection conn = new SqlConnection(connectionString);
    }
   
}
