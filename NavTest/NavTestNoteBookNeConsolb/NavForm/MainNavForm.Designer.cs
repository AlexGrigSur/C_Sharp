namespace NavTestNoteBookNeConsolb.NavForm
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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 378);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ChooseLevelComboBox
            // 
            this.ChooseLevelComboBox.FormattingEnabled = true;
            this.ChooseLevelComboBox.Location = new System.Drawing.Point(12, 30);
            this.ChooseLevelComboBox.Name = "ChooseLevelComboBox";
            this.ChooseLevelComboBox.Size = new System.Drawing.Size(121, 24);
            this.ChooseLevelComboBox.TabIndex = 1;
            // 
            // RefreshRouteButton
            // 
            this.RefreshRouteButton.Location = new System.Drawing.Point(161, 12);
            this.RefreshRouteButton.Name = "RefreshRouteButton";
            this.RefreshRouteButton.Size = new System.Drawing.Size(71, 42);
            this.RefreshRouteButton.TabIndex = 2;
            this.RefreshRouteButton.UseVisualStyleBackColor = true;
            // 
            // StartPointButton
            // 
            this.StartPointButton.Location = new System.Drawing.Point(252, 12);
            this.StartPointButton.Name = "StartPointButton";
            this.StartPointButton.Size = new System.Drawing.Size(115, 42);
            this.StartPointButton.TabIndex = 3;
            this.StartPointButton.Text = "Начальная точка";
            this.StartPointButton.UseVisualStyleBackColor = true;
            // 
            // EndPointButton
            // 
            this.EndPointButton.Location = new System.Drawing.Point(373, 12);
            this.EndPointButton.Name = "EndPointButton";
            this.EndPointButton.Size = new System.Drawing.Size(115, 42);
            this.EndPointButton.TabIndex = 4;
            this.EndPointButton.Text = "Конечная точка";
            this.EndPointButton.UseVisualStyleBackColor = true;
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(557, 12);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(153, 42);
            this.ContinueButton.TabIndex = 5;
            this.ContinueButton.Text = "Далее";
            this.ContinueButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Этажи";
            // 
            // MainNavForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.EndPointButton);
            this.Controls.Add(this.StartPointButton);
            this.Controls.Add(this.RefreshRouteButton);
            this.Controls.Add(this.ChooseLevelComboBox);
            this.Controls.Add(this.panel1);
            this.Name = "MainNavForm";
            this.Text = "MainNavForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}