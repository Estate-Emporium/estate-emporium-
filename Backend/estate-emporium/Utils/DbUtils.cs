using estate_emporium.Models;
using Microsoft.EntityFrameworkCore;

namespace estate_emporium.Utils
{
    public static class DbUtils
    {
        public static string getConnectionString()
        {
            string dbUsername = Environment.GetEnvironmentVariable("DB_USERNAME");
            string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD"); // Ensure this is securely handled
            string dbUrl = Environment.GetEnvironmentVariable("DB_URL");

            return $"Data Source={dbUrl};Initial Catalog=EstateEmporium;User ID={dbUsername};Password={dbPassword};TrustServerCertificate=True;";
        }
    }
}
