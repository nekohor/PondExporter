using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PondExporter
{
    class PathConfig
    {
        public string curDirPath { get; set; }
        public string resultDirPath { get; set; }
        public DirectoryInfo[] coilIds { get; set; }

        public PathConfig(string[] args)
        {
            curDirPath = args[0];
            resultDirPath = args[1];
            
            Console.WriteLine(curDirPath);
            Console.WriteLine(resultDirPath);

            DirectoryInfo CurDir = new DirectoryInfo(curDirPath);
            DirectoryInfo ResultDir = new DirectoryInfo(resultDirPath);

            coilIds = CurDir.GetDirectories();
            foreach (DirectoryInfo d in coilIds)
            {
                Console.WriteLine(d.FullName);
            }
        }
    }
}
