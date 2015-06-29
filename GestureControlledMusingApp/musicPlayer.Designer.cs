
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Media;
using NAudio;
using NAudio.Wave;




namespace WindowsFormsApplication1
{
    partial class musicPlayer
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
            this.components = new System.ComponentModel.Container();
            this.updateLabelTime = new System.Windows.Forms.Timer(this.components);
            this.mediaMenu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prevToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volDnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.musicTrackBar = new System.Windows.Forms.TrackBar();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.timeRemainingLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mediaFilesListBox = new System.Windows.Forms.ListBox();
            this.CurrentFileNameLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.artistLabel = new System.Windows.Forms.Label();
            this.nameTextLabel = new System.Windows.Forms.Label();
            this.titleTextLabel = new System.Windows.Forms.Label();
            this.genreTextLabel = new System.Windows.Forms.Label();
            this.CurrentFileInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.MediaFileListGroupBox = new System.Windows.Forms.GroupBox();
            this.playButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.timePassedTextLabel = new System.Windows.Forms.Label();
            this.remainingTimeTextLabel = new System.Windows.Forms.Label();
            this.isKinectGestureTrackingEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.mediaMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            this.CurrentFileInfoGroupBox.SuspendLayout();
            this.MediaFileListGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // updateLabelTime
            // 
            this.updateLabelTime.Enabled = true;
            this.updateLabelTime.Tick += new System.EventHandler(this.updateLabelTime_Tick);
            // 
            // mediaMenu
            // 
            this.mediaMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mediaMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.audioToolStripMenuItem});
            this.mediaMenu.Location = new System.Drawing.Point(0, 0);
            this.mediaMenu.Name = "mediaMenu";
            this.mediaMenu.Size = new System.Drawing.Size(551, 24);
            this.mediaMenu.TabIndex = 1;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.playlistToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // playlistToolStripMenuItem
            // 
            this.playlistToolStripMenuItem.Name = "playlistToolStripMenuItem";
            this.playlistToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.playlistToolStripMenuItem.Text = "Remove";
            this.playlistToolStripMenuItem.Click += new System.EventHandler(this.playlistToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // audioToolStripMenuItem
            // 
            this.audioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextToolStripMenuItem,
            this.prevToolStripMenuItem,
            this.volUpToolStripMenuItem,
            this.volDnToolStripMenuItem});
            this.audioToolStripMenuItem.Name = "audioToolStripMenuItem";
            this.audioToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.audioToolStripMenuItem.Text = "Audio";
            this.audioToolStripMenuItem.Click += new System.EventHandler(this.audioToolStripMenuItem_Click);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.nextToolStripMenuItem.Text = "Next";
            this.nextToolStripMenuItem.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
            // 
            // prevToolStripMenuItem
            // 
            this.prevToolStripMenuItem.Name = "prevToolStripMenuItem";
            this.prevToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.prevToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.prevToolStripMenuItem.Text = "Prev";
            this.prevToolStripMenuItem.Click += new System.EventHandler(this.prevToolStripMenuItem_Click);
            // 
            // volUpToolStripMenuItem
            // 
            this.volUpToolStripMenuItem.Name = "volUpToolStripMenuItem";
            this.volUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.volUpToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.volUpToolStripMenuItem.Text = "Vol Up";
            // 
            // volDnToolStripMenuItem
            // 
            this.volDnToolStripMenuItem.Name = "volDnToolStripMenuItem";
            this.volDnToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.volDnToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.volDnToolStripMenuItem.Text = "Vol Dn";
            // 
            // musicTrackBar
            // 
            this.musicTrackBar.Location = new System.Drawing.Point(30, 223);
            this.musicTrackBar.Name = "musicTrackBar";
            this.musicTrackBar.Size = new System.Drawing.Size(247, 45);
            this.musicTrackBar.TabIndex = 2;
            this.musicTrackBar.Scroll += new System.EventHandler(this.musicTrackBar_Scroll);
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.Location = new System.Drawing.Point(30, 297);
            this.volumeTrackBar.Maximum = 20;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(104, 45);
            this.volumeTrackBar.TabIndex = 3;
            this.volumeTrackBar.Value = 5;
            this.volumeTrackBar.Scroll += new System.EventHandler(this.volumeTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "volume";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(27, 194);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(29, 13);
            this.timeLabel.TabIndex = 5;
            this.timeLabel.Text = "time:";
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(360, 352);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(53, 23);
            this.prevButton.TabIndex = 6;
            this.prevButton.Text = "prev";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(448, 352);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(53, 23);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // timeRemainingLabel
            // 
            this.timeRemainingLabel.AutoSize = true;
            this.timeRemainingLabel.Location = new System.Drawing.Point(201, 194);
            this.timeRemainingLabel.Name = "timeRemainingLabel";
            this.timeRemainingLabel.Size = new System.Drawing.Size(28, 13);
            this.timeRemainingLabel.TabIndex = 8;
            this.timeRemainingLabel.Text = "end:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 388);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(551, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mediaFilesListBox
            // 
            this.mediaFilesListBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mediaFilesListBox.DisplayMember = "mediaFileName";
            this.mediaFilesListBox.FormattingEnabled = true;
            this.mediaFilesListBox.HorizontalScrollbar = true;
            this.mediaFilesListBox.Location = new System.Drawing.Point(6, 19);
            this.mediaFilesListBox.Name = "mediaFilesListBox";
            this.mediaFilesListBox.Size = new System.Drawing.Size(175, 264);
            this.mediaFilesListBox.TabIndex = 10;
            this.mediaFilesListBox.ValueMember = "mediaAbsoluteFileLocation";
            this.mediaFilesListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // CurrentFileNameLabel
            // 
            this.CurrentFileNameLabel.AutoSize = true;
            this.CurrentFileNameLabel.Location = new System.Drawing.Point(34, 39);
            this.CurrentFileNameLabel.Name = "CurrentFileNameLabel";
            this.CurrentFileNameLabel.Size = new System.Drawing.Size(39, 13);
            this.CurrentFileNameLabel.TabIndex = 12;
            this.CurrentFileNameLabel.Text = "name: ";
            this.CurrentFileNameLabel.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(34, 72);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(29, 13);
            this.titleLabel.TabIndex = 13;
            this.titleLabel.Text = "title: ";
            this.titleLabel.Click += new System.EventHandler(this.label2_Click_2);
            // 
            // artistLabel
            // 
            this.artistLabel.AutoSize = true;
            this.artistLabel.Location = new System.Drawing.Point(33, 101);
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.Size = new System.Drawing.Size(35, 13);
            this.artistLabel.TabIndex = 14;
            this.artistLabel.Text = "artist: ";
            // 
            // nameTextLabel
            // 
            this.nameTextLabel.AutoSize = true;
            this.nameTextLabel.Location = new System.Drawing.Point(94, 39);
            this.nameTextLabel.Name = "nameTextLabel";
            this.nameTextLabel.Size = new System.Drawing.Size(0, 13);
            this.nameTextLabel.TabIndex = 15;
            // 
            // titleTextLabel
            // 
            this.titleTextLabel.AutoSize = true;
            this.titleTextLabel.Location = new System.Drawing.Point(94, 73);
            this.titleTextLabel.Name = "titleTextLabel";
            this.titleTextLabel.Size = new System.Drawing.Size(0, 13);
            this.titleTextLabel.TabIndex = 16;
            // 
            // genreTextLabel
            // 
            this.genreTextLabel.AutoSize = true;
            this.genreTextLabel.Location = new System.Drawing.Point(97, 101);
            this.genreTextLabel.Name = "genreTextLabel";
            this.genreTextLabel.Size = new System.Drawing.Size(0, 13);
            this.genreTextLabel.TabIndex = 17;
            // 
            // CurrentFileInfoGroupBox
            // 
            this.CurrentFileInfoGroupBox.Controls.Add(this.CurrentFileNameLabel);
            this.CurrentFileInfoGroupBox.Controls.Add(this.genreTextLabel);
            this.CurrentFileInfoGroupBox.Controls.Add(this.titleLabel);
            this.CurrentFileInfoGroupBox.Controls.Add(this.titleTextLabel);
            this.CurrentFileInfoGroupBox.Controls.Add(this.artistLabel);
            this.CurrentFileInfoGroupBox.Controls.Add(this.nameTextLabel);
            this.CurrentFileInfoGroupBox.Location = new System.Drawing.Point(30, 48);
            this.CurrentFileInfoGroupBox.Name = "CurrentFileInfoGroupBox";
            this.CurrentFileInfoGroupBox.Size = new System.Drawing.Size(247, 128);
            this.CurrentFileInfoGroupBox.TabIndex = 18;
            this.CurrentFileInfoGroupBox.TabStop = false;
            this.CurrentFileInfoGroupBox.Text = "Current File Info";
            // 
            // MediaFileListGroupBox
            // 
            this.MediaFileListGroupBox.Controls.Add(this.mediaFilesListBox);
            this.MediaFileListGroupBox.Location = new System.Drawing.Point(320, 48);
            this.MediaFileListGroupBox.Name = "MediaFileListGroupBox";
            this.MediaFileListGroupBox.Size = new System.Drawing.Size(187, 294);
            this.MediaFileListGroupBox.TabIndex = 19;
            this.MediaFileListGroupBox.TabStop = false;
            this.MediaFileListGroupBox.Text = "MediaFileList";
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(188, 352);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(53, 23);
            this.playButton.TabIndex = 20;
            this.playButton.Text = "play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(273, 352);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(53, 23);
            this.pauseButton.TabIndex = 21;
            this.pauseButton.Text = "pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // timePassedTextLabel
            // 
            this.timePassedTextLabel.AutoSize = true;
            this.timePassedTextLabel.Location = new System.Drawing.Point(62, 194);
            this.timePassedTextLabel.Name = "timePassedTextLabel";
            this.timePassedTextLabel.Size = new System.Drawing.Size(0, 13);
            this.timePassedTextLabel.TabIndex = 22;
            // 
            // remainingTimeTextLabel
            // 
            this.remainingTimeTextLabel.AutoSize = true;
            this.remainingTimeTextLabel.Location = new System.Drawing.Point(234, 194);
            this.remainingTimeTextLabel.Name = "remainingTimeTextLabel";
            this.remainingTimeTextLabel.Size = new System.Drawing.Size(0, 13);
            this.remainingTimeTextLabel.TabIndex = 23;
            // 
            // isKinectGestureTrackingEnabledCheckBox
            // 
            this.isKinectGestureTrackingEnabledCheckBox.AutoSize = true;
            this.isKinectGestureTrackingEnabledCheckBox.Location = new System.Drawing.Point(13, 392);
            this.isKinectGestureTrackingEnabledCheckBox.Name = "isKinectGestureTrackingEnabledCheckBox";
            this.isKinectGestureTrackingEnabledCheckBox.Size = new System.Drawing.Size(177, 17);
            this.isKinectGestureTrackingEnabledCheckBox.TabIndex = 24;
            this.isKinectGestureTrackingEnabledCheckBox.Text = "Enable Kinect Gesture Tracking";
            this.isKinectGestureTrackingEnabledCheckBox.UseVisualStyleBackColor = true;
            this.isKinectGestureTrackingEnabledCheckBox.CheckedChanged += new System.EventHandler(this.isKinectGestureTrackingEnabledCheckBox_CheckedChanged);
            // 
            // musicPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(551, 410);
            this.Controls.Add(this.isKinectGestureTrackingEnabledCheckBox);
            this.Controls.Add(this.remainingTimeTextLabel);
            this.Controls.Add(this.timePassedTextLabel);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.MediaFileListGroupBox);
            this.Controls.Add(this.CurrentFileInfoGroupBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.timeRemainingLabel);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.volumeTrackBar);
            this.Controls.Add(this.musicTrackBar);
            this.Controls.Add(this.mediaMenu);
            this.MainMenuStrip = this.mediaMenu;
            this.Name = "musicPlayer";
            this.Text = "HCI 580-Kinect Gesture recognition";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.musicPlayer_FormClosed);
            this.Load += new System.EventHandler(this.musicPlayer_Load);
            this.mediaMenu.ResumeLayout(false);
            this.mediaMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            this.CurrentFileInfoGroupBox.ResumeLayout(false);
            this.CurrentFileInfoGroupBox.PerformLayout();
            this.MediaFileListGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        #region _Kinect Variables

        private OpenFileDialog fileExplorer;
        private System.ComponentModel.BindingList<MediaClass> mediaList = new BindingList<MediaClass>();
        
        
        private WaveFileReader waveFileReader;
        private Mp3FileReader mp3FileReader;
        private BlockAlignReductionStream stream;
        private WaveOut wavOutput ;
        private MediaClass currentPlayingFile;
        private Timer updateLabelTime;
        private KinectTrackingWindow kinectWindowForm;
        


        #endregion

        private System.Windows.Forms.MenuStrip mediaMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        public System.Windows.Forms.TrackBar musicTrackBar;
        public System.Windows.Forms.TrackBar volumeTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timeLabel;
        public System.Windows.Forms.Button prevButton;
        public System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label timeRemainingLabel;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prevToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ListBox mediaFilesListBox;
        private Label CurrentFileNameLabel;
        private Label titleLabel;
        private Label artistLabel;
        private Label nameTextLabel;
        private Label titleTextLabel;
        private Label genreTextLabel;
        private GroupBox CurrentFileInfoGroupBox;
        private GroupBox MediaFileListGroupBox;
        public Button playButton;
        public Button pauseButton;
        private Label timePassedTextLabel;
        private Label remainingTimeTextLabel;
        private ToolStripMenuItem volUpToolStripMenuItem;
        private ToolStripMenuItem volDnToolStripMenuItem;
        public CheckBox isKinectGestureTrackingEnabledCheckBox;
    }
}

