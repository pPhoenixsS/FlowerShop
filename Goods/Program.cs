using System.Text;
using Goods.BLL;
using Goods.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Goods;

public class Program
{
    public static void Main(string[] args)
    {
        var db = new DbHelper();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        
        var builder = WebApplication.CreateBuilder(args);

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

        builder.Services.AddSingleton<IProductDal, ProductDal>();
        builder.Services.AddScoped<IProductBll, ProductBll>();
        builder.Services.AddSingleton<IImagesDal, ImagesDal>();
        builder.Services.AddSingleton<IImagesBll, ImagesBll>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}