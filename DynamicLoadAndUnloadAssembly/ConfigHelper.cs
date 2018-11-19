using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DynamicLoadAndUnloadAssembly.ServiceAgent
{
    public class ConfigHelper
    {
        public static string GetValue(string configName)
        {
            string configValue = ConfigurationSettings.AppSettings[configName];
            if (configValue != null && configValue != "")
            {
                return configValue.ToString();
            }
            return "";
        }
    }
}
