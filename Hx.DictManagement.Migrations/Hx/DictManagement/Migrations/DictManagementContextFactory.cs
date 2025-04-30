using Hx.DictManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Hx.DictManagement.Migrations
{
    public class DictManagementContextFactory : IDesignTimeDbContextFactory<DictManagementMigrationDbContext>
    {
        public DictManagementMigrationDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var builder =
                new DbContextOptionsBuilder<DictManagementMigrationDbContext>()
                .UseNpgsql(
                configuration.GetConnectionString(DictManagementDbProperties.ConnectionStringName));
            return new DictManagementMigrationDbContext(builder.Options);
        }
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
