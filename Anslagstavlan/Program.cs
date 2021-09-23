using Anslagstavlan.Domain.Database;
using Anslagstavlan.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Anslagstavlan
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var host = CreateHostBuilder(args).Build();

			try
			{
				using (var scope = host.Services.CreateScope())
				{
					var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
					var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ChatUserModel>>();
					context.Database.Migrate();
					if (!context.Users.Any())
					{
						var adminUser = new ChatUserModel()
						{
							UserName = "admin"
						};
						var result = userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			host.Run();
		}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
