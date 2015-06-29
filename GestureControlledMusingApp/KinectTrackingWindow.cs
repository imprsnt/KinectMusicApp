using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;
using System.Windows.Forms.Integration;
using System.Windows.Shapes;

namespace WindowsFormsApplication1
{
  
       
  

    public partial class KinectTrackingWindow : Form
    {



        public KinectTrackingWindow()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.skeletonCanvas = new System.Windows.Controls.Canvas();
            this.skeletonElementHost = new ElementHost();
            this.skeletonElementHost.Dock = DockStyle.Fill;
            this.skeletonPanel.Controls.Add(skeletonElementHost);
            skeletonElementHost.Child = skeletonCanvas;

            this.displayColorStreamVideo = true;
            this.displayDepthStreamVideo = true;
            this.displaySkeletonStreamVideo = true;

           

        }

        public void setDataUseFulForTracking(musicPlayer __musicPlayer)
        {
            this._musicPlayer = __musicPlayer;
        }

        public KinectSensor kinectDevice
        {
            
            get
            {
                return _kinectDevice;
            }
            set
            {
                if (_kinectDevice != value)
                {
                    if (_kinectDevice != null)
                    {
                        _kinectDevice = null;
                    }
                    if (value != null && value.Status == KinectStatus.Connected)
                    {
                        _kinectDevice = value;
                    }
                }
            }
        }

#region pre initializing kinect sensors


        private void discoverKinect()
        {

            KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(KinectSensors_StatusChanged);
            kinectDevice = KinectSensor.KinectSensors.FirstOrDefault(dev => dev.Status == KinectStatus.Connected);

        }

        void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {

            switch (e.Status)
            {
                case KinectStatus.Connected:
                    if (kinectDevice == null) 
                        kinectDevice = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    if (kinectDevice == e.Sensor)
                    {
                        kinectDevice = null;
                        kinectDevice = KinectSensor.KinectSensors.FirstOrDefault(dev => dev.Status == KinectStatus.Connected);
                        if (kinectDevice == null)
                        {
                            System.Windows.Forms.MessageBox.Show("No Kinect Sensor found!");
                        }
                    }
                    break;

                case KinectStatus.NotPowered:
                    System.Windows.MessageBox.Show("Kindly ensure that Kinect is connected, or re-initialize the sensors");
                    break;

            }
        }

        private void enableKinectSensors(int sensors)
        {

            discoverKinect();
            if ((sensors & ENABLE_COLOR_SENSOR) != 0)
                enableKinectColorSensor();
            if ((sensors & ENABLE_DEPTH_SENSOR) != 0)
                enableKinectDepthSensor();
            if ((sensors & ENABLE_SKELETON_SENSOR) != 0)
                enableKinectSkeletonSensor();
            if(kinectDevice != null)
                kinectDevice.Start();

        }
#endregion
        
#region depth sensor



        //-------------------------------------------  depth sensor start ------------------------------------------------------------------------
        private void enableKinectDepthSensor()
        {
            if (kinectDevice != null && kinectDevice.Status == KinectStatus.Connected)
            {
                kinectDevice.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                setDepthImageBitmapProperty();
                kinectDevice.DepthFrameReady += new EventHandler<DepthImageFrameReadyEventArgs>(kinectDevice_DepthFrameReady);
            }
        }

