using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRoutePlanningApp
{
    public class MyUtility
    {
        public class TopoPoint
        {
            public int nLineCount { get; set; }
            public List<long> pConnectedLineIDs = new List<long>();

        }

        public class Line
        {
            public int LineNumber { get; set; }
            public PointF StartPoint { get; set; }
            
            public PointF EndPoint { get; set; }

        }
        public class PointLineRelation
        {
            public int PointCount { get; set; }
            public int LineCount { get; set; }
            public List<TopoPoint> pTopoPoints{ get; set; }
            public List<PointF> pPoints { get; set; }
            public List<Line> pLines { get; set; }
            public TopoPoint GetPointByIndex(int K)
            {
                if ((K >= PointCount) || (pTopoPoints == null)) return null;
                return pTopoPoints[K];
            }

        }
    }
    
}
