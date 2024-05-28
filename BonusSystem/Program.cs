using System.Text;
using BonusSystem.BLL;
using BonusSystem.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BonusSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var db = new DbHelper();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IBonusDal, BonusDal>();
        builder.Services.AddScoped<IBonusBll, BonusBll>();
        
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "IdentityService",
                    ValidAudience = "FlowerShopServices",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fK7s92LzN8Xm6T4pGq1YvH5jR3cW8uZb"))
                };
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }
}