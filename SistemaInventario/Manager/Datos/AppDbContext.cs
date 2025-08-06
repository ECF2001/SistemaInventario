using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDbContext
{
    public class AppDbContext
    {
        private readonly string? _connectionString;
        private IDbTransaction _transaction { get; set; }

        public IDbTransaction Transaction => _transaction;

        public AppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }

        public IDbConnection CreateConnection()
            => CreateConnection(false, true);

        public IDbConnection CreateConnection(bool useTransaction, bool open = false)
        {
            var connection = new SqlConnection(_connectionString);
            if (useTransaction)
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
            }
            else
            {
                if (open)
                {
                    connection.Open();
                }
            }

            return connection;
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        public static void EnsureDatabase(IConfiguration configuration, string name)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");
            using var connection = new SqlConnection(connectionString);
            var records = connection.Query($"SELECT name FROM sys.databases WHERE name = '{name}';");
            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE '{name}'");
            }
        }
    }
}
