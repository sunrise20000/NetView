using NetView.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace NetView.Config
{
    public class ConfigMgr : Singleton<ConfigMgr>
    {
        string FILE_DEVICE = @"Config/Device.json";
        
        public void LoadConfig()
        {
            #region Device
            try
            {
                var StrJson = File.ReadAllText(FILE_DEVICE);
                DeviceCfgEntry = JsonConvert.DeserializeObject<DeviceConfigEntry>(StrJson);
            }
            catch(Exception ex) {
                throw new Exception($"Failed to load config {FILE_DEVICE}, detail infomation:{ex.Message}");
            }
            
            #endregion
        }


        public DeviceConfigEntry DeviceCfgEntry { get; private set; } = null;
    }
}