        void kinectDevice_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame frame = e.OpenDepthImageFrame())
            {
                
                if (frame != null && displayDepthStreamVideo) 
                {
                    
                    //short[] depthData = new short[frame.PixelDataLength];
                    //int[] pixelData = new int[frame.PixelDataLength];
                    frame.CopyDepthImagePixelDataTo(depthPixelData);
                    int colorPixelIndex = 0;
                    for (int i = 0; i < depthPixelData.Length; i++)
                    {
                        short depth = depthPixelData[i].Depth;
                        byte intensity = (byte)((depth < minDepth || depth > maxDepth) ? 0 : depth);

                        depthColorPixelData[colorPixelIndex++] = intensity;
                        depthColorPixelData[colorPixelIndex++] = intensity;
                        depthColorPixelData[colorPixelIndex++] = intensity;
                        colorPixelIndex++;
                    }

                    depthImageBitmap.WritePixels(depthImageBitmapRect, depthColorPixelData, depthImageBitmapStride, 0);
                    using(MemoryStream outStream = new MemoryStream()){
                        BitmapEncoder enc = new BmpBitmapEncoder();
                        enc.Frames.Add(BitmapFrame.Create((BitmapSource)depthImageBitmap));
                        enc.Save(outStream);
                        depthPictureBox.Image = new System.Drawing.Bitmap(outStream); 
                    }
                    
                }
                

            }
        }

        void setDepthImageBitmapProperty()
        {


            minDepth = 610;         //  2 feet
            maxDepth = 3048;        //  10 feet
            depthPixelData = new DepthImagePixel[kinectDevice.DepthStream.FramePixelDataLength];
            depthColorPixelData = new byte[kinectDevice.DepthStream.FramePixelDataLength * sizeof(int)];

            depthImageBitmap = new WriteableBitmap(kinectDevice.DepthStream.FrameWidth,
                                                    kinectDevice.DepthStream.FrameHeight,
                                                    96, 96, PixelFormats.Bgra32, null);
            depthImageBitmapRect = new Int32Rect(0, 0, kinectDevice.DepthStream.FrameWidth,
                                                    kinectDevice.DepthStream.FrameHeight);



            depthImageBitmapStride = kinectDevice.DepthStream.FrameWidth * sizeof(int);

        }
        //-------------------------------------------  depth sensor end ------------------------------------------------------------------------
#endregion

#region color sensor code

        //-------------------------------------------  color sensor start ------------------------------------------------------------------------
        private void enableKinectColorSensor()
        {
            if (kinectDevice != null && kinectDevice.Status == KinectStatus.Connected)
            {
                kinectDevice.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                setColorImageBitmapProperty();
                kinectDevice.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(kinectDevice_ColorFrameReady);
                
            }
        }

        void kinectDevice_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
             using (ColorImageFrame frame = e.OpenColorImageFrame())
            {
                if (frame != null & displayColorStreamVideo) 
                {
                 //   byte[] pixelData = new byte[frame.PixelDataLength];
                    frame.CopyPixelDataTo(colorPixelData);
                    colorImageBitmap.WritePixels(colorImageBitmapRect, colorPixelData, colorImageBitmapStride, 0);
                    using(MemoryStream outStream = new MemoryStream()){
                        BitmapEncoder enc = new BmpBitmapEncoder();
                        enc.Frames.Add(BitmapFrame.Create((BitmapSource)colorImageBitmap));
                        enc.Save(outStream);
                        colorPictureBox.Image = new System.Drawing.Bitmap(outStream); 
                    }
                    
                }
                

            }
        }

        void setColorImageBitmapProperty()
        {
            colorPixelData = new byte[kinectDevice.ColorStream.FramePixelDataLength];

            colorImageBitmap = new WriteableBitmap(kinectDevice.ColorStream.FrameWidth,
                                                   kinectDevice.ColorStream.FrameHeight,
                                                   96, 96, PixelFormats.Bgr32, null);
            colorImageBitmapRect = new Int32Rect(0, 0, kinectDevice.ColorStream.FrameWidth,
                                                       kinectDevice.ColorStream.FrameHeight);

            colorImageBitmapStride = kinectDevice.ColorStream.FrameWidth * kinectDevice.ColorStream.FrameBytesPerPixel;

        }
        //-------------------------------------------  color sensor end ------------------------------------------------------------------------

#endregion

