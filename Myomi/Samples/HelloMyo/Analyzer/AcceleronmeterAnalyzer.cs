using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;

namespace Myomi.Analyzer
{
    class AcceleronmeterAnalyzer
    {
        AcceleronmeterData _data;

        public AcceleronmeterAnalyzer(AcceleronmeterRawData rawData) 
        {
            TranslateRaw(rawData);
        }

        private void TranslateRaw(AcceleronmeterRawData rawData)
        {
            double highAccelBound;
        }
    }
}
