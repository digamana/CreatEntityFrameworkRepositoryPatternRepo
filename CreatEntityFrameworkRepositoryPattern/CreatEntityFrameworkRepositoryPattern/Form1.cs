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
            if (string.IsNullOrEmpty(txtbNamespace.Text))
            {
                MessageBox.Show($"Namespace 不可空白");
                return;
            }
            else if (string.IsNullOrEmpty(txtbClassName.Text))
            {
                MessageBox.Show($"ClassName 不可空白");
                return;
            }
             creator = new ICreator();
            //取得指定路徑底下的所有資料夾名稱
           
            creator.CreatIRepository(txtbNamespace.Text);
            creator.CreatIRepository(txtbNamespace.Text, txtbClassName.Text);
            creator.CreatRepository(txtbNamespace.Text);
            creator.CreatRepository(txtbNamespace.Text, txtbClassName.Text);
            creator.CreatUnitOfWork(txtbNamespace.Text);
            creator.CreatIUnitOfWork(txtbNamespace.Text);
            Process.Start(creator.FilePath);

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output"));
        }
    }
}
