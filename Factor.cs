using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PondExporter
{
    class Factor
    {
        public string factorName { get; set; }
        public List<float> data { get; set; }

        public Factor(Config cfg, string coilId, string factorName)
        {
            this.factorName = factorName;
            data = GetData(cfg, coilId, factorName);

            //print factor name and factor data
            Console.WriteLine("");
            Console.WriteLine(factorName);
            int printNum;
            if (data.Count < cfg.MaxPrintNum)
            {
                printNum = data.Count;
            }
            else
            {
                printNum = cfg.MaxPrintNum;
            }
            for (int i = 0; i < printNum; i++)
            {
                Console.Write(data[i]);
                Console.Write(",");
            }
            Console.WriteLine("");

        }

        public List<float> GetData(Config cfg, string coilId, string factorName)
        {
            List<float> curData = new List<float>();
            switch (factorName)
            {
                case "leveling1":
                case "leveling2":
                case "leveling3":
                case "leveling4":
                case "leveling5":
                case "leveling6":
                case "leveling7":
                    int stringLength = factorName.Length;
                    string std = factorName.Substring(stringLength - 1, 1);
                    curData = GetFactorData2(cfg, coilId, "os_gap" + std, "ds_gap" + std);
                    break;
                case "asym_flt":
                    curData = GetFactorData2(cfg, coilId, "flt_ro1", "flt_ro5");
                    break;
                case "sym_flt":
                    curData = GetFactorData3Reverse(cfg, coilId, "flt_ro1", "flt_ro3", "flt_ro5");
                    break;
                case "looper_angle7":
                    curData = GetFactorData0();
                    break;
                default:
                    curData = GetFactorData1(cfg, coilId, factorName);
                    break;
            }
            return curData;

        }

        public List<float> GetFactorData0()
        {
            List<float> factorData = new List<float>();
            factorData.Add(0);
            return factorData;
        }

        public List<float> GetFactorData1(Config cfg, string coilId, string partName)
        {
            Part part = new Part(cfg, coilId, partName);
            List<float> factorData = new List<float>(part.size);
            for (int i = 0; i < part.size; i++)
            {
                factorData.Add(part.data[i]);
            }

            return factorData;
        }

        public List<float> GetFactorData2(Config cfg, string coilId, string osName, string dsName)
        {
            Part partOS = new Part(cfg, coilId, osName);
            Part partDS = new Part(cfg, coilId, dsName);
            List<float> factorData = new List<float>(partOS.size);
            for (int i = 0; i < partOS.size; i++)
            {
                factorData.Add(partOS.data[i] - partDS.data[i]);
            }
            return factorData;
        }

        public List<float> GetFactorData3(Config cfg, string coilId, string osName, string ctName, string dsName)
        {
            Part partOS = new Part(cfg, coilId, osName);
            Part partCT = new Part(cfg, coilId, ctName);
            Part partDS = new Part(cfg, coilId, dsName);
            List<float> factorData = new List<float>(partCT.size);
            for (int i = 0; i < partOS.size; i++)
            {
                factorData.Add(partCT.data[i] - (partOS.data[i] + partDS.data[i]) / 2);
            }
            return factorData;
        }

        public List<float> GetFactorData3Reverse(Config cfg, string coilId, string osName, string ctName, string dsName)
        {
            Part partOS = new Part(cfg, coilId, osName);
            Part partCT = new Part(cfg, coilId, ctName);
            Part partDS = new Part(cfg, coilId, dsName);
            List<float> factorData = new List<float>(partCT.size);
            for (int i = 0; i < partOS.size; i++)
            {
                factorData.Add((partOS.data[i] + partDS.data[i]) / 2 - partCT.data[i]);
            }
            return factorData;
        }
    }
}
