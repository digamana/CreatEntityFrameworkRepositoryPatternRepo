namespace CreatEntityFrameworkRepositoryPattern
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtbNamespace = new System.Windows.Forms.TextBox();
            this.txtbClassName = new System.Windows.Forms.TextBox();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.lblClassName = new System.Windows.Forms.Label();
            this.lblModelName = new System.Windows.Forms.Label();
            this.txtbModelName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RdbNetCore = new System.Windows.Forms.RadioButton();
            this.RdbNetFramework = new System.Windows.Forms.RadioButton();
            this.RdbDapper = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Controls.Add(this.btnOutput, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnOpen, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 293);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(569, 68);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnOutput
            // 
            this.btnOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOutput.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.btnOutput.Location = new System.Drawing.Point(3, 3);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(392, 62);
            this.btnOutput.TabIndex = 5;
            this.btnOutput.Text = "Output";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpen.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.btnOpen.Location = new System.Drawing.Point(401, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(165, 62);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "OpenFolder";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 290F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(575, 364);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Controls.Add(this.txtbNamespace, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtbClassName, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblNamespace, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblClassName, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblModelName, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtbModelName, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(569, 284);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // txtbNamespace
            // 
            this.txtbNamespace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbNamespace.Location = new System.Drawing.Point(173, 3);
            this.txtbNamespace.Name = "txtbNamespace";
            this.txtbNamespace.Size = new System.Drawing.Size(393, 29);
            this.txtbNamespace.TabIndex = 0;
            // 
            // txtbClassName
            // 
            this.txtbClassName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbClassName.Location = new System.Drawing.Point(173, 53);
            this.txtbClassName.Name = "txtbClassName";
            this.txtbClassName.Size = new System.Drawing.Size(393, 29);
            this.txtbClassName.TabIndex = 1;
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNamespace.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblNamespace.Location = new System.Drawing.Point(3, 0);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(164, 50);
            this.lblNamespace.TabIndex = 2;
            this.lblNamespace.Text = "Namespace";
            this.lblNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClassName.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblClassName.Location = new System.Drawing.Point(3, 50);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(164, 50);
            this.lblClassName.TabIndex = 3;
            this.lblClassName.Text = "ClassName";
            this.lblClassName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModelName
            // 
            this.lblModelName.AutoSize = true;
            this.lblModelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModelName.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblModelName.Location = new System.Drawing.Point(3, 100);
            this.lblModelName.Name = "lblModelName";
            this.lblModelName.Size = new System.Drawing.Size(164, 50);
            this.lblModelName.TabIndex = 4;
            this.lblModelName.Text = "ModelName";
            this.lblModelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbModelName
            // 
            this.txtbModelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbModelName.Location = new System.Drawing.Point(173, 103);
            this.txtbModelName.Name = "txtbModelName";
            this.txtbModelName.Size = new System.Drawing.Size(393, 29);
            this.txtbModelName.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Controls.Add(this.RdbDapper);
            this.panel1.Controls.Add(this.RdbNetFramework);
            this.panel1.Controls.Add(this.RdbNetCore);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(173, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 128);
            this.panel1.TabIndex = 7;
            // 
            // RdbNetCore
            // 
            this.RdbNetCore.AutoSize = true;
            this.RdbNetCore.Location = new System.Drawing.Point(3, 41);
            this.RdbNetCore.Name = "RdbNetCore";
            this.RdbNetCore.Size = new System.Drawing.Size(309, 22);
            this.RdbNetCore.TabIndex = 8;
            this.RdbNetCore.TabStop = true;
            this.RdbNetCore.Text = "使用ASP.Net Core (加入非同步呼叫)";
            this.RdbNetCore.UseVisualStyleBackColor = true;
            this.RdbNetCore.CheckedChanged += new System.EventHandler(this.RdbEvent);
            // 
            // RdbNetFramework
            // 
            this.RdbNetFramework.AutoSize = true;
            this.RdbNetFramework.Location = new System.Drawing.Point(3, 3);
            this.RdbNetFramework.Name = "RdbNetFramework";
            this.RdbNetFramework.Size = new System.Drawing.Size(211, 22);
            this.RdbNetFramework.TabIndex = 9;
            this.RdbNetFramework.TabStop = true;
            this.RdbNetFramework.Text = "使用ASP.Net Framework";
            this.RdbNetFramework.UseVisualStyleBackColor = true;
            this.RdbNetFramework.CheckedChanged += new System.EventHandler(this.RdbEvent);
            // 
            // RdbDapper
            // 
            this.RdbDapper.AutoSize = true;
            this.RdbDapper.Location = new System.Drawing.Point(3, 81);
            this.RdbDapper.Name = "RdbDapper";
            this.RdbDapper.Size = new System.Drawing.Size(119, 22);
            this.RdbDapper.TabIndex = 10;
            this.RdbDapper.TabStop = true;
            this.RdbDapper.Text = "適合Dapper";
            this.RdbDapper.UseVisualStyleBackColor = true;
            this.RdbDapper.CheckedChanged += new System.EventHandler(this.RdbEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 364);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CreatEntityFrameworkRepositoryPattern";
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtbNamespace;
        private System.Windows.Forms.TextBox txtbClassName;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.Label lblModelName;
        private System.Windows.Forms.TextBox txtbModelName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RdbDapper;
        private System.Windows.Forms.RadioButton RdbNetFramework;
        private System.Windows.Forms.RadioButton RdbNetCore;
    }
}

