using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Phonebook.Db
{
    internal static class MySqlDbHelper
    {
        // TODO: Move this to config (appsettings / user secrets) as needed.
        // Example: "Server=localhost;Database=phonebook;Uid=root;Pwd=yourpassword;SslMode=None;"
        public static string ConnectionString { get; set; } = "";

        private static MySqlConnection CreateConnection()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException("MySqlDbHelper.ConnectionString is not set.");

            return new MySqlConnection(ConnectionString);
        }

        public static DataTable Select(string sql, Dictionary<string, object>? parameters = null)
        {
            using var conn = CreateConnection();
            using var cmd = new MySqlCommand(sql, conn);

            AddParameters(cmd, parameters);

            using var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public static int Execute(string sql, Dictionary<string, object>? parameters = null)
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new MySqlCommand(sql, conn);
            AddParameters(cmd, parameters);

            return cmd.ExecuteNonQuery();
        }

        public static T? ExecuteScalar<T>(string sql, Dictionary<string, object>? parameters = null)
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new MySqlCommand(sql, conn);
            AddParameters(cmd, parameters);

            object? result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value) return default;

            return (T)Convert.ChangeType(result, typeof(T));
        }

        private static void AddParameters(MySqlCommand cmd, Dictionary<string, object>? parameters)
        {
            if (parameters == null) return;

            foreach (var kvp in parameters)
            {
                // Supports keys like "@id" or "id"
                var name = kvp.Key.StartsWith("@") ? kvp.Key : "@" + kvp.Key;
                cmd.Parameters.AddWithValue(name, kvp.Value ?? DBNull.Value);
            }
        }
    }
}