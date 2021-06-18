using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using CinePlus.Context;
using CinePlus.Context.Repositories;
using CinePlus.Context.Security;
using CinePlus.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;



namespace CinePlus.Services
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
            services.AddDbContext<CinePlusDb>(options => options.UseSqlServer(@"Server=(localDB)\MSSQLLocalDB;Database=CinePlusDB;Integrated Security=true;"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CinePlusServices", Version = "v1" });
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<CinePlusDb>();

            services.AddAuthorization(options =>
           {
               options.AddPolicy("ManageRolesAndClaimsPolicy",
                   policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

               options.AddPolicy("DeleteRolePolicy",
                   policy => policy.RequireClaim("Delete Role", "true"));

               options.AddPolicy("EditRolePolicy",
                   policy => policy.RequireClaim("Edit Role", "true"));

               options.AddPolicy("CreateRolePolicy",
                   policy => policy.RequireClaim("Create Role", "true"));

               options.AddPolicy("AdminRolePolicy",
                   policy => policy.RequireRole("Admin"));
           });

            services.AddScoped<IAuthorizationHandler, CanManageClaimHandler>();
            //services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
            services.AddScoped<IAuthorizationHandler, CanEditOtherAdminRolesAndClaimsHandler>();


            services.AddScoped<IRepository<Film>, FilmRepository>();
            services.AddScoped<IRepository<Room>, RoomRepository>();
            services.AddScoped<IRepository<Seat>, SeatRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Artist>, ArtistRepository>();
            services.AddScoped<IMemberPurchaseRepository, MemberPurchaseRepository>();
            services.AddScoped<INormalPurchaseRepository, NormalPurchaseRepository>();
            services.AddScoped<IShowingRepository, ShowingRepository>();
            services.AddScoped<IRepository<Member>, MemberRepository>();
            services.AddScoped<IPerformerRepository, PerformerRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();

            var jwtOptions = Configuration.GetSection(nameof(JwtOptions));
            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
                options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions[nameof(JwtOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtOptions[nameof(JwtOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtOptions[nameof(JwtOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CinePlusServices v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
