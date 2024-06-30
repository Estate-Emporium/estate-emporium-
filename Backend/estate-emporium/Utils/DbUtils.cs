using estate_emporium.Models.db;
using Microsoft.Data.SqlClient;
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

        public static void initDB(WebApplicationBuilder? builder)
        {
            var connectionString = DbUtils.getConnectionString();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Test the connection
                }
                // If the connection is successful, use SQL Server
                Console.WriteLine("Connected sucessfully to db");
                builder.Services.AddDbContext<EstateDbContext>(options => options.UseSqlServer(connectionString));
            }
            catch (Exception ex)
            {
                // Log the error
                Console.Error.WriteLine($"Error connecting to the database: {ex.Message}");
                Console.WriteLine("Falling back to in memory db");
                // Use in-memory database as a fallback
                builder.Services.AddDbContext<EstateDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
            }

        }
    }
}
