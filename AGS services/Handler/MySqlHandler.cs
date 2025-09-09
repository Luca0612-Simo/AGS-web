using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;

namespace AGS_services.Handler
{
    public class MySqlHandler
    {
        public static string ConnectionString = string.Empty;

        public static string GetJson(string request)
        {
            return JsonConvert.SerializeObject(GetDt(request), Formatting.Indented);
        }

        public static DataTable GetDt(string query)
        {
            DataTable dt = new DataTable();

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                cnn.Open();
                using (var cmd = new MySqlCommand(query, cnn))
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        public static bool Exec(string query)
        {
            bool response = false;

            using (var conn = new MySqlConnection(ConnectionString))
            using (var cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    response = true;
                }
                catch (Exception)
                {
                    response = false;
                }
            }
            return response;
        }

        public static string GetScalar(string request)
        {
            string scalarResult = string.Empty;

            using (var cnn = new MySqlConnection(ConnectionString))
            {
                cnn.Open();
                using (var cmd = new MySqlCommand(request, cnn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        scalarResult = result.ToString();
                    }
                }
            }

            return scalarResult;
        }
    }
}
