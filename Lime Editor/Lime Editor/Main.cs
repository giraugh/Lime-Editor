using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;

namespace Lime_Editor
{
    public partial class Main : Form
    {

        string DefProject = "D:/REPOS/LimeEditor/default";

        public Main() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loading.Load_Project(DefProject, Icons);
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                
                Loading.Load_Project(fbd.SelectedPath, Icons);
            }
        }
    }
}
