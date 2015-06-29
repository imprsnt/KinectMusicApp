
namespace WindowsFormsApplication1
{
    partial class TiltWindow
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
            this.verticalTiltValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.verticalTiltValue)).BeginInit();
            this.SuspendLayout();
            // 
            // verticalTiltValue
            // 
            this.verticalTiltValue.Location = new System.Drawing.Point(52, 69);
            this.verticalTiltValue.Maximum = new decimal(new int[] {
            27,
            0,
            0,
            0});
            this.verticalTiltValue.Minimum = new decimal(new int[] {
            27,
            0,
            0,
            -2147483648});
            this.verticalTiltValue.Name = "verticalTiltValue";
            this.verticalTiltValue.Size = new System.Drawing.Size(76, 20);
            this.verticalTiltValue.TabIndex = 0;
            this.verticalTiltValue.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Verical Tilt Orientation";
            // 
            // TiltWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 152);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.verticalTiltValue);
            this.Name = "TiltWindow";
            this.Text = "Tilt Window";
            this.Load += new System.EventHandler(this.TiltWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.verticalTiltValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown verticalTiltValue;
        private System.Windows.Forms.Label label1;

        private musicPlayer _musicPlayer;
        private Microsoft.Kinect.KinectSensor kinectDevice;
    }
}