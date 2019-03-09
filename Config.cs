using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IniParser;
using IniParser.Model;

namespace PondExporter
{

    class Config
    {
        public PathConfig pathConf;
        public PartConfig partConf;
        public FactorConfig factorConf;

        public string curCoilId = null;

        public int DataMaxNum;
        public int MaxPrintNum;

        public Config(string[] args)
        {
            pathConf = new PathConfig(args);
            partConf = new PartConfig();
            factorConf = new FactorConfig();

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Components/Configuration.ini");
            DataMaxNum = int.Parse(data["Data"]["DataMaxNum"]);
            MaxPrintNum = int.Parse(data["Data"]["MaxPrintNum"]);
        }
    }
}
