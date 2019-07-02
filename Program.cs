using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ContosoUniversity
{
	public class Program
	{
		public static async System.Threading.Tasks.Task Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var context = services.GetRequiredService<SchoolContext>();
					await DbInitializer.InitializeAsync(context);
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occured creating the DB");
				}
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
				WebHost.CreateDefaultBuilder(args)
						.UseStartup<Startup>();
	}
}
