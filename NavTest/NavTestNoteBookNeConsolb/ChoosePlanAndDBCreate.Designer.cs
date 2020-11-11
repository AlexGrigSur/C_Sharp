namespace NavTestNoteBookNeConsolb
{
    partial class ChoosePlanAndDBCreate
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
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Continue = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNameInOutput = new System.Windows.Forms.TextBox();
            this.DeleteBuilding = new System.Windows.Forms.Button();
            this.DropDBButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(269, 262);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(100, 21);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Рисование";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(269, 290);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(99, 21);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Навигация";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(153, 137);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(638, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(146, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(542, 50);
            this.label1.TabIndex = 3;
            this.label1.Text = "Здравствуйте. \r\nВыберите план, с которым будет производиться работа";
            // 
            // Continue
            // 
            this.Continue.Location = new System.Drawing.Point(269, 319);
            this.Continue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Continue.Name = "Continue";
            this.Continue.Size = new System.Drawing.Size(107, 50);
            this.Continue.TabIndex = 4;
            this.Continue.Text = "Продолжить";
            this.Continue.UseVisualStyleBackColor = true;
            this.Continue.Click += new System.EventHandler(this.Continue_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(148, 165);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Введите название";
            // 
            // textBoxNameInOutput
            // 
            this.textBoxNameInOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNameInOutput.Location = new System.Drawing.Point(154, 194);
            this.textBoxNameInOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNameInOutput.Name = "textBoxNameInOutput";
            this.textBoxNameInOutput.Size = new System.Drawing.Size(638, 22);
            this.textBoxNameInOutput.TabIndex = 1;
            // 
            // DeleteBuilding
            // 
            this.DeleteBuilding.Location = new System.Drawing.Point(12, 126);
            this.DeleteBuilding.Name = "DeleteBuilding";
            this.DeleteBuilding.Size = new System.Drawing.Size(111, 44);
            this.DeleteBuilding.TabIndex = 6;
            this.DeleteBuilding.Text = "Удалить здание";
            this.DeleteBuilding.UseVisualStyleBackColor = true;
            this.DeleteBuilding.Click += new System.EventHandler(this.DeleteBuilding_Click);
            // 
            // DropDBButton
            // 
            this.DropDBButton.Location = new System.Drawing.Point(12, 194);
            this.DropDBButton.Name = "DropDBButton";
            this.DropDBButton.Size = new System.Drawing.Size(111, 48);
            this.DropDBButton.TabIndex = 7;
            this.DropDBButton.Text = "Удалить базу";
            this.DropDBButton.UseVisualStyleBackColor = true;
            this.DropDBButton.Click += new System.EventHandler(this.DropDBButton_Click);
            // 
            // ChoosePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 384);
            this.Controls.Add(this.DropDBButton);
            this.Controls.Add(this.DeleteBuilding);
            this.Controls.Add(this.textBoxNameInOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Continue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ChoosePlan";
            this.Text = "ChoosePlan";
            this.Load += new System.EventHandler(this.ChoosePlan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Continue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNameInOutput;
        private System.Windows.Forms.Button DeleteBuilding;
        private System.Windows.Forms.Button DropDBButton;
    }
}