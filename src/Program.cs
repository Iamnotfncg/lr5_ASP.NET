//╭━━━━━━╮ 
//┃╭━━━━━╯┈┈▂▂▂▂▂
//┃┃▕╲▂▂▂╱▏╱▔▔▔▔▔╲
//┃╰▕╭▃┈╭▃▏▏╱▔▔╲╱▏▏
//┃┈▕╰━▃╰━▏▏▏▋┊┈╳▏▏
//┃┈┈╲╰┻╯╱┈▏╲━╮╱╲▏▏
//┃┣━┫▔▔▔┃┈▏┈▔▔┈┈▕
//╰╯┈╰╯┈╰╯┈╲▂▂▂▂▂╱

using pr5.src.Logging;

namespace pr5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();

            builder.Services.AddTransient<IMiddleware>(_ => new ErrorLoggingService(
                    _.GetRequiredService<ILogger<ErrorLoggingService>>(),
                    "error-log.txt"));

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();

            app.MapControllers();
            app.Run();
        }
    }
}