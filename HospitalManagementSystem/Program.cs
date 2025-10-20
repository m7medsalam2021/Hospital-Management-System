using Hospital.Core.Entities.Identity;
using Hospital.Repository.Data;
using Hospital.Repository.Identity;
using HospitalManagementSystem.Extenstion;
using HospitalManagementSystem.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace HospitalManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services To Container
            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<HospitalContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddDbContext<ApplicationIdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("identityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            #endregion

            var app = builder.Build();

            #region Migrate and Update Database
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
        
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<HospitalContext>();
                //now apply migrations
                await dbContext.Database.MigrateAsync();
                // add data 
                await HospitalContextSeeding.SeedAsync(dbContext);

                var identityContext = services.GetRequiredService<ApplicationIdentityContext>();
                await identityContext.Database.MigrateAsync();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await ApplicationIdentityDbContextSeed.SeedUsersAsync(userManager);
            }
            catch(Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }

            #endregion

            #region Configure Kestrel Middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerServices();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            #endregion


            app.Run();
        }
    }
}
