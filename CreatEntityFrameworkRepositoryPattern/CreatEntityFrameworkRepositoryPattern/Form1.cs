using CreatEntityFrameworkRepositoryPattern.Domain.IDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CreatEntityFrameworkRepositoryPattern.Domain;
using CreatEntityFrameworkRepositoryPattern.Domain.Dto;

namespace CreatEntityFrameworkRepositoryPattern
{
    public partial class Form1 : Form
    {
        ICreator creator { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            WriteFile WF = WriteFile.Creat(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output"),string.Empty);
            ArguName AN = ArguName.Creat(txtbNamespace.Text,txtbClassName.Text,txtbModelName.Text);
            ICreatFile creatFile = null;
            if (RdbDapper.Checked)
            {
                creatFile =new WtihDapper(AN, WF);
            }
            else if (RdbNetCore.Checked)
            {
                creatFile = new ASPNetCore_Have_Async(AN, WF);
            }
            else if (RdbNetFramework.Checked)
            {
                creatFile = new ASPNetFramework_No_Async(AN, WF);
            }
            creatFile.CreatFile();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output"));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RdbEvent(object sender, EventArgs e)
        {

        }
    }
}
