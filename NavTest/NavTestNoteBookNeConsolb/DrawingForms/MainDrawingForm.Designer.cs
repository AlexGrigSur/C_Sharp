namespace NavTestNoteBookNeConsolb
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
            this.button1 = new System.Windows.Forms.Button();
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
            this.panel1.Location = new System.Drawing.Point(96, 22);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 455);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
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
            this.ChooseLevelComboBox.Location = new System.Drawing.Point(2, 15);
            this.ChooseLevelComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.ChooseLevelComboBox.Name = "ChooseLevelComboBox";
            this.ChooseLevelComboBox.Size = new System.Drawing.Size(85, 21);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 29);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(91, 257);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Этаж";
            // 
            // CreateNode
            // 
            this.CreateNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateNode.Location = new System.Drawing.Point(2, 40);
            this.CreateNode.Margin = new System.Windows.Forms.Padding(2);
            this.CreateNode.Name = "CreateNode";
            this.CreateNode.Size = new System.Drawing.Size(84, 37);
            this.CreateNode.TabIndex = 0;
            this.CreateNode.Text = "Create Node";
            this.CreateNode.UseVisualStyleBackColor = true;
            this.CreateNode.Click += new System.EventHandler(this.CreateNode_Click);
            // 
            // EditNode
            // 
            this.EditNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.EditNode.Location = new System.Drawing.Point(2, 81);
            this.EditNode.Margin = new System.Windows.Forms.Padding(2);
            this.EditNode.Name = "EditNode";
            this.EditNode.Size = new System.Drawing.Size(84, 37);
            this.EditNode.TabIndex = 3;
            this.EditNode.Text = "Edit Node";
            this.EditNode.UseVisualStyleBackColor = true;
            this.EditNode.Click += new System.EventHandler(this.EditNode_Click);
            // 
            // DeleteNode
            // 
            this.DeleteNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteNode.Location = new System.Drawing.Point(2, 122);
            this.DeleteNode.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteNode.Name = "DeleteNode";
            this.DeleteNode.Size = new System.Drawing.Size(84, 37);
            this.DeleteNode.TabIndex = 2;
            this.DeleteNode.Text = "Delete Node";
            this.DeleteNode.UseVisualStyleBackColor = true;
            this.DeleteNode.Click += new System.EventHandler(this.DeleteNode_Click);
            // 
            // CreateEdge
            // 
            this.CreateEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateEdge.Location = new System.Drawing.Point(2, 163);
            this.CreateEdge.Margin = new System.Windows.Forms.Padding(2);
            this.CreateEdge.Name = "CreateEdge";
            this.CreateEdge.Size = new System.Drawing.Size(84, 37);
            this.CreateEdge.TabIndex = 1;
            this.CreateEdge.Text = "Create Edge";
            this.CreateEdge.UseVisualStyleBackColor = true;
            this.CreateEdge.Click += new System.EventHandler(this.CreateEdge_Click);
            // 
            // DeleteEdge
            // 
            this.DeleteEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteEdge.Location = new System.Drawing.Point(2, 204);
            this.DeleteEdge.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteEdge.Name = "DeleteEdge";
            this.DeleteEdge.Size = new System.Drawing.Size(84, 37);
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
            this.toolStrip1.Size = new System.Drawing.Size(897, 27);
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
            this.toolStripButton1.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
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
            this.toolStripButton2.Size = new System.Drawing.Size(103, 24);
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
            this.DrawToLeftToolStripButton.Size = new System.Drawing.Size(100, 24);
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
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(777, 29);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(109, 273);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // labelTypeInOutput
            // 
            this.labelTypeInOutput.AutoSize = true;
            this.labelTypeInOutput.Location = new System.Drawing.Point(3, 0);
            this.labelTypeInOutput.Name = "labelTypeInOutput";
            this.labelTypeInOutput.Size = new System.Drawing.Size(26, 13);
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
            this.comboBoxTypeInOutput.Location = new System.Drawing.Point(3, 16);
            this.comboBoxTypeInOutput.Name = "comboBoxTypeInOutput";
            this.comboBoxTypeInOutput.Size = new System.Drawing.Size(96, 21);
            this.comboBoxTypeInOutput.TabIndex = 2;
            this.comboBoxTypeInOutput.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeInOutput_SelectedIndexChanged);
            // 
            // textBoxTypeInOutput
            // 
            this.textBoxTypeInOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTypeInOutput.Location = new System.Drawing.Point(3, 43);
            this.textBoxTypeInOutput.Name = "textBoxTypeInOutput";
            this.textBoxTypeInOutput.ReadOnly = true;
            this.textBoxTypeInOutput.Size = new System.Drawing.Size(96, 20);
            this.textBoxTypeInOutput.TabIndex = 13;
            // 
            // labelNameInOutput
            // 
            this.labelNameInOutput.AutoSize = true;
            this.labelNameInOutput.Location = new System.Drawing.Point(3, 66);
            this.labelNameInOutput.Name = "labelNameInOutput";
            this.labelNameInOutput.Size = new System.Drawing.Size(57, 13);
            this.labelNameInOutput.TabIndex = 0;
            this.labelNameInOutput.Text = "Название";
            // 
            // textBoxNameInOutput
            // 
            this.textBoxNameInOutput.Location = new System.Drawing.Point(3, 82);
            this.textBoxNameInOutput.Name = "textBoxNameInOutput";
            this.textBoxNameInOutput.Size = new System.Drawing.Size(96, 20);
            this.textBoxNameInOutput.TabIndex = 1;
            // 
            // labelDescriptionInOutput
            // 
            this.labelDescriptionInOutput.AutoSize = true;
            this.labelDescriptionInOutput.Location = new System.Drawing.Point(3, 105);
            this.labelDescriptionInOutput.Name = "labelDescriptionInOutput";
            this.labelDescriptionInOutput.Size = new System.Drawing.Size(57, 13);
            this.labelDescriptionInOutput.TabIndex = 6;
            this.labelDescriptionInOutput.Text = "Описание";
            // 
            // textBoxDescriptionInOutput
            // 
            this.textBoxDescriptionInOutput.Location = new System.Drawing.Point(3, 121);
            this.textBoxDescriptionInOutput.Multiline = true;
            this.textBoxDescriptionInOutput.Name = "textBoxDescriptionInOutput";
            this.textBoxDescriptionInOutput.Size = new System.Drawing.Size(96, 36);
            this.textBoxDescriptionInOutput.TabIndex = 7;
            // 
            // labelXInOutput
            // 
            this.labelXInOutput.AutoSize = true;
            this.labelXInOutput.Location = new System.Drawing.Point(3, 160);
            this.labelXInOutput.Name = "labelXInOutput";
            this.labelXInOutput.Size = new System.Drawing.Size(14, 13);
            this.labelXInOutput.TabIndex = 8;
            this.labelXInOutput.Text = "X";
            // 
            // textBoxXInOutput
            // 
            this.textBoxXInOutput.Location = new System.Drawing.Point(3, 176);
            this.textBoxXInOutput.Name = "textBoxXInOutput";
            this.textBoxXInOutput.ReadOnly = true;
            this.textBoxXInOutput.Size = new System.Drawing.Size(96, 20);
            this.textBoxXInOutput.TabIndex = 9;
            // 
            // labelYInOutput
            // 
            this.labelYInOutput.AutoSize = true;
            this.labelYInOutput.Location = new System.Drawing.Point(3, 199);
            this.labelYInOutput.Name = "labelYInOutput";
            this.labelYInOutput.Size = new System.Drawing.Size(14, 13);
            this.labelYInOutput.TabIndex = 10;
            this.labelYInOutput.Text = "Y";
            // 
            // textBoxYInOutput
            // 
            this.textBoxYInOutput.Location = new System.Drawing.Point(3, 215);
            this.textBoxYInOutput.Name = "textBoxYInOutput";
            this.textBoxYInOutput.ReadOnly = true;
            this.textBoxYInOutput.Size = new System.Drawing.Size(96, 20);
            this.textBoxYInOutput.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2, 240);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 31);
            this.button1.TabIndex = 12;
            this.button1.Text = "Continue";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModeStatusLable});
            this.statusStrip1.Location = new System.Drawing.Point(0, 479);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(897, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ModeStatusLable
            // 
            this.ModeStatusLable.Name = "ModeStatusLable";
            this.ModeStatusLable.Size = new System.Drawing.Size(118, 17);
            this.ModeStatusLable.Text = "toolStripStatusLabel1";
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(897, 501);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxTypeInOutput;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ModeStatusLable;
    }
}

