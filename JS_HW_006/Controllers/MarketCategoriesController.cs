using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ASP_007.Controllers
{
    /*  ASP_009
     *  AspCore
     *  Связывается с бд и обладает отдельными контроллерами для работы с товарами и их категориями*/

    [Route("api/[controller]")]
    [ApiController]
    public class MarketCategoriesController : ControllerBase
    {
        string connStr = new string("Server=tcp:my-server93-2.database.windows.net,1433;Initial Catalog=ASP_009;Persist Security Info=False;User ID=admin93;Password=PASSworld93;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> transactions = new List<string>();
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                string str = $"SELECT * FROM [categories]";
                using (SqlCommand command = new SqlCommand(str, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        transactions.Add(reader.GetValue(1).ToString());
                    }
                }
                return transactions;
            };
        }

        [HttpGet("{id}")]
        public IEnumerable<string> Get(int id)
        {
            List<string> transactions = new List<string>();
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                string str = $"SELECT * FROM [categories] WHERE id = {id}";
                using (SqlCommand command = new SqlCommand(str, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        transactions.Add(reader.GetValue(1).ToString());
                    }
                }
                return transactions;
            };
        }

        [HttpPost("{name}, {description}")]
        public StatusCodeResult Post(string name, string description)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();


                string str = $"INSERT INTO [categories] VALUES (\'{name}\', \'{description}\')";
                using (SqlCommand command = new SqlCommand(str, connection))
                {
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        return StatusCode(405);
                    }
                }
                return StatusCode(201);
            };
        }

        [HttpPut("{id}, {name}, {description}")]
        public StatusCodeResult Put(int id, string name, string description)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();


                string str = $"UPDATE [categories] SET [name] = {name}, description = {description} WHERE id = {id}";
                using (SqlCommand command = new SqlCommand(str, connection))
                {
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        return StatusCode(405);
                    }
                }
                return StatusCode(201);
            };
        }

        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                string str = $"DELETE FROM [categories] WHERE id = {id}";
                using (SqlCommand command = new SqlCommand(str, connection))
                {
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        return StatusCode(405);
                    }
                }
                return StatusCode(202);
            };
        }
    }
}
