using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.IO;

namespace PondExporter
{
    class Try
    {
        public void HaveATry(string[] args)
        {
            Config cfg = new Config(args);

            Dictionary<string, Coil> coils = new Dictionary<string, Coil>();

            DirectoryInfo[] coilIds = cfg.pathConf.coilIds;
            foreach (DirectoryInfo coilId in coilIds)
            {
                coils[coilId.Name] = new Coil(cfg, coilId.Name);
            }

            string resultfilePath = cfg.pathConf.resultDirPath +"/ExportedData.json";
            File.WriteAllText(resultfilePath, JsonConvert.SerializeObject(coils, Formatting.Indented));

        }
    }
}
