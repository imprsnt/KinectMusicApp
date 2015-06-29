using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public enum GESTURES
    {
        SWIPE_RIGHT = 1,            // for track forward
        SWIPE_LEFT = 2,             // for track backwards
        VERTICAL_UP = 4,            // for volume up
        VERTICAL_DOWN = 8,          // for volume down
        PUSH_FORWARD = 16,          // for pause
        PULL_BACKWARDS = 32,        // for play / resume
        CONST_HAND_LEFT = 64,       // for prev track
        CONST_HAND_RIGHT = 128      // for next track
    }

    public delegate void gestureEventHandler     (object source, gestureEventArgs e);



    public class GestureEngine
    {
        public event gestureEventHandler OnGestureDetected;     //custom event that is fired when a gesture is recognized


        
        private GestureDatabase gestureDatabase;
        private SwipeGesture swipeGestureProcessor;
        private VerticalUpDownGesture verticalUpDownGestureProcessor;
        private PushPullGesture pushPullGestureProcessor;
        private ConstHandLeftRightGesture constHandLeftRightGestureProcessor;
        private Skeleton[] tempSkeletonFrames;
        private Skeleton tempSkeleton;
        private AppropriateJointInfo aptJoint;
        private KinectSensor kinectDevice;
        private int framesSkipped;
        private readonly int MIN_NO_FRAMES_TO_CHECK = 60;
        private readonly int MAX_NO_FRAMES_TO_CHECK = 180;
        private readonly int KINECT_MAX_SKELETON_TRACKABLE = 6;
        private readonly int MULTIPLYING_FACTOR = 1000;
        private readonly int INITIAL_NUM_OF_FRAMES_TO_SKIP = 60;

        public GestureEngine()
        {
         
            gestureDatabase = new GestureDatabase();
            swipeGestureProcessor = new SwipeGesture();
            verticalUpDownGestureProcessor = new VerticalUpDownGesture();
            pushPullGestureProcessor = new PushPullGesture();
            constHandLeftRightGestureProcessor = new ConstHandLeftRightGesture();
            tempSkeletonFrames = new Skeleton[KINECT_MAX_SKELETON_TRACKABLE];
            framesSkipped = 0;
        }

        public GestureEngine(KinectSensor kinectDevice)
        {

            this.kinectDevice = kinectDevice;
            gestureDatabase = new GestureDatabase();
            swipeGestureProcessor = new SwipeGesture();
            verticalUpDownGestureProcessor = new VerticalUpDownGesture();
            pushPullGestureProcessor = new PushPullGesture();
            constHandLeftRightGestureProcessor = new ConstHandLeftRightGesture();
            tempSkeletonFrames = new Skeleton[KINECT_MAX_SKELETON_TRACKABLE];
            framesSkipped = 0;


        }
         

        public void copyDataTo(SkeletonFrame frame)
        {
            // process the frames here , get all the tracked Joints and add them to Database.

            // dont copy first 60 frames used to stabilize skeletons and filter out garbage data
            if (++this.framesSkipped <= INITIAL_NUM_OF_FRAMES_TO_SKIP)
            {
                return;
            }

            frame.CopySkeletonDataTo(tempSkeletonFrames);
            for (int i = 0; i < 6; i++)
            {
                tempSkeleton = tempSkeletonFrames[i];
                if (tempSkeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    
                    aptJoint = trackAptJoints(tempSkeleton);
                    if (aptJoint == null) continue;
                    int count = gestureDatabase.addToDatabase(tempSkeleton, aptJoint);
                    onProcessGestures();
                    

                }
            }


        }

        private void scaleUp(ref SkeletonPoint skeletonPos)
        {
            skeletonPos.X *= MULTIPLYING_FACTOR;
            skeletonPos.Y *= MULTIPLYING_FACTOR;
            skeletonPos.Z *= MULTIPLYING_FACTOR;
        }

        private AppropriateJointInfo trackAptJoints(Skeleton skeleton)
        {
            
            

            AppropriateJointInfo aptJointInfo = new AppropriateJointInfo();
            bool isJointNotTracked = false;

            aptJointInfo.skeletonTrackingID = skeleton.TrackingId;

            if (skeleton.Joints[JointType.WristLeft].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.handLeftPos = skeleton.Joints[JointType.WristLeft].Position;
                scaleUp(ref aptJointInfo.handLeftPos);

            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.ShoulderLeft].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.shoulderLeftPos = skeleton.Joints[JointType.ShoulderLeft].Position;
                scaleUp(ref aptJointInfo.shoulderLeftPos);                               
            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.ElbowLeft].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.elbowLeftPos = skeleton.Joints[JointType.ElbowLeft].Position ;
                scaleUp(ref aptJointInfo.elbowLeftPos);
            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.WristRight].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.handRightPos = skeleton.Joints[JointType.WristRight].Position;
                scaleUp(ref aptJointInfo.handRightPos);
            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.ShoulderRight].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.shoulderRightPos = skeleton.Joints[JointType.ShoulderRight].Position ;
                scaleUp(ref aptJointInfo.shoulderRightPos);
            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.ElbowRight].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.elbowRightPos = skeleton.Joints[JointType.ElbowRight].Position ;
                scaleUp(ref aptJointInfo.elbowRightPos);
            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.Head].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.headPos = skeleton.Joints[JointType.Head].Position;
                scaleUp(ref aptJointInfo.headPos);
            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.ShoulderCenter].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.shoulderCenterPos = skeleton.Joints[JointType.ShoulderCenter].Position;
                scaleUp(ref aptJointInfo.shoulderCenterPos);
            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.Spine].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.spinePos =  skeleton.Joints[JointType.Spine].Position ;
                scaleUp(ref aptJointInfo.spinePos);
            }
            else
            {
                isJointNotTracked = true;
            }

            if (skeleton.Joints[JointType.HipCenter].TrackingState == JointTrackingState.Tracked)
            {
                aptJointInfo.hipCenterPos =  skeleton.Joints[JointType.HipCenter].Position ;
                scaleUp(ref aptJointInfo.hipCenterPos);
            }
            else
            {
                isJointNotTracked = true;
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("C:/t.txt", true))
            {
                file.WriteLine(aptJointInfo.handRightPos.X + " " + aptJointInfo.handRightPos.Y + " " + aptJointInfo.handRightPos.Z);
                file.WriteLine(aptJointInfo.spinePos.X + " " + aptJointInfo.spinePos.Y + " " + aptJointInfo.spinePos.Z);
                file.WriteLine(aptJointInfo.shoulderRightPos.X + " " + aptJointInfo.shoulderRightPos.Y + " " + aptJointInfo.shoulderRightPos.Z);
                file.WriteLine();
            }

            if (isJointNotTracked)
                return null;
            return aptJointInfo;

        }

        private Point getJointPoint(Joint joint)
        {
            Point p = new Point();
            return p;
        }

        public void addToGestureDatabase(Skeleton skeleton , AppropriateJointInfo aptJoint)
        {

            gestureDatabase.addToDatabase(skeleton, aptJoint);
            
        }


        private PRIMARY_HAND getPrimaryHand()
        {
            return PRIMARY_HAND.HAND_RIGHT;
        }


        private void preProcessGestures()
        {
            Skeleton skeleton0;
            AppropriateJointInfo aptJoint0;

            Skeleton skeleton1;
            AppropriateJointInfo aptJoint1;

            if (gestureDatabase.getTotalSize() <= 1)
            {
                return;
            }

            gestureDatabase.getLastRecord(out skeleton0, out aptJoint0);
            gestureDatabase.getRecord(gestureDatabase.getTotalSize() - 2, out skeleton1, out aptJoint1);

            if (aptJoint0.skeletonTrackingID != aptJoint1.skeletonTrackingID)
            {
                // delete all the records before this frame, the skeleton is lost and let only the new skeleton be there as valid frame
                gestureDatabase.removeInvalidData(0,gestureDatabase.getTotalSize() - 2);
            }


        }

        public void onProcessGestures()
        {

            //Console.WriteLine("Message On process gesture called");
            bool isGestureDetected = false;

            if (OnGestureDetected == null)
            {
                System.Windows.Forms.MessageBox.Show("Error! Gesture Event Handler is undefined , Gesture engine cannot handle gestures ");
                return;
            }

            preProcessGestures();

  /*          
            if (isPushForwardGesture())
             {
                 isGestureDetected = true;
                 OnGestureDetected(this, new gestureEventArgs(GESTURES.PUSH_FORWARD));
             }
            
            if (isPullBackwardsGesture())
            {
                isGestureDetected = true;
                OnGestureDetected(this, new gestureEventArgs(GESTURES.PULL_BACKWARDS));

            }
         
*/
            if (isSwipeRightGesture())
            {
                isGestureDetected = true;
                OnGestureDetected(this, new gestureEventArgs(GESTURES.SWIPE_RIGHT));
                
            }
          
            if (isSwipeLeftGesture())
            {
                isGestureDetected = true;
                OnGestureDetected(this, new gestureEventArgs(GESTURES.SWIPE_LEFT));
                
            }


/*                         
            if (isVerticalUpGesture())
            {
                isGestureDetected = true;
                OnGestureDetected(this, new gestureEventArgs(GESTURES.VERTICAL_UP));
                
            }
            if (isVerticalDownGesture())
            {
                isGestureDetected = true;
                gestureDatabase.ClearDatabase();
                OnGestureDetected(this, new gestureEventArgs(GESTURES.VERTICAL_DOWN));
                
            }
*/

            if (isConstHandLeftGesture())
            {
                isGestureDetected = true;
                OnGestureDetected(this, new gestureEventArgs(GESTURES.CONST_HAND_LEFT));
              
            }
         
            if (isConstHandRightGesture())
            {
                isGestureDetected = true;
                OnGestureDetected(this, new gestureEventArgs(GESTURES.CONST_HAND_RIGHT));
              
            }

            if (gestureDatabase.getTotalSize() > gestureDatabase.MAX_DATABASE_SIZE) gestureDatabase.ClearDatabase(); // clear database when it reaches maximum no of frames
            if (isGestureDetected) postProcessGestures();

            return;
        }

        private void postProcessGestures()
        {
            gestureDatabase.ClearDatabase();
            pushPullGestureProcessor.reset();
            constHandLeftRightGestureProcessor.reset();
            swipeGestureProcessor.reset();
        }

        private bool isSwipeRightGesture()
        {

            return swipeGestureProcessor.processSwipeRightGesture(this.gestureDatabase);
            
        }

        private bool isSwipeLeftGesture()
        {
            return swipeGestureProcessor.processSwipeLeftGesture(this.gestureDatabase);
        }

        private bool isVerticalUpGesture()
        {
            return verticalUpDownGestureProcessor.processVerticalUpGesture(this.gestureDatabase);
        }

        private  bool isVerticalDownGesture()
        {

            return verticalUpDownGestureProcessor.processVerticalDownGesture(this.gestureDatabase);
        }

        private bool isPushForwardGesture()
        {
            return pushPullGestureProcessor.processPushForwardGesture(this.gestureDatabase);
        }

        private bool isPullBackwardsGesture()
        {
            
            return pushPullGestureProcessor.processPullBackwardsGesture(this.gestureDatabase);
        }

        private bool isConstHandLeftGesture()
        {
            return constHandLeftRightGestureProcessor.processConstHandLeftGesture(this.gestureDatabase);
        }

        private bool isConstHandRightGesture()
        {
            return constHandLeftRightGestureProcessor.processConstHandRightGesture(this.gestureDatabase);
        }

    }
}
