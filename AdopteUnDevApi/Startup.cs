using AdopteUnDevApi.Tools;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Entities.Views;
using Data_Access_Layer.Interface;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopteUnDevApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdopteUnDevApi", Version = "v1" });
            });

            services.AddScoped<IRepository<User>, RepositoryUser>();
            services.AddScoped<IRepository<Contract>, RepositoryContract>();
            services.AddScoped<IRepository<Skill>, RepositorySkill>();
            services.AddScoped<IRepository<ProfilDev>, RepositoryProfilDev>();
            services.AddScoped<ILogin, RepositoryExistedUser>();

            // Service pour l'utilisation de l'objet TokenManager
            services.AddScoped<ITokenManager, TokenManager>();

            // Les services pour la police de sécurité
            services.AddAuthorization(options => {
                options.AddPolicy("clientPolicy", cpolicy => cpolicy.RequireRole("client"));
                options.AddPolicy("devPolicy", dpolicy => dpolicy.RequireRole("developper"));
            });

            // Service d'authentification
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                        ValidateIssuer = true,
                        ValidIssuer = TokenManager._issuer,
                        ValidateAudience = true,
                        ValidAudience = TokenManager._audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenManager._secretKey))
                     };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdopteUnDevApi v1"));
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
