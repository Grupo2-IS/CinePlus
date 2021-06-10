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

           services.AddIdentity<Usuario, IdentityRole>(opts =>
            {
                
                opts.User.RequireUniqueEmail = true;
                //opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
                opts.Password.RequiredLength = 8;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })  .AddEntityFrameworkStores<AdminDAeropuertosContext>()
                 .AddDefaultTokenProviders();        

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CinePlusServices", Version = "v1" });
            });

            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();

             var builder = services.AddIdentityCore<AppUser>(o =>

      {

        // configure identity options

        o.Password.RequireDigit = false;

        o.Password.RequireLowercase = false;

        o.Password.RequireUppercase = false;

        o.Password.RequireNonAlphanumeric = false;

        o.Password.RequiredLength = 6;

      });

      builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);

      builder.AddEntityFrameworkStores<NetCore21DbContext>().AddDefaultTokenProviders();



      services.AddAutoMapper();

      services.AddMvc();

// Configure JWT

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