#region skeleton sensor


        //-------------------------------------------  skeleton sensor start ------------------------------------------------------------------------
        private void enableKinectSkeletonSensor()
        {
            if (kinectDevice != null && kinectDevice.Status == KinectStatus.Connected)
            {
              
                kinectDevice.SkeletonStream.Enable();
                setSkeletonBitmapProperty();
              // kinectDevice.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                kinectDevice.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinectDevice_SkeletonFrameReady);
            }
        }

        void kinectDevice_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
                using(SkeletonFrame frame = e.OpenSkeletonFrame())
                {
                    if (frame != null)
                    {
                        mediaAppGestureEngine.copyDataTo(frame);
                        

                        if (!displaySkeletonStreamVideo)
                            return;

                        this.skeletonCanvas.Children.Clear();
                        Polyline figure;
                        Brush brush;
                        Skeleton skeleton;
                        
                        frame.CopySkeletonDataTo(frameSkeletons);
                        for (int i = 0; i < frameSkeletons.Length; i++)
                        {
                            skeleton = frameSkeletons[i];
                            skeleton.ClippedEdges = FrameEdges.Bottom | FrameEdges.Top;
                            if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                            {
                                brush = skeletonBrushes[i % skeletonBrushes.Length];
                                joints = new[]{ JointType.Head ,
                                                JointType.ShoulderCenter,
                                                JointType.ShoulderLeft,
                                                JointType.Spine,
                                                JointType.ShoulderRight,
                                                JointType.ShoulderCenter,
                                                JointType.HipCenter,
                                                JointType.HipLeft,
                                                JointType.Spine,
                                                JointType.HipRight,
                                                JointType.HipCenter,
                                                };
                                this.skeletonCanvas.Children.Add(CreateFigure(skeleton,brush,joints));
                              


                                    joints = new[]{ JointType.HipLeft ,
                                                JointType.KneeLeft,
                                                JointType.FootLeft,
                                                JointType.AnkleLeft,
                                
                                            };
                                this.skeletonCanvas.Children.Add(CreateFigure(skeleton, brush, joints));

                                joints = new[]{ JointType.HipRight ,
                                                JointType.KneeRight,
                                                JointType.FootRight,
                                                JointType.AnkleRight,
                                
                                            };
                                this.skeletonCanvas.Children.Add(CreateFigure(skeleton, brush, joints));

                                joints = new[]{ JointType.ShoulderLeft,
                                                JointType.ElbowLeft,
                                                JointType.WristLeft,
                                                JointType.HandLeft,
                                
                                            };
                                this.skeletonCanvas.Children.Add(CreateFigure(skeleton, brush, joints));

                                joints = new[]{ JointType.ShoulderRight,
                                                JointType.ElbowRight,
                                                JointType.WristRight,
                                                JointType.HandRight,
                                
                                            };
                                this.skeletonCanvas.Children.Add(CreateFigure(skeleton, brush, joints));

                            }
                        }

                    }
                }            
        }

        void setSkeletonBitmapProperty()
        {
            this.skeletonBrushes = new[] { System.Windows.Media.Brushes.Red, System.Windows.Media.Brushes.Green, System.Windows.Media.Brushes.Blue ,
                                           System.Windows.Media.Brushes.Orange , System.Windows.Media.Brushes.DarkGoldenrod, System.Windows.Media.Brushes.Cyan};
            this.frameSkeletons = new Skeleton[kinectDevice.SkeletonStream.FrameSkeletonArrayLength];

        }

        private Polyline CreateFigure(Skeleton skeleton , Brush brush , JointType[] joints)
        {
            Polyline figure = new Polyline();
            figure.StrokeThickness = 8;
            figure.Stroke = brush;

            for (int i = 0; i < joints.Length; i++)
            {
                figure.Points.Add(getJointPoints(skeleton.Joints[joints[i]]));
            }
            return figure;
        }

        private Point getJointPoints(Joint joint)
        {
            DepthImagePoint point = this.kinectDevice.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, kinectDevice.DepthStream.Format);
            point.X = ((int)skeletonCanvas.ActualWidth / kinectDevice.DepthStream.FrameWidth) > 0 ? point.X * (int)(skeletonCanvas.ActualWidth / kinectDevice.DepthStream.FrameWidth) : (int)(point.X);
            point.Y = ((int)skeletonCanvas.ActualHeight / kinectDevice.DepthStream.FrameHeight) > 0 ? point.Y * (int)(skeletonCanvas.ActualHeight / kinectDevice.DepthStream.FrameHeight) : (int)(point.Y);

            return new Point(point.X,point.Y);
        }
        //-------------------------------------------  skeleton sensor stop ------------------------------------------------------------------------

