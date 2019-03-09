using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace PondExporter
{
    class Part
    {
        [DllImport("Components/ReadDCADLL.dll", EntryPoint = "ReadData")]
        public static extern long ReadData(string DCAFilePath, string SignalName,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] float[] DataArray);

        public int size { get; set; }
        public float[] data { get; set; }

        public Part(Config cfg, string coilId, string partName)
        {
            string curDir = cfg.pathConf.curDirPath;
            string line = GetMillLine(coilId);
            string dcaFileName = cfg.partConf.GetDcaFileName(line, partName);
            string dcaPath = ConcatPath(curDir, coilId, dcaFileName);
            string signalName = cfg.partConf.GetSignalName(line, partName);

            data = new float[cfg.DataMaxNum];

            Console.WriteLine(dcaPath);
            Console.WriteLine(signalName);

            //dcaPath = "D:/Work/sample/data/pOND/H19013640A/FDT_POND.dca";
            //signalName = "TN\\L_FA_FDT1TEMP";

            if (File.Exists(dcaPath))
            {
                size = (int)ReadData(dcaPath, signalName, data);
            }
            else
            {
                size = -2;
            }
            Console.WriteLine("actual return size {0}", size);
            if (-1 == size || -2 == size )
            {
                size = 1;
                Console.WriteLine("[Warning] wrong DCA path or signal name in DLL function");
            }
        }

        public string GetMillLine(string coilId)
        {
            if ( coilId.StartsWith("M") )
            {
                return "1580";
            } 
            else if ( coilId.StartsWith("H") )
            {
                return "2250";
            } 
            else 
            {
                throw new Exception( 
                    string.Format("{0} {1} {2}",
                    "In JudgeLine Else Logic.",
                    "This coil from wrong line.",
                    coilId) );
            }
        }

        string ConcatPath(string curDir, string coilId, string dcaFileName)
        {
            return curDir +"/" + coilId + "/" + dcaFileName + ".dca";
        }
    }
}
