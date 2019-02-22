namespace AspNetCore.DynaX.Myrmec
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.FileSelecter = new System.Windows.Forms.Button();
            this.HexInfoList = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CellContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyCellData = new System.Windows.Forms.ToolStripMenuItem();
            this.HexInfoListClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.HexInfoList)).BeginInit();
            this.CellContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileSelecter
            // 
            this.FileSelecter.Location = new System.Drawing.Point(12, 13);
            this.FileSelecter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FileSelecter.Name = "FileSelecter";
            this.FileSelecter.Size = new System.Drawing.Size(527, 37);
            this.FileSelecter.TabIndex = 0;
            this.FileSelecter.Text = "选择文件...";
            this.FileSelecter.UseVisualStyleBackColor = true;
            this.FileSelecter.Click += new System.EventHandler(this.FileSelecter_Click);
            // 
            // HexInfoList
            // 
            this.HexInfoList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HexInfoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HexInfoList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.FileHex});
            this.HexInfoList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HexInfoList.Location = new System.Drawing.Point(12, 58);
            this.HexInfoList.Name = "HexInfoList";
            this.HexInfoList.RowTemplate.Height = 23;
            this.HexInfoList.Size = new System.Drawing.Size(621, 358);
            this.HexInfoList.TabIndex = 1;
            this.HexInfoList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.HexInfoList_CellMouseDown);
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "文件名称";
            this.FileName.Name = "FileName";
            // 
            // FileHex
            // 
            this.FileHex.DataPropertyName = "FileHex";
            this.FileHex.HeaderText = "Hex";
            this.FileHex.Name = "FileHex";
            // 
            // CellContextMenu
            // 
            this.CellContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyCellData});
            this.CellContextMenu.Name = "CellContextMenu";
            this.CellContextMenu.Size = new System.Drawing.Size(157, 26);
            // 
            // CopyCellData
            // 
            this.CopyCellData.Image = ((System.Drawing.Image)(resources.GetObject("CopyCellData.Image")));
            this.CopyCellData.Name = "CopyCellData";
            this.CopyCellData.Size = new System.Drawing.Size(156, 22);
            this.CopyCellData.Text = "复制 HEX 内容";
            this.CopyCellData.Click += new System.EventHandler(this.CopyCellData_Click);
            // 
            // HexInfoListClear
            // 
            this.HexInfoListClear.Enabled = false;
            this.HexInfoListClear.Location = new System.Drawing.Point(545, 12);
            this.HexInfoListClear.Name = "HexInfoListClear";
            this.HexInfoListClear.Size = new System.Drawing.Size(88, 37);
            this.HexInfoListClear.TabIndex = 3;
            this.HexInfoListClear.Text = "清空";
            this.HexInfoListClear.UseVisualStyleBackColor = true;
            this.HexInfoListClear.Click += new System.EventHandler(this.HexInfoListClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 422);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Richfiter V1.0";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkVisited = true;
            this.linkLabel1.Location = new System.Drawing.Point(579, 422);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(54, 20);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "github";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 446);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HexInfoListClear);
            this.Controls.Add(this.HexInfoList);
            this.Controls.Add(this.FileSelecter);
            this.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件Hex头信息采集工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HexInfoList)).EndInit();
            this.CellContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FileSelecter;
        private System.Windows.Forms.DataGridView HexInfoList;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileHex;
        private System.Windows.Forms.ContextMenuStrip CellContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyCellData;
        private System.Windows.Forms.Button HexInfoListClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

