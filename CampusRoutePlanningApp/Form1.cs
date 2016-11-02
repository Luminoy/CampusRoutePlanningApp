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

namespace CampusRoutePlanningApp
{
    public partial class Form1 : KryptonForm
    {
        private FeatureLayer campusLayer;
        private KryptonRibbonGroupButton lastButton;
        private double[,] roadmaps;
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
            campusLayer = map1.AddLayer("GeoData//Campus_Union.shp") as FeatureLayer;
            map1.FunctionMode = DotSpatial.Controls.FunctionMode.Pan;
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
