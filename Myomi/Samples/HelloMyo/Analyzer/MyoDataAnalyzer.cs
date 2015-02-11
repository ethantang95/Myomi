using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myomi.Data;

namespace Myomi.Analyzer
{

    //the results are simply used to see what kind of matching it is
    //Match, NoMatch, and NotAnalyzed are straight forward
    //SlightMatch is if it matches with an adjacent level, NotRest is when a rest is detected/expected and obtained is not that
    enum Result { Match, SlightMatch, NoMatch, NotRest, NotAnalyzed }

    internal class MyoDataAnalyzer
    {
        public static int Evaluator(MyoData obtained, MyoDataProfile toCompare)
        {
            int score = 100;

            return score;
        }

        public static MyoData GetDataFromRaw(MyoRawData rawData)
        {
            MyoData toReturn = new MyoData();


            return null;
        }
    }
}
