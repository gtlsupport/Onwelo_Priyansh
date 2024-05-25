using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VotingApp.DE;

namespace VotingApp
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            // Create the default host builder and configure it for web hosting.
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a default host builder and configures it for web hosting.
        /// </summary>
        /// <param name="args">Arguments passed to the application.</param>
        /// <returns>An IHostBuilder instance for further configuration.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Specify the startup class for the web application.
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services required by the application.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configure Core Services

            services.AddControllers(); // Add services for controllers handling API requests.

            services.AddAutoMapper(typeof(AutoMapperVotingApp)); // Add AutoMapper for object mapping.

            #endregion Configure Core Services

            #region Configure Database

            services.AddDbContext<VotingAppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); // Configure connection to the database using connection string.

            #endregion Configure Database

            #region Configure Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Voting App API", Version = "v1" });
            });

            #endregion Configure Swagger

            #region Configure CORS

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Allow requests from this origin
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            #endregion Configure CORS

            // Call configuration logic from the Business Logic (BL) layer.
            BL.IocConfig.ConfigureServices(ref services);
        }

        /// <summary>
        /// Configures the application pipeline.
        /// </summary>
        /// <param name="app">The IApplicationBuilder instance representing the application pipeline.</param>
        /// <param name="env">The IWebHostEnvironment instance providing information about the web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Configure Environment

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Voting App API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #endregion Configure Environment

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map controllers to handle API requests.
            });
        }
    }
}