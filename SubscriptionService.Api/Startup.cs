using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SubscriptionService.Entity;
using SubscriptionService.Package;
using SubscriptionService.Repository;

namespace SubscriptionService.API
{
    public class Startup
    {
        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SubscriptionService.Api", Version = "v1" });
            });

            services.AddRouting(routeOption => routeOption.LowercaseUrls = true);

            services.AddCors(options =>
                options.AddPolicy(
                    "CorsPolicy",
                    b => b.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .Build()));

            //services
            //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            //    {
            //        options.Authority = Configuration.GetValue<string>("AuthorityService");
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateAudience = false,
            //            ClockSkew = TimeSpan.FromSeconds(1)
            //        };
            //    });

            services.AddDbContext<SubscriptionContext>(x => x.UseSqlServer(Configuration.GetConnectionString("SubscriptionDb")));
            services.AddControllers();

            services.AddSingleton<PackageProvider>();
            services.AddScoped<ISubscriptionInfoRepository, SubscriptionInfoRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SubscriptionService.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}