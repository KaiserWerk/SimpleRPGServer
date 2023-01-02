using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleRPGServer.Middleware;
using SimpleRPGServer.Models;
using SimpleRPGServer.Service;

namespace SimpleRPGServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IChatService, ChatService>();
            builder.Services.AddSingleton<IMapService, MapService>();
            builder.Services.AddSingleton<IEmailService, EmailService>();

            builder.Services.AddDbContext<GameDbContext>(ServiceLifetime.Singleton);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();

            app.UseWhen(
                httpContext => httpContext.Request.Path.StartsWithSegments("/api/gamedata"),
                subApp => subApp.UseMiddleware<AuthTokenMiddleware>()
            );

            app.MapControllers();

            var dbc = app.Services.GetService<GameDbContext>();
            dbc.Database.EnsureCreated();

            app.Run();
        }
    }
}