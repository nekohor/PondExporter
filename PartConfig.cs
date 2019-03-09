using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace PondExporter
{
    class PartConfig
    {
        public List<PartTable> partTables;
        public PartConfig()
        {
            string jsonText = File.ReadAllText("Components/Tables/partTable.json");
            PartTableRoot root = JsonConvert.DeserializeObject<PartTableRoot>(jsonText);
            //Console.WriteLine(jsonText);
            partTables = root.partTable;
        }

        public PartTable SelectPartTable(string line)
        {
            PartTable selected = null;
            for (int i = 0; i < partTables.Count(); i++)
            {
                if (partTables[i].line == line)
                {
                    selected = partTables[i];
                }
            }
            return selected;
            
        }

        public string GetDcaFileName(string line, string partName)
        {
            string selectedPartName = "";
            PartTable pt = SelectPartTable(line);
            for (int i = 0; i < pt.table.Count(); i++)
            {
                if (pt.table[i].part == partName)
                {
                    selectedPartName =  pt.table[i].dcafile;
                }
            }
            return selectedPartName;
        }

        public string GetSignalName(string line, string partName)
        {
            string selectedSignalName = "";
            PartTable pt = SelectPartTable(line);
            for (int i = 0; i < pt.table.Count(); i++)
            {
                if (pt.table[i].part == partName)
                {
                    selectedSignalName = pt.table[i].signal;
                }
            }
            return selectedSignalName;
        }
    }

    public class Table
    {
        public string part { get; set; }
        public string dcafile { get; set; }
        public string signal { get; set; }
    }

    public class PartTable
    {
        public string line { get; set; }
        public List<Table> table { get; set; }
    }

    public class PartTableRoot
    {
        public List<PartTable> partTable { get; set; }
    }
}
