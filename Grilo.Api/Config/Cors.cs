namespace Grilo.Api.Config
{
    public class Cors
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                    policy =>
                        {
                            policy.WithOrigins(
                                "http://localhost:5173",
                                "http://192.168.1.86:5173"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        }
                );
            });
        }
    }
}