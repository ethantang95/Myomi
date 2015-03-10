using Myomi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myomi.Analyzer
{
    class OrientationAnalyzer
    {
        public OrientationData Data { get; private set; }

        public OrientationAnalyzer(OrientationRawData rawData) 
        {
            TranslateRaw(rawData);
        }
        private void TranslateRaw(OrientationRawData rawData)
        {
            Data.Azimuth = CalculateThreashold(rawData.Azimuth);
            Data.Pitch = CalculateThreashold(rawData.Pitch);
            Data.Roll = CalculateThreashold(rawData.Roll);
        }
        private int CalculateThreashold(double source) 
        {
            if (source >= 0 && source < 90) 
            {
                return 1;
            }
            else if (source >= 90 && source < 180) 
            {
                return 2;
            }
            else if (source >= 180 && source < 270)
            {
                return 3;
            }
            else 
            {
                return 4;
            }
        }
    }
}
