using Dapper;
using MySql.Data.MySqlClient;

namespace DBproject.DAL
{
    public class DbHelper
    {
        public static string ConnString = @"server=localhost;port=3306;userid=root;password=secret;database=cocodo; AllowUserVariables=true";

        public static async Task ExecuteAsync(string sql, object model)
        {
            using (var connection = new MySqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
            
        }

        public static async Task<T?> QueryScalarAsync<T>(string sql, object model)
        {
            using (var connection = new MySqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new MySqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sql, model);
            }
        }
    }
}
