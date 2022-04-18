using System;
using System.Data.SqlClient;
using Xunit;

namespace KlirTechChallenge.Tests
{
    public class TestConnection
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\Klir\LeninAguiar_Klir_Project\Klir.TechChallenge.InfraStructure\Context\KlirEcommerceDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30;";

        [Fact]
        public void TestConnectionString()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Close();
            }
        }

    }
}
