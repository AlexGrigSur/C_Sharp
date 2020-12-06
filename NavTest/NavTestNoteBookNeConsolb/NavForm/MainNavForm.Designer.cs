namespace NavTest//NavTestNoteBookNeConsolb.NavForm
{
    partial class MainNavForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ChooseLevelComboBox = new System.Windows.Forms.ComboBox();
            this.RefreshRouteButton = new System.Windows.Forms.Button();
            this.StartPointButton = new System.Windows.Forms.Button();
            this.EndPointButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Step = new System.Windows.Forms.Label();
            this.CalcButton = new System.Windows.Forms.Button();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(9, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 439);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 41);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ChooseLevelComboBox
            // 
            this.ChooseLevelComboBox.FormattingEnabled = true;
            this.ChooseLevelComboBox.Location = new System.Drawing.Point(10, 20);
            this.ChooseLevelComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChooseLevelComboBox.Name = "ChooseLevelComboBox";
            this.ChooseLevelComboBox.Size = new System.Drawing.Size(124, 21);
            this.ChooseLevelComboBox.TabIndex = 1;
            this.ChooseLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.ChooseLevelComboBox_SelectedIndexChanged);
            // 
            // RefreshRouteButton
            // 
            this.RefreshRouteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshRouteButton.Location = new System.Drawing.Point(92, 53);
            this.RefreshRouteButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RefreshRouteButton.Name = "RefreshRouteButton";
            this.RefreshRouteButton.Size = new System.Drawing.Size(42, 26);
            this.RefreshRouteButton.TabIndex = 2;
            this.RefreshRouteButton.UseVisualStyleBackColor = true;
            this.RefreshRouteButton.Click += new System.EventHandler(this.RefreshRouteButton_Click);
            // 
            // StartPointButton
            // 
            this.StartPointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartPointButton.Location = new System.Drawing.Point(10, 122);
            this.StartPointButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartPointButton.Name = "StartPointButton";
            this.StartPointButton.Size = new System.Drawing.Size(124, 34);
            this.StartPointButton.TabIndex = 3;
            this.StartPointButton.Text = "Выберите точку";
            this.StartPointButton.UseVisualStyleBackColor = true;
            this.StartPointButton.Click += new System.EventHandler(this.StartPointButton_Click);
            // 
            // EndPointButton
            // 
            this.EndPointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EndPointButton.Location = new System.Drawing.Point(10, 193);
            this.EndPointButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EndPointButton.Name = "EndPointButton";
            this.EndPointButton.Size = new System.Drawing.Size(124, 34);
            this.EndPointButton.TabIndex = 4;
            this.EndPointButton.Text = "Выберите точку";
            this.EndPointButton.UseVisualStyleBackColor = true;
            this.EndPointButton.Click += new System.EventHandler(this.EndPointButton_Click);
            // 
            // ContinueButton
            // 
            this.ContinueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ContinueButton.Location = new System.Drawing.Point(73, 325);
            this.ContinueButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(58, 34);
            this.ContinueButton.TabIndex = 5;
            this.ContinueButton.Text = "Далее";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Этажи";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 26);
            this.label3.TabIndex = 8;
            this.label3.Text = "Сбросить \r\nмаршрут";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "Начальная \r\nточка";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 165);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "Конечная\r\nточка";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.Step);
            this.panel3.Controls.Add(this.CalcButton);
            this.panel3.Controls.Add(this.PreviousButton);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.ChooseLevelComboBox);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.RefreshRouteButton);
            this.panel3.Controls.Add(this.StartPointButton);
            this.panel3.Controls.Add(this.EndPointButton);
            this.panel3.Controls.Add(this.ContinueButton);
            this.panel3.Location = new System.Drawing.Point(554, 10);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(136, 439);
            this.panel3.TabIndex = 11;
            // 
            // Step
            // 
            this.Step.AutoSize = true;
            this.Step.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Step.Location = new System.Drawing.Point(10, 304);
            this.Step.Name = "Step";
            this.Step.Size = new System.Drawing.Size(35, 18);
            this.Step.TabIndex = 13;
            this.Step.Text = "Шаг";
            // 
            // CalcButton
            // 
            this.CalcButton.Location = new System.Drawing.Point(10, 254);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(123, 36);
            this.CalcButton.TabIndex = 12;
            this.CalcButton.Text = "Рассчитать маршрут";
            this.CalcButton.UseVisualStyleBackColor = true;
            this.CalcButton.Click += new System.EventHandler(this.CalcButton_Click);
            // 
            // PreviousButton
            // 
            this.PreviousButton.Location = new System.Drawing.Point(10, 325);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(58, 34);
            this.PreviousButton.TabIndex = 11;
            this.PreviousButton.Text = "Назад";
            this.PreviousButton.UseVisualStyleBackColor = true;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // MainNavForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 458);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainNavForm";
            this.Text = "MainNavForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox ChooseLevelComboBox;
        private System.Windows.Forms.Button RefreshRouteButton;
        private System.Windows.Forms.Button StartPointButton;
        private System.Windows.Forms.Button EndPointButton;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button CalcButton;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.Label Step;
    }
}