using Prometheus;

namespace FastTechFoods.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                var method = context.Request.Method;
                var path = context.Request.Path;
                var timestamp = DateTime.UtcNow.ToString("s");
                Console.WriteLine($"[{timestamp}] {method} {path}");
                await next.Invoke();
            });


            app.MapReverseProxy();
            app.UseMetricServer(); 
            app.UseHttpMetrics();
            app.Run();

        }
    }
}
