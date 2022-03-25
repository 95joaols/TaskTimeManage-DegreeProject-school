using Microsoft.EntityFrameworkCore;

using Npgsql;

using System;
using System.Data.Common;
using System.Diagnostics;

using TaskTimeManage.Domain.Context;

namespace TaskTimeManage.Core
{
    public class TTMContextPostgresqlFixture : IDisposable
    {
        public TTMDbContext Context
        {
            get; set;
        }

        readonly DbConnection connection;

        public TTMContextPostgresqlFixture()
        {
            connection = new NpgsqlConnection("DataSource=:memory:");

            //connection = new SqliteConnection("DataSource=Testin.db");
            connection.Open();

            // Instruct EF to use a sqlite in-memory (FAKE!!!) database instance.
            DbContextOptionsBuilder<TTMDbContext> builder = new DbContextOptionsBuilder<TTMDbContext>().UseNpgsql(connection);

            Context = new TTMDbContext(builder.Options);

            bool result = Context.Database.EnsureCreated();

            Debug.Assert(result);
        }

        public void Dispose()
        {
            connection.Close();
            Context.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
