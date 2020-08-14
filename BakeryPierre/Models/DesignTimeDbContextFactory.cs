using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BakeryPierre.Models
{
  public class BakeryPierreContextFactory : IDesignTimeDbContextFactory<BakeryPierreContext>
  {

    BakeryPierreContext IDesignTimeDbContextFactory<BakeryPierreContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<BakeryPierreContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new BakeryPierreContext(builder.Options);
    }
  }
}