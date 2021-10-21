using System;
using System.Text;
using AuthServer.Application.DTOs.Account;
using AuthServer.Application.Interfaces;
using AuthServer.Application.Interfaces.Repositories;
using AuthServer.Core.Entities;
using AuthServer.Infrastructure.Data;
using AuthServer.Infrastructure.Repositories;
using AuthServer.Infrastructure.Repositories.Base;
using AuthServer.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<AuthServerDbContext>(x => x.USe(configuration.GetConnectionString("SqliteConnection"),
            //         b => b.MigrationsAssembly(typeof(AuthServerDbContext).Assembly.FullName)));
            services.AddDbContext<AuthServerDbContext>(options =>
                            options.UseInMemoryDatabase("AuthServerDbContext"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AuthServerDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<ApplicationDbContext>(optionsAction =>
            {
                optionsAction.UseInMemoryDatabase("ApplicationDbContext");
            });

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IMovieRepositoryAsync, MovieRepositoryAsync>();
            #endregion

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTSettings:Audience"],
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("You are not Authorized");
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("You are not authorized to access this resource");
                    },
                };
            }

            );
        }
    }
}
