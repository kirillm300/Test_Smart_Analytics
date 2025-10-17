using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Test_Smart_Analytics
{
    public class DatabaseManager
    {
        private readonly string _connectionString;
        private NpgsqlConnection? _connection;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Connect()
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }

        public List<string> GetUserTables()
        {
            var tables = new List<string>();

            try
            {
                using var cmd = new NpgsqlCommand(
                    "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' ORDER BY table_name;",
                    _connection
                );

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    tables.Add(reader.GetString(0));
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении списка таблиц: {ex.Message}");
            }

            return tables;
        }

        public void Disconnect()
        {
            _connection?.Close();
        }
    }
}