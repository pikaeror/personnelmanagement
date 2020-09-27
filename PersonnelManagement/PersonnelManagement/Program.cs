using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonnelManagement.Models;

namespace PersonnelManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PersonnelManagement.Models.DataPeriod k = new PersonnelManagement.Models.DataPeriod(new DateTime(1988, 01, 02), new DateTime(1998, 03, 10), 1);
            PersonnelManagement.Models.DataPeriod kk = new PersonnelManagement.Models.DataPeriod(new DateTime(2000, 1, 15), new DateTime(2000, 1, 30), 1);
            //k.getDeltaDates(DateTime.Today.AddDays(-365));

            //DataPeriod.getEducationPeriods(99);

            var z = k.equalCalculationEndDate();
            var zz = kk.equalCalculationEndDate();
            var time = CustomDate.add(zz, z);
            var exp = CustomDate.add(z, new CustomDate(1988, 1, 2));
            var l = CustomDate.difference(exp, new CustomDate(1988, 1, 2));

            TelemetryDebugWriter.IsTracingDisabled = true;
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .UseIISIntegration()
                .Build();
    }
}
