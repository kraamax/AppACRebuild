using System.Data.Common;
using System.Linq;
using AppAC.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppAC.Infrastructure.WebApi.Test.Base
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public static DbConnection CreateConnection()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }
     
        private readonly  DbConnection connection = CreateConnection();
        private readonly string _connectionString=@"Data Source=C:\sqlite\AppACDataBaseTestWeb.db";
        public AppACContext CreateContext() 
        {
            var builder = new DbContextOptionsBuilder<AppACContext>().UseSqlite(connection);
            return new AppACContext(builder.Options);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                #region Reemplazar la inyección del Contexto de Datos de EF Core
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppACContext>));

                services.Remove(descriptor);
                services.AddDbContext<AppACContext>(options =>
                {
                    options.UseSqlite(connection);
                });
                #endregion

                #region Eliminar y Crear nueva base de datos. 
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppACContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    //invocar clase que inicilice los datos semillas. 
                }
                
                #endregion 
            });
        }
    }
}