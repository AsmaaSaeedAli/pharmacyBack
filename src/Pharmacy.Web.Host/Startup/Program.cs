﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Pharmacy.Web.Helpers;

namespace Pharmacy.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CurrentDirectoryHelpers.SetCurrentDirectory();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel(opt => opt.AddServerHeader = false)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIIS()
                .UseIISIntegration()
                .UseStartup<Startup>();
        }
    }
}
