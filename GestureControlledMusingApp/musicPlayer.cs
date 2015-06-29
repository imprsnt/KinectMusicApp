using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;


namespace WindowsFormsApplication1
{
    public partial class musicPlayer : Form
    {
        public musicPlayer()
        {
            InitializeComponent();
            //
            // kinect updates
            //
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.mediaFilesListBox.DataSource = this.mediaList;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            int index = mediaList.IndexOf(currentPlayingFile);
            if (index < 0)
                return;
            Console.WriteLine(index);
            if (index == 0)
            {
                index = mediaList.Count;
            }
            mediaFilesListBox.SelectedIndex = index - 1;
            playButton.PerformClick();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            int index = mediaList.IndexOf(currentPlayingFile);
            if (index < 0)
                return;
            Console.WriteLine(index);
            if (index + 1 == mediaList.Count)
            {
                index = -1;
            }
            mediaFilesListBox.SelectedIndex = index + 1;
            playButton.PerformClick();
        }

        public void musicTrackBar_PerformScroll()
        {
            musicTrackBar_Scroll(null, null);
        }

        private void musicTrackBar_Scroll(object sender, EventArgs e)
        {
            if (wavOutput != null)
            {
                if (currentPlayingFile == null)
                    return;
                string extension = getFileExtension(currentPlayingFile.mediaAbsoluteFileLocation);
                switch (extension)
                {
                    case "mp3":
                        mp3FileReader.CurrentTime = new TimeSpan(0, musicTrackBar.Value / 60, musicTrackBar.Value % 60);
                        break;
                    case "wav":
                        waveFileReader.CurrentTime = new TimeSpan(0, musicTrackBar.Value / 60, musicTrackBar.Value % 60);
                        break;

                }
            }
        }

        private void volumeTrackBar_Scroll(object sender, EventArgs e)
        {
            if(wavOutput != null)
                wavOutput.Volume = (float)(volumeTrackBar.Value * 0.05);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (this.fileExplorer = new OpenFileDialog())
            {
                fileExplorer.Filter = " Mp3 Files(*.mp3)| *.mp3|Wave Files(*.wav)|*.wav|All Files(*.*)|*.*";
                fileExplorer.Title = "select a media file";
                fileExplorer.Multiselect = true;
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    string[] filepaths = fileExplorer.FileNames;
                    
                    foreach (string filepath in filepaths)
                        mediaList.Add(new MediaClass() { mediaFileName  =   this.getFileName(filepath) , mediaAbsoluteFileLocation = filepath});

                }

            }
        }

