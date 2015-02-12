using Myomi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myomi.Analyzer
{
    class OrientationAnalyzer
    {
        OrientationRawData _rawData;
        OrientationData _data;
        OrientationProfileData _profile;

        public OrientationAnalyzer(OrientationRawData rawData) 
        {
            this._rawData = rawData;
        }
        public void SetData(OrientationData data)
        {
            this._data = data;
        }
        public void SetProfile(OrientationProfileData profile)
        {
            this._profile = profile;
        }
    }
}
