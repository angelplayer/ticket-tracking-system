// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using System.Security.Claims;
using System.Text;
using Backend.Configuration.Implementation;
using Backend.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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
          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
          .Build();

        // options.AddPolicy("DeleteTicketPolicy", policy => policy.Requirements.Add(new DeleteTicketRequirement()));
        // options.AddPolicy("CreateTicketPolicy", policy => policy.Requirements.Add(new CreateTicketRequirement()));
        // options.AddPolicy("ModifyTicketPolicy", policy => policy.Requirements.Add(new ModifyTicketRequirement()));
        options.AddPolicy("CreateTicketPolicy", policy => policy.AddRequirements(new TicketPolicyRequirement(Domain.UserType.QA)));
        options.AddPolicy("ModifyTicketPolicy", policy => policy.AddRequirements(new TicketPolicyRequirement(Domain.UserType.QA)));
        options.AddPolicy("DeleteTicketPolicy", policy => policy.AddRequirements(new TicketPolicyRequirement(Domain.UserType.QA)));
        options.AddPolicy("ResolveTicketPolicy", policy => policy.AddRequirements(new TicketPolicyRequirement(Domain.UserType.RD)));

      })
      // .AddScoped<IAuthorizationHandler, CreateTicketPolicyHandle>()
      // .AddScoped<IAuthorizationHandler, DeleteTicketPolicyHandler>()
      // .AddScoped<IAuthorizationHandler, ModifyTicketHandler>()
      .AddTransient<IAuthorizationHandler, TicketPolicyHandler>()
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
  }



}
