using Microsoft.Kinect;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
namespace WindowsFormsApplication1
{
    partial class KinectTrackingWindow
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.depthPictureBox = new System.Windows.Forms.PictureBox();
            this.skeletonPanel = new System.Windows.Forms.Panel();
            this.colorPictureBox = new System.Windows.Forms.PictureBox();
            this.quadrantLabel = new System.Windows.Forms.Label();
            this.kinectWindowMenuStrip = new System.Windows.Forms.MenuStrip();
            this.kinectWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deactivateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depthStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deactivateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.skeletonStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activateToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deactivateToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.informationWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorStreamDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depthStreamDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.skeletonStreamDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReleaseResourceButton = new System.Windows.Forms.Button();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primarySideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.positionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sittingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adjustTiltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adjustDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kinectSensorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allStreamDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.depthPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).BeginInit();
            this.kinectWindowMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.58974F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.41026F));
            this.tableLayoutPanel1.Controls.Add(this.depthPictureBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.skeletonPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.colorPictureBox, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.75751F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.24249F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(788, 868);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // depthPictureBox
            // 
            this.depthPictureBox.Location = new System.Drawing.Point(386, 5);
            this.depthPictureBox.Name = "depthPictureBox";
            this.depthPictureBox.Size = new System.Drawing.Size(396, 292);
            this.depthPictureBox.TabIndex = 1;
            this.depthPictureBox.TabStop = false;
            // 
            // skeletonPanel
            // 
            this.skeletonPanel.Location = new System.Drawing.Point(5, 306);
            this.skeletonPanel.Name = "skeletonPanel";
            this.skeletonPanel.Size = new System.Drawing.Size(373, 557);
            this.skeletonPanel.TabIndex = 2;
            // 
            // colorPictureBox
            // 
            this.colorPictureBox.Location = new System.Drawing.Point(5, 5);
            this.colorPictureBox.Name = "colorPictureBox";
            this.colorPictureBox.Size = new System.Drawing.Size(373, 292);
            this.colorPictureBox.TabIndex = 0;
            this.colorPictureBox.TabStop = false;
            // 
            // quadrantLabel
            // 
            this.quadrantLabel.AutoSize = true;
            this.quadrantLabel.Location = new System.Drawing.Point(3, 905);
            this.quadrantLabel.Name = "quadrantLabel";
            this.quadrantLabel.Size = new System.Drawing.Size(385, 13);
            this.quadrantLabel.TabIndex = 1;
            this.quadrantLabel.Text = "TopLeft - Color Stream | TopRight - Depth Stream | BottomLeft - Skeleton Stream";
            // 
            // kinectWindowMenuStrip
            // 
            this.kinectWindowMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kinectWindowToolStripMenuItem,
            this.userToolStripMenuItem,
            this.displayToolStripMenuItem,
            this.kinectSensorToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.kinectWindowMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.kinectWindowMenuStrip.Name = "kinectWindowMenuStrip";
            this.kinectWindowMenuStrip.Size = new System.Drawing.Size(788, 24);
            this.kinectWindowMenuStrip.TabIndex = 2;
            this.kinectWindowMenuStrip.Text = "kinect window ";
            // 
            // kinectWindowToolStripMenuItem
            // 
            this.kinectWindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorStreamToolStripMenuItem,
            this.depthStreamToolStripMenuItem,
            this.skeletonStreamToolStripMenuItem,
            this.informationWindowToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.kinectWindowToolStripMenuItem.Name = "kinectWindowToolStripMenuItem";
            this.kinectWindowToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.kinectWindowToolStripMenuItem.Text = "Kinect Window";
            // 
            // colorStreamToolStripMenuItem
            // 
            this.colorStreamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activateToolStripMenuItem,
            this.deactivateToolStripMenuItem});
            this.colorStreamToolStripMenuItem.Name = "colorStreamToolStripMenuItem";
            this.colorStreamToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.colorStreamToolStripMenuItem.Text = "Color Stream";
            // 
            // activateToolStripMenuItem
            // 
            this.activateToolStripMenuItem.Name = "activateToolStripMenuItem";
            this.activateToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.activateToolStripMenuItem.Text = "Activate";
            // 
            // deactivateToolStripMenuItem
            // 
            this.deactivateToolStripMenuItem.Name = "deactivateToolStripMenuItem";
            this.deactivateToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.deactivateToolStripMenuItem.Text = "Deactivate";
            // 
            // depthStreamToolStripMenuItem
            // 
            this.depthStreamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activateToolStripMenuItem1,
            this.deactivateToolStripMenuItem1});
            this.depthStreamToolStripMenuItem.Name = "depthStreamToolStripMenuItem";
            this.depthStreamToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.depthStreamToolStripMenuItem.Text = "Depth Stream";
            // 
            // activateToolStripMenuItem1
            // 
            this.activateToolStripMenuItem1.Name = "activateToolStripMenuItem1";
            this.activateToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.activateToolStripMenuItem1.Text = "Activate";
            // 
            // deactivateToolStripMenuItem1
            // 
            this.deactivateToolStripMenuItem1.Name = "deactivateToolStripMenuItem1";
            this.deactivateToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.deactivateToolStripMenuItem1.Text = "Deactivate";
            // 
            // skeletonStreamToolStripMenuItem
            // 
            this.skeletonStreamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activateToolStripMenuItem2,
            this.deactivateToolStripMenuItem2});
            this.skeletonStreamToolStripMenuItem.Name = "skeletonStreamToolStripMenuItem";
            this.skeletonStreamToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.skeletonStreamToolStripMenuItem.Text = "Skeleton Stream";
            // 
            // activateToolStripMenuItem2
            // 
            this.activateToolStripMenuItem2.Name = "activateToolStripMenuItem2";
            this.activateToolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
            this.activateToolStripMenuItem2.Text = "Activate";
            // 
            // deactivateToolStripMenuItem2
            // 
            this.deactivateToolStripMenuItem2.Name = "deactivateToolStripMenuItem2";
            this.deactivateToolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
            this.deactivateToolStripMenuItem2.Text = "Deactivate";
            // 
            // informationWindowToolStripMenuItem
            // 
            this.informationWindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.informationWindowToolStripMenuItem.Name = "informationWindowToolStripMenuItem";
            this.informationWindowToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.informationWindowToolStripMenuItem.Text = "Information Window";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorStreamDisplayToolStripMenuItem,
            this.depthStreamDisplayToolStripMenuItem,
            this.skeletonStreamDisplayToolStripMenuItem,
            this.allStreamDisplayToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // colorStreamDisplayToolStripMenuItem
            // 
            this.colorStreamDisplayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.colorStreamDisplayToolStripMenuItem.Name = "colorStreamDisplayToolStripMenuItem";
            this.colorStreamDisplayToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.colorStreamDisplayToolStripMenuItem.Text = "Color Stream Display";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.onToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.offToolStripMenuItem_Click);
            // 
            // depthStreamDisplayToolStripMenuItem
            // 
            this.depthStreamDisplayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem1,
            this.offToolStripMenuItem1});
            this.depthStreamDisplayToolStripMenuItem.Name = "depthStreamDisplayToolStripMenuItem";
            this.depthStreamDisplayToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.depthStreamDisplayToolStripMenuItem.Text = "Depth Stream Display";
            // 
            // onToolStripMenuItem1
            // 
            this.onToolStripMenuItem1.Name = "onToolStripMenuItem1";
            this.onToolStripMenuItem1.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem1.Text = "On";
            this.onToolStripMenuItem1.Click += new System.EventHandler(this.onToolStripMenuItem1_Click);
            // 
            // offToolStripMenuItem1
            // 
            this.offToolStripMenuItem1.Name = "offToolStripMenuItem1";
            this.offToolStripMenuItem1.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem1.Text = "Off";
            this.offToolStripMenuItem1.Click += new System.EventHandler(this.offToolStripMenuItem1_Click);
            // 
            // skeletonStreamDisplayToolStripMenuItem
            // 
            this.skeletonStreamDisplayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem2,
            this.offToolStripMenuItem2});
            this.skeletonStreamDisplayToolStripMenuItem.Name = "skeletonStreamDisplayToolStripMenuItem";
            this.skeletonStreamDisplayToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.skeletonStreamDisplayToolStripMenuItem.Text = "Skeleton Stream Display";
            // 
            // onToolStripMenuItem2
            // 
            this.onToolStripMenuItem2.Name = "onToolStripMenuItem2";
            this.onToolStripMenuItem2.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem2.Text = "On";
            this.onToolStripMenuItem2.Click += new System.EventHandler(this.onToolStripMenuItem2_Click);
            // 
            // offToolStripMenuItem2
            // 
            this.offToolStripMenuItem2.Name = "offToolStripMenuItem2";
            this.offToolStripMenuItem2.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem2.Text = "Off";
            this.offToolStripMenuItem2.Click += new System.EventHandler(this.offToolStripMenuItem2_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // ReleaseResourceButton
            // 
            this.ReleaseResourceButton.Location = new System.Drawing.Point(816, 900);
            this.ReleaseResourceButton.Name = "ReleaseResourceButton";
            this.ReleaseResourceButton.Size = new System.Drawing.Size(106, 23);
            this.ReleaseResourceButton.TabIndex = 3;
            this.ReleaseResourceButton.Text = "ReleaseResouce";
            this.ReleaseResourceButton.UseVisualStyleBackColor = true;
            this.ReleaseResourceButton.Click += new System.EventHandler(this.ReleaseResourceButton_Click);
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.primarySideToolStripMenuItem1,
            this.positionToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.userToolStripMenuItem.Text = "User";
            // 
            // primarySideToolStripMenuItem1
            // 
            this.primarySideToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftToolStripMenuItem1,
            this.rightToolStripMenuItem1});
            this.primarySideToolStripMenuItem1.Name = "primarySideToolStripMenuItem1";
            this.primarySideToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.primarySideToolStripMenuItem1.Text = "Primary Side";
            // 
            // leftToolStripMenuItem1
            // 
            this.leftToolStripMenuItem1.Name = "leftToolStripMenuItem1";
            this.leftToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.leftToolStripMenuItem1.Text = "Left";
            // 
            // rightToolStripMenuItem1
            // 
            this.rightToolStripMenuItem1.Name = "rightToolStripMenuItem1";
            this.rightToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.rightToolStripMenuItem1.Text = "Right";
            // 
            // positionToolStripMenuItem
            // 
            this.positionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.standingToolStripMenuItem,
            this.sittingToolStripMenuItem});
            this.positionToolStripMenuItem.Name = "positionToolStripMenuItem";
            this.positionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.positionToolStripMenuItem.Text = "Position";
            // 
            // standingToolStripMenuItem
            // 
            this.standingToolStripMenuItem.Name = "standingToolStripMenuItem";
            this.standingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.standingToolStripMenuItem.Text = "Standing";
            this.standingToolStripMenuItem.Click += new System.EventHandler(this.standingToolStripMenuItem_Click);
            // 
            // sittingToolStripMenuItem
            // 
            this.sittingToolStripMenuItem.Name = "sittingToolStripMenuItem";
            this.sittingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sittingToolStripMenuItem.Text = "Sitting";
            // 
            // adjustTiltToolStripMenuItem
            // 
            this.adjustTiltToolStripMenuItem.Name = "adjustTiltToolStripMenuItem";
            this.adjustTiltToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.adjustTiltToolStripMenuItem.Text = "Adjust Tilt";
            this.adjustTiltToolStripMenuItem.Click += new System.EventHandler(this.adjustTiltToolStripMenuItem_Click);
            // 
            // adjustDepthToolStripMenuItem
            // 
            this.adjustDepthToolStripMenuItem.Name = "adjustDepthToolStripMenuItem";
            this.adjustDepthToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.adjustDepthToolStripMenuItem.Text = "Adjust Depth";
            this.adjustDepthToolStripMenuItem.Click += new System.EventHandler(this.adjustDepthToolStripMenuItem_Click);
            // 
            // kinectSensorToolStripMenuItem
            // 
            this.kinectSensorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adjustTiltToolStripMenuItem,
            this.adjustDepthToolStripMenuItem});
            this.kinectSensorToolStripMenuItem.Name = "kinectSensorToolStripMenuItem";
            this.kinectSensorToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.kinectSensorToolStripMenuItem.Text = "Kinect Sensor";
            // 
            // allStreamDisplayToolStripMenuItem
            // 
            this.allStreamDisplayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem3,
            this.offToolStripMenuItem3});
            this.allStreamDisplayToolStripMenuItem.Name = "allStreamDisplayToolStripMenuItem";
            this.allStreamDisplayToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.allStreamDisplayToolStripMenuItem.Text = "All Stream Display";
            // 
            // onToolStripMenuItem3
            // 
            this.onToolStripMenuItem3.Name = "onToolStripMenuItem3";
            this.onToolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.onToolStripMenuItem3.Text = "On";
            // 
            // offToolStripMenuItem3
            // 
            this.offToolStripMenuItem3.Name = "offToolStripMenuItem3";
            this.offToolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.offToolStripMenuItem3.Text = "Off";
            // 
            // KinectTrackingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 927);
            this.Controls.Add(this.ReleaseResourceButton);
            this.Controls.Add(this.quadrantLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.kinectWindowMenuStrip);
            this.MainMenuStrip = this.kinectWindowMenuStrip;
            this.Name = "KinectTrackingWindow";
            this.Text = "KinectTrackingWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KinectTrackingWindow_FormClosed);
            this.Load += new System.EventHandler(this.KinectTrackingWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.depthPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).EndInit();
            this.kinectWindowMenuStrip.ResumeLayout(false);
            this.kinectWindowMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox depthPictureBox;
        private System.Windows.Forms.PictureBox colorPictureBox;
        private System.Windows.Forms.Label quadrantLabel;
        private System.Windows.Forms.MenuStrip kinectWindowMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem kinectWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deactivateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depthStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deactivateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem skeletonStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activateToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deactivateToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem informationWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button ReleaseResourceButton;
        private ToolStripMenuItem displayToolStripMenuItem;
        private ToolStripMenuItem colorStreamDisplayToolStripMenuItem;
        private ToolStripMenuItem onToolStripMenuItem;
        private ToolStripMenuItem offToolStripMenuItem;
        private ToolStripMenuItem depthStreamDisplayToolStripMenuItem;
        private ToolStripMenuItem onToolStripMenuItem1;
        private ToolStripMenuItem offToolStripMenuItem1;
        private ToolStripMenuItem skeletonStreamDisplayToolStripMenuItem;
        private ToolStripMenuItem onToolStripMenuItem2;
        private ToolStripMenuItem offToolStripMenuItem2;
        private Panel skeletonPanel;


        private  readonly int   ENABLE_DEPTH_SENSOR     = 1;
        private  readonly int   ENABLE_COLOR_SENSOR     = 2;
        private  readonly int   ENABLE_SKELETON_SENSOR  = 4;

        public KinectSensor _kinectDevice;

        private WriteableBitmap colorImageBitmap;
        private Int32Rect colorImageBitmapRect;
        private int colorImageBitmapStride;
        private byte[] colorPixelData;

        private WriteableBitmap depthImageBitmap;
        private Int32Rect depthImageBitmapRect;
        private int depthImageBitmapStride;
        private DepthImagePixel[] depthPixelData;
        private byte[] depthColorPixelData;
        int minDepth;
        int maxDepth;

        private WriteableBitmap skeletonImageBitmap;
        private Int32Rect skeletonImageBitmapRect;
        private int skeletonImageBitmapStride;
        private byte[] skeletonPixelData;
        private JointType[] joints;
        private Skeleton[] frameSkeletons;
        private System.Windows.Media.Brush[] skeletonBrushes;

        private musicPlayer _musicPlayer;
        private ElementHost skeletonElementHost;
        private System.Windows.Controls.Canvas skeletonCanvas;

        private GestureEngine mediaAppGestureEngine;

        private bool displayColorStreamVideo;
        private bool displayDepthStreamVideo;
        private bool displaySkeletonStreamVideo;

        private readonly System.Collections.Generic.Dictionary<JointType, System.Windows.Media.Brush> jointColors =
                         new System.Collections.Generic.Dictionary<JointType, System.Windows.Media.Brush>()
                         {
                             {JointType.HipCenter, new SolidColorBrush(Color.FromRgb(169, 176, 155))},
                             {JointType.Spine, new SolidColorBrush(Color.FromRgb(169, 176, 155))},
                             {JointType.ShoulderCenter, new SolidColorBrush(Color.FromRgb(168, 230, 29))},
                             {JointType.Head, new SolidColorBrush(Color.FromRgb(200, 0, 0))},
                             {JointType.ShoulderLeft, new SolidColorBrush(Color.FromRgb(79, 84, 33))},
                             {JointType.ElbowLeft, new SolidColorBrush(Color.FromRgb(84, 33, 42))},
                             {JointType.WristLeft, new SolidColorBrush(Color.FromRgb(255, 126, 0))},
                             {JointType.HandLeft, new SolidColorBrush(Color.FromRgb(215, 86, 0))},
                             {JointType.ShoulderRight, new SolidColorBrush(Color.FromRgb(33, 79,  84))},
                             {JointType.ElbowRight, new SolidColorBrush(Color.FromRgb(33, 33, 84))},
                             {JointType.WristRight, new SolidColorBrush(Color.FromRgb(77, 109, 243))},
                             {JointType.HandRight, new SolidColorBrush(Color.FromRgb(37,  69, 243))},
                             {JointType.HipLeft, new SolidColorBrush(Color.FromRgb(77, 109, 243))},
                             {JointType.KneeLeft, new SolidColorBrush(Color.FromRgb(69, 33, 84))},
                             {JointType.AnkleLeft, new SolidColorBrush(Color.FromRgb(229, 170, 122))},
                             {JointType.FootLeft, new SolidColorBrush(Color.FromRgb(255, 126, 0))},
                             {JointType.HipRight, new SolidColorBrush(Color.FromRgb(181, 165, 213))},
                             {JointType.KneeRight, new SolidColorBrush(Color.FromRgb(71, 222, 76))},
                             {JointType.AnkleRight, new SolidColorBrush(Color.FromRgb(245, 228, 156))},
                             {JointType.FootRight, new SolidColorBrush(Color.FromRgb(77, 109, 243))}
                         };
        private ToolStripMenuItem userToolStripMenuItem;
        private ToolStripMenuItem primarySideToolStripMenuItem1;
        private ToolStripMenuItem leftToolStripMenuItem1;
        private ToolStripMenuItem rightToolStripMenuItem1;
        private ToolStripMenuItem positionToolStripMenuItem;
        private ToolStripMenuItem standingToolStripMenuItem;
        private ToolStripMenuItem sittingToolStripMenuItem;
        private ToolStripMenuItem kinectSensorToolStripMenuItem;
        private ToolStripMenuItem adjustTiltToolStripMenuItem;
        private ToolStripMenuItem adjustDepthToolStripMenuItem;
        private ToolStripMenuItem allStreamDisplayToolStripMenuItem;
        private ToolStripMenuItem onToolStripMenuItem3;
        private ToolStripMenuItem offToolStripMenuItem3;
        
    }
}