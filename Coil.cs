using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PondExporter
{
    class Coil
    {
        public string coilId;
        public Dictionary<string, Factor> factors = new Dictionary<string, Factor>();

        public Coil(Config cfg, string coilId)
        {
            this.coilId = coilId;
            List<string> factorNameList = cfg.factorConf.GetAllFactorName();
            foreach (string factorName in factorNameList)
            {
                factors[factorName] = new Factor(cfg, coilId, factorName);
            }
        }

    }
}
