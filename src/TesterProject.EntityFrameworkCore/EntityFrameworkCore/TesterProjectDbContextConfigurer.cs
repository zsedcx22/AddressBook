using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TesterProject.EntityFrameworkCore
{
    public static class TesterProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TesterProjectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TesterProjectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