#endregion

#region manipulate music app based on gestures
       public void mediaAppGestureEngine_OnGestureDetected(object source, gestureEventArgs e)
        {
            switch (e.gesture)
            {
                case GESTURES.SWIPE_RIGHT:
                    _musicPlayer.nextButton.PerformClick();
                    break;
                case GESTURES.SWIPE_LEFT:
                    _musicPlayer.prevButton.PerformClick();
                  
                    break;

                case GESTURES.VERTICAL_UP:
                    _musicPlayer.volumeTrackBar.Value++;
                    break;
                case GESTURES.VERTICAL_DOWN:
                    _musicPlayer.volumeTrackBar.Value--;
                    break;

                case GESTURES.PUSH_FORWARD:
                    _musicPlayer.pauseButton.PerformClick();
                    break;
                case GESTURES.PULL_BACKWARDS:
                    _musicPlayer.pauseButton.PerformClick();
                    break;

                case GESTURES.CONST_HAND_LEFT:
                    _musicPlayer.musicTrackBar.Value -= 20;
                    _musicPlayer.musicTrackBar_PerformScroll();
                    break;
                case GESTURES.CONST_HAND_RIGHT:
                    _musicPlayer.musicTrackBar.Value += 20;
                    _musicPlayer.musicTrackBar_PerformScroll();
                    break;
            }
        }
#endregion

#region events handler for forms

        private void KinectTrackingWindow_Load(object sender, EventArgs e)
        {
            enableKinectSensors(ENABLE_COLOR_SENSOR | ENABLE_DEPTH_SENSOR | ENABLE_SKELETON_SENSOR);
            this.mediaAppGestureEngine = new GestureEngine(kinectDevice);  // start gesture engine once the Kinect has finished initializing
            mediaAppGestureEngine.OnGestureDetected += new gestureEventHandler(mediaAppGestureEngine_OnGestureDetected);
           
        }

        private void KinectTrackingWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (kinectDevice == null) return;

            if (kinectDevice.DepthStream != null)
            {
                kinectDevice.DepthFrameReady -= kinectDevice_DepthFrameReady;
                kinectDevice.DepthStream.Disable();

            }
            if (kinectDevice.ColorStream != null)
            {
                kinectDevice.ColorFrameReady -= kinectDevice_ColorFrameReady;
                kinectDevice.ColorStream.Disable();
            }
            if (kinectDevice.SkeletonStream != null)
            {
                kinectDevice.SkeletonFrameReady -= kinectDevice_SkeletonFrameReady; 
                kinectDevice.SkeletonStream.Disable();
            }
            kinectDevice.Stop();
            kinectDevice = null;

            _musicPlayer.isKinectGestureTrackingEnabledCheckBox.Checked = false;
            

        }

        private void ReleaseResourceButton_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void adjustTiltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TiltWindow tiltwindow = new TiltWindow();
            tiltwindow.setKinectSensors(kinectDevice);
            tiltwindow.Show();

        }

        private void adjustDepthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AdjustDepth().Show();
        }

        private void skeletonPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayColorStreamVideo = true;
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayColorStreamVideo = false;
            this.colorPictureBox.Image = null;
        }

        private void onToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            displayDepthStreamVideo = true;
        }

        private void offToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            displayDepthStreamVideo = false;
            this.depthPictureBox.Image = null;
        }

        private void onToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            displaySkeletonStreamVideo = true;
        }

        private void offToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            displaySkeletonStreamVideo = false;
            this.skeletonCanvas.Children.Clear();
        }

        private void standingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
#endregion

     







    }

    
}
