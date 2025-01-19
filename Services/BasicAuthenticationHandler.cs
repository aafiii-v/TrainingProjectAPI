﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace TrainingProjectAPI.Services
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public readonly IConfiguration _configuration;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                {
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }

                var encodedCredentials = authHeader["Basic ".Length..].Trim();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var parts = decodedCredentials.Split(':', 2);

                if (parts.Length != 2)
                {
                    return AuthenticateResult.Fail("Invalid Basic Authentication Format");
                }

                var username = parts[0];
                var password = parts[1];

                if (!IsAuthorized(username, password))
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }

                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsAuthorized(string username, string password)
        {
            var userAuth = _configuration["UsernameAuth"];
            var passAuth = _configuration["PasswordAuth"];

            return username == userAuth && password == passAuth;
        }
    }
}