namespace WindowsFormsApplication1
{
    partial class AdjustDepth
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
            this.minDepthLabel = new System.Windows.Forms.Label();
            this.maxDepthLabel = new System.Windows.Forms.Label();
            this.minTextBox = new System.Windows.Forms.TextBox();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.modifyCheckBox1 = new System.Windows.Forms.CheckBox();
            this.modifyCheckBox2 = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // minDepthLabel
            // 
            this.minDepthLabel.AutoSize = true;
            this.minDepthLabel.Location = new System.Drawing.Point(22, 32);
            this.minDepthLabel.Name = "minDepthLabel";
            this.minDepthLabel.Size = new System.Drawing.Size(61, 13);
            this.minDepthLabel.TabIndex = 0;
            this.minDepthLabel.Text = "min Depth: ";
            // 
            // maxDepthLabel
            // 
            this.maxDepthLabel.AutoSize = true;
            this.maxDepthLabel.Location = new System.Drawing.Point(22, 68);
            this.maxDepthLabel.Name = "maxDepthLabel";
            this.maxDepthLabel.Size = new System.Drawing.Size(64, 13);
            this.maxDepthLabel.TabIndex = 1;
            this.maxDepthLabel.Text = "max Depth: ";
            // 
            // minTextBox
            // 
            this.minTextBox.Enabled = false;
            this.minTextBox.HideSelection = false;
            this.minTextBox.Location = new System.Drawing.Point(89, 29);
            this.minTextBox.Name = "minTextBox";
            this.minTextBox.Size = new System.Drawing.Size(68, 20);
            this.minTextBox.TabIndex = 2;
            // 
            // maxTextBox
            // 
            this.maxTextBox.Enabled = false;
            this.maxTextBox.Location = new System.Drawing.Point(89, 61);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.Size = new System.Drawing.Size(68, 20);
            this.maxTextBox.TabIndex = 3;
            // 
            // modifyCheckBox1
            // 
            this.modifyCheckBox1.AutoSize = true;
            this.modifyCheckBox1.Location = new System.Drawing.Point(163, 32);
            this.modifyCheckBox1.Name = "modifyCheckBox1";
            this.modifyCheckBox1.Size = new System.Drawing.Size(56, 17);
            this.modifyCheckBox1.TabIndex = 4;
            this.modifyCheckBox1.Text = "modify";
            this.modifyCheckBox1.UseVisualStyleBackColor = true;
            this.modifyCheckBox1.CheckedChanged += new System.EventHandler(this.modifyCheckBox1_CheckedChanged);
            // 
            // modifyCheckBox2
            // 
            this.modifyCheckBox2.AutoSize = true;
            this.modifyCheckBox2.Location = new System.Drawing.Point(163, 61);
            this.modifyCheckBox2.Name = "modifyCheckBox2";
            this.modifyCheckBox2.Size = new System.Drawing.Size(56, 17);
            this.modifyCheckBox2.TabIndex = 5;
            this.modifyCheckBox2.Text = "modify";
            this.modifyCheckBox2.UseVisualStyleBackColor = true;
            this.modifyCheckBox2.CheckedChanged += new System.EventHandler(this.modifyCheckBox2_CheckedChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(163, 96);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(49, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // AdjustDepth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 122);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.modifyCheckBox2);
            this.Controls.Add(this.modifyCheckBox1);
            this.Controls.Add(this.maxTextBox);
            this.Controls.Add(this.minTextBox);
            this.Controls.Add(this.maxDepthLabel);
            this.Controls.Add(this.minDepthLabel);
            this.Name = "AdjustDepth";
            this.Text = "AdjustDepth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label minDepthLabel;
        private System.Windows.Forms.Label maxDepthLabel;
        private System.Windows.Forms.TextBox minTextBox;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.CheckBox modifyCheckBox1;
        private System.Windows.Forms.CheckBox modifyCheckBox2;
        private System.Windows.Forms.Button saveButton;

        private musicPlayer _musicPlayer;
    }
}