using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace InspecWeb.Data
{
    public static class MigrationManager
    {
        /// <summary>
        /// เรียกใช้การ migrate ข้อมูล
        /// </summary>
        /// <param name="host">โฮสต์</param>
        /// <returns>โฮสต์</returns>
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    Console.WriteLine(ex.Message);

                    throw;
                }
            }

            return host;
        }
    }
}
