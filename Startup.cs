using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using InterventionAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Primitives;

namespace InterventionAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<Learn_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("constring")));


            var _jwtsetting = Configuration.GetSection("JWTSetting");
            services.Configure<JWTSetting>(_jwtsetting);

            var authkey = Configuration.GetValue<string>("JWTSetting:securitykey");

            services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {

                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authkey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew=TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("api", new OpenApiInfo()
                {
                    Description = "Baniola API",
                    Title = "Baniola",
                    Version = "1"
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(context => {
                    if (context.Request.Headers.TryGetValue("Origin", out StringValues origin))
                    {
                        context.Response.Headers.Add("Access-Control-Allow-Origin", origin.ToString());
                    }

                    return Task.CompletedTask;
                });
            });

            app.UseSwaggerUI(options => options.SwaggerEndpoint("api/swagger.json", "Baniola"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
