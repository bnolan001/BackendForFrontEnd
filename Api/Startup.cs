using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Api
{
    public class Startup
    {
        public Startup()
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Map controllers and require that any request to them contains the expected
                // Authorization, in this case a JWT Bearer token
                endpoints.MapControllers()
                    .RequireAuthorization();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication("jwt")
                .AddJwtBearer("jwt", options =>
                {
                    options.Authority = "https://demo.identityserver.io";
                    options.Audience = "api";
                });
        }
    }
}