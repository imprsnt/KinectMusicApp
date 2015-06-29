using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace WindowsFormsApplication1
{
    public partial class TiltWindow : Form
    {
        public TiltWindow()
        {
            InitializeComponent();
        }

        public void setMusicPlayer(musicPlayer _musicPlayer)
        {
            this._musicPlayer = _musicPlayer;
        }

        public void setKinectSensors(KinectSensor kinectDevice)
        {
            this.kinectDevice = kinectDevice;
            int elevation = kinectDevice.ElevationAngle;;
            this.verticalTiltValue.Value = elevation;
           
        }

        private void TiltWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            kinectDevice.ElevationAngle = (int)this.verticalTiltValue.Value;
            System.Threading.Thread.Sleep(1500);
        }

      
    }
}
