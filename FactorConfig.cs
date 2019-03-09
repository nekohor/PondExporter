using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace PondExporter
{
    class FactorConfig
    {
        public List<FactorTable> factorTables;
        public FactorConfig()
        {
            string jsonText = File.ReadAllText("Components/Tables/factorTable.json");
            FactorTableRoot root = JsonConvert.DeserializeObject<FactorTableRoot>(jsonText);
            //Console.WriteLine(jsonText);
            factorTables = root.factorTable;
            Console.WriteLine(root.factorTable[0].seriesName);
        }

        public List<string> GetAllFactorName()
        {
            List<string> FactorNameList = new List<string>();
            foreach (FactorTable ft in factorTables)
            {
                foreach (string factor in ft.taskList)
                {
                    FactorNameList.Add(factor);
                }
            }
            return FactorNameList;
        }
    }

    public class FactorTable
    {
        public string seriesName { get; set; }
        public List<string> taskList { get; set; }
    }

    public class FactorTableRoot
    {
        public List<FactorTable> factorTable { get; set; }
    }
}
