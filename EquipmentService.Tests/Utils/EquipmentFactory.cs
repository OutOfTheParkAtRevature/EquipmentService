using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Repository;

namespace EquipmentService.Tests.Utils {
    public class EquipmentFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.UseContentRoot(".");
			base.ConfigureWebHost(builder);
			builder.ConfigureServices(services => {
				var descriptor = services.SingleOrDefault(d =>
					d.ServiceType == typeof(DbContextOptions<EquipmentContext>)
				);
				services.Remove(descriptor);
				services.AddDbContext<EquipmentContext>(options => {
					options.UseInMemoryDatabase(databaseName: "intg-db");
				});
				var sp = services.BuildServiceProvider();
				using (var scope = sp.CreateScope()) {
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<EquipmentContext>();
					var logger = scopedServices.GetRequiredService<ILogger<EquipmentFactory<TStartup>>>();
					db.Database.EnsureDeleted();
					db.Database.EnsureCreated();
				}
			});
        }
    }
}