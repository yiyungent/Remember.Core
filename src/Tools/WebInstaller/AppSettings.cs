using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace WebInstaller
{
    class AppSettings
    {
        public AppSettings()
        {
            string dir = Directory.GetCurrentDirectory();
            string appSettingsFilePath = Path.Combine(dir, "appsettings.json");
            Console.WriteLine(appSettingsFilePath);
            string appSettingsJsonStr = File.ReadAllText(appSettingsFilePath);

            var temp = JsonSerializer.Deserialize<AppSettings>(appSettingsJsonStr);
            this.ConnectionStrings = temp.ConnectionStrings;
        }

        public ConnectionStringsModel ConnectionStrings { get; set; }

        public class ConnectionStringsModel
        {
            public string DefaultConnection { get; set; }
        }
    }
}
