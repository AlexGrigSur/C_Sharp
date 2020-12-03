namespace NavTest//NavTestNoteBookNeConsolb
{
    partial class DrawingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ChooseLevelComboBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateNode = new System.Windows.Forms.Button();
            this.EditNode = new System.Windows.Forms.Button();
            this.DeleteNode = new System.Windows.Forms.Button();
            this.CreateEdge = new System.Windows.Forms.Button();
            this.DeleteEdge = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DrawToLeftToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelTypeInOutput = new System.Windows.Forms.Label();
            this.comboBoxTypeInOutput = new System.Windows.Forms.ComboBox();
            this.textBoxTypeInOutput = new System.Windows.Forms.TextBox();
            this.labelNameInOutput = new System.Windows.Forms.Label();
            this.textBoxNameInOutput = new System.Windows.Forms.TextBox();
            this.labelDescriptionInOutput = new System.Windows.Forms.Label();
            this.textBoxDescriptionInOutput = new System.Windows.Forms.TextBox();
            this.labelXInOutput = new System.Windows.Forms.Label();
            this.textBoxXInOutput = new System.Windows.Forms.TextBox();
            this.labelYInOutput = new System.Windows.Forms.Label();
            this.textBoxYInOutput = new System.Windows.Forms.TextBox();
            this.MainActivityButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ModeStatusLable = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(128, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(903, 560);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // ChooseLevelComboBox
            // 
            this.ChooseLevelComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseLevelComboBox.FormattingEnabled = true;
            this.ChooseLevelComboBox.Location = new System.Drawing.Point(3, 19);
            this.ChooseLevelComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChooseLevelComboBox.Name = "ChooseLevelComboBox";
            this.ChooseLevelComboBox.Size = new System.Drawing.Size(112, 24);
            this.ChooseLevelComboBox.TabIndex = 1;
            this.ChooseLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.ChooseLevelComboBox_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.ChooseLevelComboBox);
            this.flowLayoutPanel1.Controls.Add(this.CreateNode);
            this.flowLayoutPanel1.Controls.Add(this.EditNode);
            this.flowLayoutPanel1.Controls.Add(this.DeleteNode);
            this.flowLayoutPanel1.Controls.Add(this.CreateEdge);
            this.flowLayoutPanel1.Controls.Add(this.DeleteEdge);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 36);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(121, 316);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Этаж";
            // 
            // CreateNode
            // 
            this.CreateNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateNode.Location = new System.Drawing.Point(3, 47);
            this.CreateNode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CreateNode.Name = "CreateNode";
            this.CreateNode.Size = new System.Drawing.Size(112, 46);
            this.CreateNode.TabIndex = 0;
            this.CreateNode.Text = "Create Node";
            this.CreateNode.UseVisualStyleBackColor = true;
            this.CreateNode.Click += new System.EventHandler(this.CreateNode_Click);
            // 
            // EditNode
            // 
            this.EditNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.EditNode.Location = new System.Drawing.Point(3, 97);
            this.EditNode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditNode.Name = "EditNode";
            this.EditNode.Size = new System.Drawing.Size(112, 46);
            this.EditNode.TabIndex = 3;
            this.EditNode.Text = "Edit Node";
            this.EditNode.UseVisualStyleBackColor = true;
            this.EditNode.Click += new System.EventHandler(this.EditNode_Click);
            // 
            // DeleteNode
            // 
            this.DeleteNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteNode.Location = new System.Drawing.Point(3, 147);
            this.DeleteNode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteNode.Name = "DeleteNode";
            this.DeleteNode.Size = new System.Drawing.Size(112, 46);
            this.DeleteNode.TabIndex = 2;
            this.DeleteNode.Text = "Delete Node";
            this.DeleteNode.UseVisualStyleBackColor = true;
            this.DeleteNode.Click += new System.EventHandler(this.DeleteNode_Click);
            // 
            // CreateEdge
            // 
            this.CreateEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateEdge.Location = new System.Drawing.Point(3, 197);
            this.CreateEdge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CreateEdge.Name = "CreateEdge";
            this.CreateEdge.Size = new System.Drawing.Size(112, 46);
            this.CreateEdge.TabIndex = 1;
            this.CreateEdge.Text = "Create Edge";
            this.CreateEdge.UseVisualStyleBackColor = true;
            this.CreateEdge.Click += new System.EventHandler(this.CreateEdge_Click);
            // 
            // DeleteEdge
            // 
            this.DeleteEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteEdge.Location = new System.Drawing.Point(3, 247);
            this.DeleteEdge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteEdge.Name = "DeleteEdge";
            this.DeleteEdge.Size = new System.Drawing.Size(112, 46);
            this.DeleteEdge.TabIndex = 4;
            this.DeleteEdge.Text = "Delete Edge";
            this.DeleteEdge.UseVisualStyleBackColor = true;
            this.DeleteEdge.Click += new System.EventHandler(this.DeleteEdge_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.DrawToLeftToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1196, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(34, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(131, 24);
            this.toolStripButton2.Text = "Удалить уровень";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // DrawToLeftToolStripButton
            // 
            this.DrawToLeftToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DrawToLeftToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("DrawToLeftToolStripButton.Image")));
            this.DrawToLeftToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DrawToLeftToolStripButton.Name = "DrawToLeftToolStripButton";
            this.DrawToLeftToolStripButton.Size = new System.Drawing.Size(125, 24);
            this.DrawToLeftToolStripButton.Text = "Растянуть влево";
            this.DrawToLeftToolStripButton.Click += new System.EventHandler(this.DrawToLeftToolStripButton_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.labelTypeInOutput);
            this.flowLayoutPanel2.Controls.Add(this.comboBoxTypeInOutput);
            this.flowLayoutPanel2.Controls.Add(this.textBoxTypeInOutput);
            this.flowLayoutPanel2.Controls.Add(this.labelNameInOutput);
            this.flowLayoutPanel2.Controls.Add(this.textBoxNameInOutput);
            this.flowLayoutPanel2.Controls.Add(this.labelDescriptionInOutput);
            this.flowLayoutPanel2.Controls.Add(this.textBoxDescriptionInOutput);
            this.flowLayoutPanel2.Controls.Add(this.labelXInOutput);
            this.flowLayoutPanel2.Controls.Add(this.textBoxXInOutput);
            this.flowLayoutPanel2.Controls.Add(this.labelYInOutput);
            this.flowLayoutPanel2.Controls.Add(this.textBoxYInOutput);
            this.flowLayoutPanel2.Controls.Add(this.MainActivityButton);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1036, 36);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(145, 336);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // labelTypeInOutput
            // 
            this.labelTypeInOutput.AutoSize = true;
            this.labelTypeInOutput.Location = new System.Drawing.Point(4, 0);
            this.labelTypeInOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTypeInOutput.Name = "labelTypeInOutput";
            this.labelTypeInOutput.Size = new System.Drawing.Size(33, 17);
            this.labelTypeInOutput.TabIndex = 5;
            this.labelTypeInOutput.Text = "Тип";
            // 
            // comboBoxTypeInOutput
            // 
            this.comboBoxTypeInOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeInOutput.FormattingEnabled = true;
            this.comboBoxTypeInOutput.Items.AddRange(new object[] {
            "Коридор",
            "Кабинет",
            "Лестница"});
            this.comboBoxTypeInOutput.Location = new System.Drawing.Point(4, 21);
            this.comboBoxTypeInOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxTypeInOutput.Name = "comboBoxTypeInOutput";
            this.comboBoxTypeInOutput.Size = new System.Drawing.Size(127, 24);
            this.comboBoxTypeInOutput.TabIndex = 2;
            this.comboBoxTypeInOutput.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeInOutput_SelectedIndexChanged);
            // 
            // textBoxTypeInOutput
            // 
            this.textBoxTypeInOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTypeInOutput.Location = new System.Drawing.Point(4, 53);
            this.textBoxTypeInOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxTypeInOutput.Name = "textBoxTypeInOutput";
            this.textBoxTypeInOutput.ReadOnly = true;
            this.textBoxTypeInOutput.Size = new System.Drawing.Size(127, 22);
            this.textBoxTypeInOutput.TabIndex = 13;
            // 
            // labelNameInOutput
            // 
            this.labelNameInOutput.AutoSize = true;
            this.labelNameInOutput.Location = new System.Drawing.Point(4, 79);
            this.labelNameInOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNameInOutput.Name = "labelNameInOutput";
            this.labelNameInOutput.Size = new System.Drawing.Size(72, 17);
            this.labelNameInOutput.TabIndex = 0;
            this.labelNameInOutput.Text = "Название";
            // 
            // textBoxNameInOutput
            // 
            this.textBoxNameInOutput.Location = new System.Drawing.Point(4, 100);
            this.textBoxNameInOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNameInOutput.Name = "textBoxNameInOutput";
            this.textBoxNameInOutput.Size = new System.Drawing.Size(127, 22);
            this.textBoxNameInOutput.TabIndex = 1;
            // 
            // labelDescriptionInOutput
            // 
            this.labelDescriptionInOutput.AutoSize = true;
            this.labelDescriptionInOutput.Location = new System.Drawing.Point(4, 126);
            this.labelDescriptionInOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDescriptionInOutput.Name = "labelDescriptionInOutput";
            this.labelDescriptionInOutput.Size = new System.Drawing.Size(74, 17);
            this.labelDescriptionInOutput.TabIndex = 6;
            this.labelDescriptionInOutput.Text = "Описание";
            // 
            // textBoxDescriptionInOutput
            // 
            this.textBoxDescriptionInOutput.Location = new System.Drawing.Point(4, 147);
            this.textBoxDescriptionInOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDescriptionInOutput.Multiline = true;
            this.textBoxDescriptionInOutput.Name = "textBoxDescriptionInOutput";
            this.textBoxDescriptionInOutput.Size = new System.Drawing.Size(127, 43);
            this.textBoxDescriptionInOutput.TabIndex = 7;
            // 
            // labelXInOutput
            // 
            this.labelXInOutput.AutoSize = true;
            this.labelXInOutput.Location = new System.Drawing.Point(4, 194);
            this.labelXInOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelXInOutput.Name = "labelXInOutput";
            this.labelXInOutput.Size = new System.Drawing.Size(17, 17);
            this.labelXInOutput.TabIndex = 8;
            this.labelXInOutput.Text = "X";
            // 
            // textBoxXInOutput
            // 
            this.textBoxXInOutput.Location = new System.Drawing.Point(4, 215);
            this.textBoxXInOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxXInOutput.Name = "textBoxXInOutput";
            this.textBoxXInOutput.ReadOnly = true;
            this.textBoxXInOutput.Size = new System.Drawing.Size(127, 22);
            this.textBoxXInOutput.TabIndex = 9;
            // 
            // labelYInOutput
            // 
            this.labelYInOutput.AutoSize = true;
            this.labelYInOutput.Location = new System.Drawing.Point(4, 241);
            this.labelYInOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelYInOutput.Name = "labelYInOutput";
            this.labelYInOutput.Size = new System.Drawing.Size(17, 17);
            this.labelYInOutput.TabIndex = 10;
            this.labelYInOutput.Text = "Y";
            // 
            // textBoxYInOutput
            // 
            this.textBoxYInOutput.Location = new System.Drawing.Point(4, 262);
            this.textBoxYInOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxYInOutput.Name = "textBoxYInOutput";
            this.textBoxYInOutput.ReadOnly = true;
            this.textBoxYInOutput.Size = new System.Drawing.Size(127, 22);
            this.textBoxYInOutput.TabIndex = 11;
            // 
            // MainActivityButton
            // 
            this.MainActivityButton.Location = new System.Drawing.Point(3, 290);
            this.MainActivityButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MainActivityButton.Name = "MainActivityButton";
            this.MainActivityButton.Size = new System.Drawing.Size(129, 38);
            this.MainActivityButton.TabIndex = 12;
            this.MainActivityButton.Text = "Continue";
            this.MainActivityButton.UseVisualStyleBackColor = true;
            this.MainActivityButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModeStatusLable});
            this.statusStrip1.Location = new System.Drawing.Point(0, 591);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1196, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ModeStatusLable
            // 
            this.ModeStatusLable.Name = "ModeStatusLable";
            this.ModeStatusLable.Size = new System.Drawing.Size(151, 20);
            this.ModeStatusLable.Text = "toolStripStatusLabel1";
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1196, 617);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DrawingForm";
            this.Text = "Draw Interface";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button CreateNode;
        private System.Windows.Forms.Button EditNode;
        private System.Windows.Forms.Button DeleteNode;
        private System.Windows.Forms.Button CreateEdge;
        private System.Windows.Forms.Button DeleteEdge;
        private System.Windows.Forms.ComboBox ChooseLevelComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton DrawToLeftToolStripButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label labelNameInOutput;
        private System.Windows.Forms.TextBox textBoxNameInOutput;
        private System.Windows.Forms.Label labelTypeInOutput;
        private System.Windows.Forms.ComboBox comboBoxTypeInOutput;
        private System.Windows.Forms.Label labelDescriptionInOutput;
        private System.Windows.Forms.TextBox textBoxDescriptionInOutput;
        private System.Windows.Forms.Label labelXInOutput;
        private System.Windows.Forms.TextBox textBoxXInOutput;
        private System.Windows.Forms.Label labelYInOutput;
        private System.Windows.Forms.TextBox textBoxYInOutput;
        private System.Windows.Forms.Button MainActivityButton;
        private System.Windows.Forms.TextBox textBoxTypeInOutput;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ModeStatusLable;
    }
}

