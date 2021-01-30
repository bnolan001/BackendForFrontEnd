using IdentityModel.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseMiddleware<StrictSameSiteExternalAuthenticationMiddleware>();
            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                // We want the entire application to enforce authentication from the start
                if (!context.User.Identity.IsAuthenticated)
                {
                    // No need to have a custom page for authentication. This call will redirect the
                    // user to the Identity Server login page
                    await context.ChallengeAsync();
                    return;
                }

                await next();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    // .RequireAuthorization();
                    endpoints.MapReverseProxy(proxyPipeline =>
                    {
                        // The proxied controllers need the bearer token
                        proxyPipeline.Use(async (context, next) =>
                        {
                            var token = await context.GetTokenAsync("access_token");
                            context.Request.Headers.Add("Authorization", $"Bearer {token}");

                            await next().ConfigureAwait(false);
                        });
                    });
                });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddReverseProxy()
                .LoadFromConfig(Configuration.GetSection("ReverseProxy"));
            // We want to enable the automatic management of tokens, auto refresh, in-memory storage
            services.AddAccessTokenManagement();

            services.AddControllers();

            // Enable the in-memory storage of tokens. In production or a multi-hosting environment
            // you will want to use a SQL Server or Redis-like cache so tokens aren't lost during a
            // reboot or deployment.
            services.AddDistributedMemoryCache();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("cookies", options =>
            {
                options.Cookie.Name = "bff";
                options.Cookie.SameSite = SameSiteMode.Strict;
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://demo.identityserver.io";
                options.ClientId = "interactive.confidential";
                options.ClientSecret = "secret";

                options.ResponseType = "code";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("api");
                options.Scope.Add("offline_access");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });
        }
    }
}