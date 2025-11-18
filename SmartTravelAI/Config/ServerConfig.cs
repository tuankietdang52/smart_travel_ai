using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartTravelAI.Data;

namespace SmartTravelAI.Config
{
    public static class ServerConfig
    {
        public static WebApplicationBuilder UsePostgresDatabase(this WebApplicationBuilder builder)
        {
            var serverSettings = builder.Configuration.GetSection("SERVER");
            string username = serverSettings["USERNAME"]!;
            string password = serverSettings["PASSWORD"]!;
            string con = builder.Configuration.GetConnectionString("Server")!;

            con = $"{con};Username={username};Password={password}";

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(con);
            });

            return builder;
        }
    }
}