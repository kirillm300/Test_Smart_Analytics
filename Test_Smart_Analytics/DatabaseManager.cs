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

        public void CreateTable(string tableName, List<ColumnDefinition> columns)
        {
            if (columns.Count == 0)
                throw new Exception("Не указаны поля таблицы.");

            List<string> columnDefs = new();
            List<string> pkColumns = new();

            foreach (var col in columns)
            {
                string notNullPart = col.NotNull ? " NOT NULL" : "";
                string colDef = $"\"{col.Name}\" {col.Type}{notNullPart}";
                columnDefs.Add(colDef);
                if (col.IsPrimaryKey)
                    pkColumns.Add($"\"{col.Name}\"");
            }

            if (pkColumns.Count > 0)
                columnDefs.Add($"PRIMARY KEY ({string.Join(", ", pkColumns)})");

            string sql = $"CREATE TABLE public.\"{tableName}\" (\n{string.Join(",\n", columnDefs)}\n);";

            using var cmd = new NpgsqlCommand(sql, _connection);
            cmd.ExecuteNonQuery();
        }

        public List<ColumnDefinition> GetTableColumns(string tableName)
        {
            var result = new List<ColumnDefinition>();

            string sql = $@"
        SELECT
            column_name,
            data_type,
            is_nullable = 'NO' AS not_null,
            column_name IN (
                SELECT a.attname
                FROM pg_index i
                JOIN pg_attribute a ON a.attrelid = i.indrelid AND a.attnum = ANY(i.indkey)
                WHERE i.indrelid = 'public.""{tableName}""'::regclass AND i.indisprimary
            ) AS is_primary
        FROM information_schema.columns
        WHERE table_name = '{tableName}';";

            using var cmd = new NpgsqlCommand(sql, _connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new ColumnDefinition
                {
                    Name = reader.GetString(0),
                    Type = reader.GetString(1),
                    NotNull = reader.GetBoolean(2),
                    IsPrimaryKey = reader.GetBoolean(3)
                });
            }

            return result;
        }

        public void RecreateTable(string oldTableName, string newTableName, List<ColumnDefinition> newColumns)
        {
            string tempName = oldTableName + "_old_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            using (var cmd = new NpgsqlCommand($"ALTER TABLE public.\"{oldTableName}\" RENAME TO \"{tempName}\";", _connection))
                cmd.ExecuteNonQuery();

            CreateTable(newTableName, newColumns);

            var oldCols = GetTableColumns(tempName);
            var matchingCols = newColumns.FindAll(c => oldCols.Exists(o => o.Name == c.Name));

            if (matchingCols.Count > 0)
            {
                string cols = string.Join(", ", matchingCols.ConvertAll(c => $"\"{c.Name}\""));
                string sqlCopy = $"INSERT INTO public.\"{newTableName}\" ({cols}) SELECT {cols} FROM public.\"{tempName}\";";
                using var cmdCopy = new NpgsqlCommand(sqlCopy, _connection);
                cmdCopy.ExecuteNonQuery();
            }

            using (var cmdDrop = new NpgsqlCommand($"DROP TABLE public.\"{tempName}\";", _connection))
                cmdDrop.ExecuteNonQuery();
        }
        
        public void DropTable(string tableName)
        {
            string sql = $"DROP TABLE IF EXISTS public.\"{tableName}\" CASCADE;";

            using var cmd = new NpgsqlCommand(sql, _connection);
            cmd.ExecuteNonQuery();
        }

        public class ColumnDefinition
        {
            public string Name { get; set; } = "";
            public string Type { get; set; } = "";
            public bool IsPrimaryKey { get; set; }
            public bool NotNull { get; set; }
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

        public DataTable GetTableStructure(string tableName)
        {
            var table = new DataTable();
            try
            {
                string query = $@"
            SELECT 
                column_name AS ""Имя столбца"",
                data_type AS ""Тип данных"",
                is_nullable AS ""Может быть NULL"",
                column_default AS ""Значение по умолчанию""
            FROM information_schema.columns
            WHERE table_schema = 'public' AND table_name = @tableName
            ORDER BY ordinal_position;";

                using var cmd = new NpgsqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@tableName", tableName);

                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении структуры таблицы {tableName}: {ex.Message}");
            }

            return table;
        }


        public void Disconnect()
        {
            _connection?.Close();
        }
    }
}