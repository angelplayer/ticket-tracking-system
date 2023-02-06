using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Backend.Configuration.Implementation;

namespace Backend.Configuration
{

  public static class ServiceConfiguration
  {

    public static IServiceCollection AddJWT(this IServiceCollection serviceCollection, ConfigurationManager configuration)
    {
      var jwt = configuration.GetSection("Jwt");
      var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt["Key"]));
      var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
      var issuer = jwt["Issuer"];
      var audience = jwt["Audience"];


      serviceCollection.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = issuer;
        options.Audience = audience;
        options.SigningCredentials = signingCredentials;
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingCredentials.Key,
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        NameClaimType = ClaimTypes.NameIdentifier
      };


      serviceCollection
      .AddAuthorization(options =>
      {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "oidc")
          .Build();
      })
      .AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
      {
        options.TokenValidationParameters = tokenValidationParameters;
        options.Events = new JwtBearerEvents
        {
          OnMessageReceived = (context) =>
          {
            var token = context.HttpContext.Request.Headers["Authorization"];
            if (token.Count > 0 && token[0].StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
              context.Token = token[0].Substring("Bearer ".Length).Trim();
            }

            return Task.CompletedTask;
          }
        };
      });

      serviceCollection
      .AddScoped<IPasswordHasher, PasswordHasher>()
      .AddScoped<ITokenService, TokenService>();

      return serviceCollection;
    }


    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection serviceCollection)
    {

      serviceCollection.AddAuthorization(configuration =>
      {

      });

      return serviceCollection;
    }

  }



}