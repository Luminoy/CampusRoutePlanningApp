using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Ribbon;
using DotSpatial.Symbology;
using System.IO;
using DotSpatial.Data;
using GeoAPI.Geometries;
using System.Collections;
using static CampusRoutePlanningApp.MyUtility;

namespace CampusRoutePlanningApp
{
    public partial class Form1 : KryptonForm
    {
        private FeatureLayer campusLayer;
        private KryptonRibbonGroupButton lastButton;
        private PointLineRelation PointLineTopology = new PointLineRelation();
        public Form1()
        {
            InitializeComponent();
        }

        public FeatureLayer CampusLayer
        {
            get
            {
                return campusLayer;
            }
        }

        private void buttonSpecAny7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            campusLayer = map1.AddLayer(@"GeoData\roadmaps.shp") as FeatureLayer;
            map1.FunctionMode = DotSpatial.Controls.FunctionMode.Pan;

            // 构建拓扑关系
            TopologyConstruction(campusLayer.DataSet);
        }

        public class PointEqualityComparer: IEqualityComparer<PointF>
        {
            public bool Equals(PointF P1, PointF P2)
            {
                return ((P1.X - P2.X) < 1e-6) && ((P1.Y - P2.Y) < 1e-6);
            }

            public int GetHashCode(PointF obj)
            {
                if (obj == null)
                    return 0;
                else
                    return obj.X.ToString().GetHashCode() + obj.Y.ToString().GetHashCode();
            }
        }
        private void TopologyConstruction(IFeatureSet pFeatureSet)
        {
            List<TopoPoint> pTopoPoints = new List<TopoPoint>();
            List<PointF> XYs = new List<PointF>();
            List<Line> LINEs = new List<Line>();
            //HashSet<TopoPoint> m_HashSet = new HashSet<TopoPoint>();
            int nLineCount = 0;
            
            foreach (IFeature pFeature in pFeatureSet.Features)
            {
                ILineString pLine = pFeature.Geometry as ILineString;
                int nPointCount = pLine.NumPoints;
                for (int k = 0; k < nPointCount - 1; k++)
                {
                    IPoint P1 = pLine.GetPointN(k);
                    IPoint P2 = pLine.GetPointN(k + 1);

                    PointF pf1 = new PointF();
                    pf1.X = (float)P1.X;
                    pf1.Y = (float)P1.Y;
                    XYs.Add(pf1);

                    PointF pf2 = new PointF();
                    pf2.X = (float)P2.X;
                    pf2.Y = (float)P2.Y;
                    XYs.Add(pf2);

                    Line line = new Line();
                    line.StartPoint = pf1;
                    line.EndPoint = pf2;
                    line.LineNumber = nLineCount++;
                    LINEs.Add(line);
                }
            }
            List<PointF> newXYs = XYs.Distinct(new PointEqualityComparer()).ToList();

            IDictionary<PointF, int> hashTable = new Dictionary<PointF, int>(new PointEqualityComparer());
            for (int k = 0; k < newXYs.Count; k++)
            {
                hashTable.Add(newXYs[k], k);
                pTopoPoints.Add(new TopoPoint());
            }
            
            for(int k = 0; k < LINEs.Count; k++)
            {
                int nId1 = hashTable[LINEs[k].StartPoint];
                int nId2 = hashTable[LINEs[k].EndPoint];
                pTopoPoints[nId1].nLineCount++;
                pTopoPoints[nId1].pConnectedLineIDs.Add(k);
                pTopoPoints[nId2].nLineCount++;
                pTopoPoints[nId2].pConnectedLineIDs.Add(k);
            }

            PointLineTopology.PointCount = newXYs.Count;
            PointLineTopology.LineCount = LINEs.Count;
            PointLineTopology.pPoints = newXYs;
            PointLineTopology.pLines = LINEs;
            PointLineTopology.pTopoPoints = pTopoPoints;

        }

        private void kryptonRibbonGroupButton1_Click(object sender, EventArgs e)
        {
            map1.FunctionMode = DotSpatial.Controls.FunctionMode.Pan;
            UpdateNavButtons(sender);
        }

        private void kryptonRibbonGroupButton2_Click(object sender, EventArgs e)
        {
            map1.FunctionMode = DotSpatial.Controls.FunctionMode.ZoomIn;
            UpdateNavButtons(sender);
        }

        private void UpdateNavButtons(object sender)
        {
            KryptonRibbonGroupButton button = sender as KryptonRibbonGroupButton;
            button.Checked = true;
            if (lastButton != null)
            {
                lastButton.Checked = false;              
            }
            lastButton = sender as KryptonRibbonGroupButton;
        }

        private void kryptonRibbonGroupButton3_Click(object sender, EventArgs e)
        {
            map1.FunctionMode = DotSpatial.Controls.FunctionMode.ZoomOut;
            UpdateNavButtons(sender);
        }

        private void kryptonRibbonGroupButton6_Click(object sender, EventArgs e)
        {
            // 获取起点坐标
            
        }

        private void kryptonRibbonGroupButton7_Click(object sender, EventArgs e)
        {
            // 获取终点坐标
        }

        private void kryptonRibbonGroupButton8_Click(object sender, EventArgs e)
        {
            // 添加障碍（获取障碍坐标）
        }

        private void kryptonRibbonGroupButton9_Click(object sender, EventArgs e)
        {
            // 计算最短路径
        }
    }
}
