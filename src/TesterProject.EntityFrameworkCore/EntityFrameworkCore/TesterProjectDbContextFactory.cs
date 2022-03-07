using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TesterProject.Configuration;
using TesterProject.Web;

namespace TesterProject.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TesterProjectDbContextFactory : IDesignTimeDbContextFactory<TesterProjectDbContext>
    {
        public TesterProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TesterProjectDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            TesterProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TesterProjectConsts.ConnectionStringName));

            return new TesterProjectDbContext(builder.Options);
        }
    }
}
