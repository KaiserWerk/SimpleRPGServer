using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleRPGServer.Middleware;
using SimpleRPGServer.Persistence.Models;
using SimpleRPGServer.Service;

namespace SimpleRPGServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddLogging();

        builder.Services.AddDbContext<GameDbContext>(ServiceLifetime.Transient);

        builder.Services.AddSingleton<IChatService, ChatService>();
        builder.Services.AddSingleton<IMapService, MapService>();
        builder.Services.AddSingleton<IEmailService, EmailService>();
        builder.Services.AddSingleton<IPlayerService, PlayerService>();
        builder.Services.AddSingleton<INpcService, NpcService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseWhen(
            httpContext => httpContext.Request.Path.StartsWithSegments("/api/gamedata"),
            subApp => subApp.UseMiddleware<AuthTokenMiddleware>()
        );

        app.MapControllers();

        // make sure the database tables are set up
        var dbc = app.Services.GetService<GameDbContext>();
        dbc.Database.EnsureCreated();

        // make sure the NPC service is started up and running
        var npcService = app.Services.GetService<INpcService>();

        app.Run();
    }
}