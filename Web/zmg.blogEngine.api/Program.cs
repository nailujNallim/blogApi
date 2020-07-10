using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace zmg.blogEngine.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args != null && args.Length > 0 && args[0].ToUpper().Equals("RUNASYNC"))
            {
                //  si se ejecuta con RunAsync como parámetro, el servidor se construye, se ejecuta y se detiene.
                //  se usa en C.I. para detectar errores de nHibernate en tiempo de EJECUCIÓN

                CreateWebHostBuilder(args).Build().RunAsync();
            }
            else
            {
                CreateWebHostBuilder(args).Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddAzureWebAppDiagnostics();
                });
    }
}
