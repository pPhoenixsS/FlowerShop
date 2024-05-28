using System.Text;
using Identity.BLL;
using Identity.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Identity;

public class Program
{
    public static void Main(string[] args)
    {
        /*using (var db = new DbHelper())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }*/
        
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IUserDal, UserDal>();
        builder.Services.AddScoped<IUserBll, UserBll>();
        builder.Services.AddSingleton<ISessionDal, SessionDal>();
        builder.Services.AddScoped<ISessionBll, SessionBll>();

        // Add services to the container.
        builder.Services.AddAuthorization();
        
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

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

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