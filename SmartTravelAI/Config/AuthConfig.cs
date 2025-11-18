using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SmartTravelAI.Config
{
    public static class AuthConfig
    {
        public static readonly string CorsPolicyKey = "AllowAll";

        public static WebApplicationBuilder WithAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddEndpointsApiExplorer();
            return builder;
        }

        public static WebApplicationBuilder WithAuthentication(this WebApplicationBuilder builder)
        {
            var settings = builder.Configuration.GetSection("JWTSettings");
            var key = settings["SECRET_KEY"];
            var issuer = settings["Issuer"];
            var audience = settings["Audience"];

            builder
                .Services.AddAuthentication(options =>
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
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                    };
                });

            return builder;
        }

        public static WebApplicationBuilder WithCORS(this WebApplicationBuilder builder)
        {
            string origin =
                builder.Configuration.GetConnectionString("Frontend")
                ?? throw new InvalidOperationException("Origin is null");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsPolicyKey,
                    builder =>
                    {
                        builder
                            .WithOrigins(origin)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }
                );
            });

            return builder;
        }
    }
}
