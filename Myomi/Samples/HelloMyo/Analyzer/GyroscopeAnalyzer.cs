using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;

namespace Myomi.Analyzer
{
    class GyroscopeAnalyzer
    {
        GyroscopeRawData _rawData;
        GyroscopeData _data;
        GyroscopeProfileData _profile;

        public GyroscopeAnalyzer(GyroscopeRawData rawData) 
        {
            this._rawData = rawData;
        }
        public void SetData(GyroscopeData data)
        {
            this._data = data;
        }
        public void SetProfile(GyroscopeProfileData profile)
        {
            this._profile = profile;
        }
    }
}