        private void playlistToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
            this.Close();
        }

        private void audioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextButton.PerformClick();
        }

        private void prevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prevButton.PerformClick();
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rewindToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void musicPlayer_Load(object sender, EventArgs e)
        {

        }

        private string getFileName(string filePath)
        {
             return filePath.Substring(filePath.LastIndexOf("\\")  + 1);
        }

        private string getFileExtension(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf(".") + 1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            playButton.PerformClick();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void _KinectDispose()
        {

            isKinectGestureTrackingEnabledCheckBox.Checked = false;

            if (wavOutput != null)
            {
                if (wavOutput.PlaybackState != PlaybackState.Stopped)
                    wavOutput.Stop();
                wavOutput.Dispose();
                wavOutput = null;
            }
            if (waveFileReader != null)
            {
                waveFileReader.Dispose();
                waveFileReader = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
            if (updateLabelTime != null)
            {
                updateLabelTime.Tick -= updateLabelTime_Tick;
                updateLabelTime.Dispose();
                updateLabelTime = null;
            }   
        }

        private void setDurationLabels(TimeSpan currentDuration , TimeSpan totalDuration)
        {
            timePassedTextLabel.Text = ((currentDuration.Minutes < 10) ? "0" + currentDuration.Minutes.ToString() : currentDuration.Minutes.ToString()) +
                                           ":" +
                                       ((currentDuration.Seconds < 10) ? "0" + currentDuration.Seconds.ToString() : currentDuration.Seconds.ToString());

            remainingTimeTextLabel.Text = ((totalDuration.Minutes < 10) ? "0" + totalDuration.Minutes.ToString() : totalDuration.Minutes.ToString()) +
                                           ":" +
                                          ((totalDuration.Seconds < 10) ? "0" + totalDuration.Seconds.ToString() : totalDuration.Seconds.ToString());
            setMediaTrackBarLocation(currentDuration,totalDuration);
        }

        private void setMediaTrackBarLocation(TimeSpan currentDuration , TimeSpan totalDuration)
        {
            //int width = this.musicTrackBar.Width;
            
            int currentTimeInSecs = (currentDuration.Minutes * 60) + currentDuration.Seconds;
            int totalTimeInSecs = (totalDuration.Minutes * 60) + totalDuration.Seconds;
            musicTrackBar.Maximum = totalTimeInSecs;
            musicTrackBar.Value = currentTimeInSecs;
        }


        private void setVolume()
        {
            wavOutput.Volume = (float)(volumeTrackBar.Value * .05);
        }

        private void playMP3(string filename)
        {

            if (wavOutput != null && wavOutput.PlaybackState != PlaybackState.Stopped)
                wavOutput.Stop();
            mp3FileReader = new Mp3FileReader(filename);
            setDurationLabels(mp3FileReader.CurrentTime , mp3FileReader.TotalTime);
            WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3FileReader);
            stream = new BlockAlignReductionStream(pcm);
            wavOutput = new WaveOut();
            wavOutput.Init(stream);
            wavOutput.Play();
            

            
        }

        private void playWAV(string filename)
        {
            if (wavOutput != null && wavOutput.PlaybackState != PlaybackState.Stopped)
                wavOutput.Stop();
            waveFileReader = new NAudio.Wave.WaveFileReader(filename);
            setDurationLabels(waveFileReader.CurrentTime , waveFileReader.TotalTime);
            wavOutput = new WaveOut();
            wavOutput.Init(new WaveChannel32(waveFileReader));
            wavOutput.Play();

        }

        private void playButton_Click(object sender, EventArgs e)
        {
            try
            {
                currentPlayingFile = mediaFilesListBox.SelectedItem as MediaClass;
                string extension = getFileExtension((mediaFilesListBox.SelectedItem as MediaClass).mediaAbsoluteFileLocation);
                switch (extension)
                {
                    case "mp3":
                        playMP3((mediaFilesListBox.SelectedItem as MediaClass).mediaAbsoluteFileLocation);
                        break;
                    case "wav":
                        playWAV((mediaFilesListBox.SelectedItem as MediaClass).mediaAbsoluteFileLocation);
                        break;
                    default:
                        MessageBox.Show("Incorrect file format, Please select *.mp3 / *.wav","Error");
                        break;
                }
            }
            catch (Exception exp) 
            {
                MessageBox.Show("Error! Unable to read the file format. "+ exp.Message);
            }
            
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (wavOutput != null)
            {
                if (wavOutput.PlaybackState == PlaybackState.Playing)
                {
                    wavOutput.Pause();
                    this.pauseButton.Text = "resume";
                }
                else if(wavOutput.PlaybackState == PlaybackState.Paused)
                {
                    wavOutput.Play();
                    this.pauseButton.Text = "pause";
                }

            }
        }

        private void musicPlayer_FormClosed(object sender, FormClosedEventArgs e)
        {
            _KinectDispose();
        }

        private void updateLabelTime_Tick(object sender, System.EventArgs e)
        {

            if (currentPlayingFile == null)
                return;
            string extension = getFileExtension(currentPlayingFile.mediaAbsoluteFileLocation);
            switch (extension)
            {
                case "mp3":
                    setDurationLabels(mp3FileReader.CurrentTime , mp3FileReader.TotalTime);
                    break;
                case "wav":
                    setDurationLabels(waveFileReader.CurrentTime, waveFileReader.TotalTime);
                    break;

            }
        }

        private void isKinectGestureTrackingEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
           
            if ((sender as CheckBox).Checked == true)
            {
                this.kinectWindowForm = new KinectTrackingWindow();
                kinectWindowForm.Show();
                kinectWindowForm.setDataUseFulForTracking(this);
            }
            else
            {
                kinectWindowForm.Close();
            }
        }
    }
}
