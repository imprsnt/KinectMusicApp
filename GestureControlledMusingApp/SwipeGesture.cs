using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;



namespace WindowsFormsApplication1
{
   
    class SwipeGesture
    {

#region MEMBER VARIABLES

        // for both SWIPE RIGHT and SWIPE LEFT
        private readonly int THRESHOLD_X_Y_Z_DEFLECTION = 200;                  // represents maximum permissible body deflection - 20 cm
        private readonly int THRESHOLD_TIME_FOR_VALID_GESTURE = 2;              // min time for gesture to complete - 3 sec
        private readonly int THRESHOLD_TIME_FOR_GESTURE_TO_EXPIRE = 10;         // max time before the gesture expires - 10 sec
        private readonly int MAX_RATE_OF_ANGLE_CHANGE = 15;                     // rate of change of angle between frames - 15 deg
        private readonly int THRESHOLD_OPPOSITE_ANGLE = 2;                      // Max amount of permissible movement in the opposite dir - 2 deg
        enum GESTURE {SWIPE_RIGHT , SWIPE_LEFT}                                 // Gesture enum
        
        // for SWIPE RIGHT
        private readonly int SWIPE_RIGHT_MIN_START_ANGLE = 30;                  // The implementation uses the difference of MIN_FINISH and MIN_START 
        private readonly int SWIPE_RIGHT_MIN_FINISH_ANGLE = 90;
        private int indexOfFirstValidSwipeRightFrame;                           // first valid frame that is marking the swipe right gesture start
        private int indexOfLastValidSwipeRightFrame;                            // last valid frame for swipe right
        private bool isSwipeRightGestureStarted;                                // boolean to check if the gesture has started
        private double currentSwipeRightAngle;                                  // this the angle difference between elbowShoulder and elbomHand between consecutive frames
                                                                                // make this angle postive, as the angle for swipe right increases every frame

        DateTime startSwipeRightTime;                                           // records the time when the swipe right started
        DateTime currentSwipeRightTime;                                         // records the current time of a valid swipe right frame           
        TimeSpan swipeRightTimeSpan;                                            // Time difference between current valid frame and first valid frame


        // for SWIPE LEFT                                                       // The parameters defined are same as the swipe right params except ones stated
        private readonly int SWIPE_LEFT_MIN_START_ANGLE = 90;                   // Swipe right starts as an anti clock wise , so a wider starting angle
        private readonly int SWIPE_LEFT_MIN_FINISH_ANGLE = 30;                  // Swipe right starts as an anti clock wise , so a narrower finishing angle
        private int indexOfFirstValidSwipeLeftFrame;
        private int indexOfLastValidSwipeLeftFrame;
        private bool isSwipeLeftGestureStarted;             
        private double currentSwipeLeftAngle;                                   // Since the angle subtended by elbowShoulder and elbowHand decreases
                                                                                // this angle would have negative value between consecutive frames
        DateTime currentSwipeLeftTime;                  
        DateTime startSwipeLeftTime;
        TimeSpan swipeLeftTimeSpan;


#endregion


#region UTILITY FUNCTIONS

       /*
        *   <constructor/>
        */ 
        public SwipeGesture()
        {
            reset(GESTURE.SWIPE_RIGHT);
            reset(GESTURE.SWIPE_LEFT);
        }

        /*
         *  <reset>
         *      Call from gesture engine to reset the whole SwipeGesture Processor
         *  </reset>
         */ 
        public void reset()
        {
            reset(GESTURE.SWIPE_RIGHT);
            reset(GESTURE.SWIPE_LEFT);
        }


        /*
         *  <reset param="gesture">
         *      Takes the type of gesture and resets their apt parameters
         *      1. resets the indexOfFirstValidGestureFrame , -1 is an invalid index
         *      2.        the indexOfLastValidGestureFrame
         *      3.        the Timers , current and start timer
         *      4.        the Timespan
         *      5.        the gestureStarted parameter to false
         *      6.        the currentAngle parameter to 0.
         *  </reset>
         */ 

        private void reset(GESTURE gesture)
        {
            switch (gesture)
            {
                case GESTURE.SWIPE_RIGHT:
                    {
                         indexOfFirstValidSwipeRightFrame = -1;                 // reset the index        
                         indexOfLastValidSwipeRightFrame = -1 ;
                         isSwipeRightGestureStarted = false;                    // gesture started false
                         currentSwipeRightAngle = 0;                            // current angle 0
                         currentSwipeRightTime = new DateTime(1,1,1);           // reset the time to some invalid Time
                         startSwipeRightTime = new DateTime(1, 1, 1); ;
                         swipeRightTimeSpan = new TimeSpan(currentSwipeRightTime.Ticks - startSwipeRightTime.Ticks);
                    }
                    break;
                case GESTURE.SWIPE_LEFT:                                        // same as swipe right
                    {
                        indexOfFirstValidSwipeLeftFrame = -1 ;
                        indexOfLastValidSwipeLeftFrame = -1;
                        isSwipeLeftGestureStarted = false;
                        currentSwipeLeftAngle = 0;
                        currentSwipeLeftTime = new DateTime(1,1,1);
                        startSwipeLeftTime = new DateTime(1,1,1);
                        swipeLeftTimeSpan = new TimeSpan();
                    }
                    break;
            }
        }

        /*
         *  <isJointDelectionValid param="final , initial ">
         *      If the deflection in any of the coordinates between the current frame , i.e. final and the first frame
         *      i.e. initial is more than a threshold value, 20cm in this case, mark is as a faulty frame and so return false.
         *  </isJointDelectionValid>
         */

        private bool isJointDelectionValid(SkeletonPoint final , SkeletonPoint initial )
        {
            if(Math.Abs(final.X - initial.X) >= THRESHOLD_X_Y_Z_DEFLECTION || 
               Math.Abs(final.Y - initial.Y) >= THRESHOLD_X_Y_Z_DEFLECTION ||
               Math.Abs(final.Z - initial.Z) >= THRESHOLD_X_Y_Z_DEFLECTION)
                return false;

            return true;
        }

        /*
         * doc: The function takes the first valid gesture frame ,depending upon what the gesture is, and currently added 
         *      and compares the shouldercenter position , which is representative of the body , accross these frames to
         *      check for out of range body movements
         */
        private bool isBodyLinearDeflectionWithInBounds(GESTURE gesture ,  GestureDatabase gestureDatabase)
        {

            Skeleton firstValidSkeleton;
            AppropriateJointInfo firstValidAptInfo;
            Skeleton currentSkeleton;
            AppropriateJointInfo currentAptInfo;

            int indexOfFirstValidGesture = (gesture == GESTURE.SWIPE_LEFT) ?
                                            indexOfFirstValidSwipeLeftFrame :
                                            indexOfFirstValidSwipeRightFrame;
            

            if(indexOfFirstValidGesture < 0)
                return true;

            gestureDatabase.getLastRecord(out currentSkeleton, out currentAptInfo);                           // last added frame
            gestureDatabase.getRecord(indexOfFirstValidGesture , out firstValidSkeleton , out firstValidAptInfo);       // first valid frame

            // difference between joint positions of shouldercenter in currently added and first valid gesture frame
            return isJointDelectionValid(currentAptInfo.shoulderCenterPos, firstValidAptInfo.shoulderCenterPos); 
   
        }

        private bool isFirstValidGestureFrame(GESTURE gesture , GestureDatabase gestureDatabase)
        {
            Skeleton skeleton;
            AppropriateJointInfo aptJointInfo;

            gestureDatabase.getLastRecord(out skeleton ,out aptJointInfo);

            switch(gesture)
            {
                case GESTURE.SWIPE_RIGHT:
                    {
                        HCI580_Geometry.Vector2D elbowShoulder = new HCI580_Geometry.Vector2D((aptJointInfo.shoulderRightPos.X - aptJointInfo.elbowRightPos.X),
                                                                                              (aptJointInfo.shoulderRightPos.Y - aptJointInfo.elbowRightPos.Y));

                        HCI580_Geometry.Vector2D elbowHand = new HCI580_Geometry.Vector2D((aptJointInfo.handRightPos.X - aptJointInfo.elbowRightPos.X),
                                                                                          (aptJointInfo.handRightPos.Y - aptJointInfo.elbowRightPos.Y));


                         
                        // check if hand Y > elbow Y , hand X < elbow X and angle between elbowShoulder and elbowHand negative
                        if(aptJointInfo.handRightPos.Y > aptJointInfo.elbowRightPos.Y &&
                           aptJointInfo.handRightPos.X < aptJointInfo.elbowRightPos.X && 
                           elbowShoulder.cpsign(elbowHand) < 0)
                        {
                            return true;
                        }
                    }
                    break;

                case GESTURE.SWIPE_LEFT:
                    {
                        HCI580_Geometry.Vector2D elbowShoulder = new HCI580_Geometry.Vector2D((aptJointInfo.shoulderRightPos.X - aptJointInfo.elbowRightPos.X),
                                                                                              (aptJointInfo.shoulderRightPos.Y - aptJointInfo.elbowRightPos.Y));

                        HCI580_Geometry.Vector2D elbowHand = new HCI580_Geometry.Vector2D((aptJointInfo.handRightPos.X - aptJointInfo.elbowRightPos.X),
                                                                                          (aptJointInfo.handRightPos.Y - aptJointInfo.elbowRightPos.Y));


                         
                        // check if hand Y > elbow Y , hand X < elbow X and angle between elbowShoulder and elbowHand negative
                        if(aptJointInfo.handRightPos.Y > aptJointInfo.elbowRightPos.Y &&
                           aptJointInfo.handRightPos.X > aptJointInfo.elbowRightPos.X && 
                           elbowShoulder.cpsign(elbowHand) < 0)
                        {
                            return true;
                        }
                    }
                    break;
            
            }
            return false;
        }

        private bool isIntermediaryFrameValidGestureFrame(GESTURE gesture, GestureDatabase gestureDatabase)
        {
            Skeleton currentSkeleton;
            AppropriateJointInfo currentAptJointInfo;
           

            gestureDatabase.getLastRecord(out currentSkeleton, out currentAptJointInfo);

            

            switch (gesture)
            {
                case GESTURE.SWIPE_RIGHT:
                    {
                        HCI580_Geometry.Vector2D currentElbowShoulder = new HCI580_Geometry.Vector2D((currentAptJointInfo.shoulderRightPos.X - currentAptJointInfo.elbowRightPos.X),
                                                                                              (currentAptJointInfo.shoulderRightPos.Y - currentAptJointInfo.elbowRightPos.Y));

                        HCI580_Geometry.Vector2D currentElbowHand = new HCI580_Geometry.Vector2D((currentAptJointInfo.handRightPos.X - currentAptJointInfo.elbowRightPos.X),
                                                                                          (currentAptJointInfo.handRightPos.Y - currentAptJointInfo.elbowRightPos.Y));


                        // check if hand Y > elbow Y , angle between elbowShoulder and elbowHand negative
                        if (currentAptJointInfo.handRightPos.Y > currentAptJointInfo.elbowRightPos.Y &&
                            currentElbowShoulder.cpsign(currentElbowHand) < 0)
                        {

                            return true;

                        }
                    }
                    break;
                case GESTURE.SWIPE_LEFT:
                    {
                        HCI580_Geometry.Vector2D currentElbowShoulder = new HCI580_Geometry.Vector2D((currentAptJointInfo.shoulderRightPos.X - currentAptJointInfo.elbowRightPos.X),
                                                                                              (currentAptJointInfo.shoulderRightPos.Y - currentAptJointInfo.elbowRightPos.Y));

                        HCI580_Geometry.Vector2D currentElbowHand = new HCI580_Geometry.Vector2D((currentAptJointInfo.handRightPos.X - currentAptJointInfo.elbowRightPos.X),
                                                                                          (currentAptJointInfo.handRightPos.Y - currentAptJointInfo.elbowRightPos.Y));



                        // check if hand Y > elbow Y , angle between elbowShoulder and elbowHand negative
                        if (currentAptJointInfo.handRightPos.Y > currentAptJointInfo.elbowRightPos.Y &&
                            currentElbowShoulder.cpsign(currentElbowHand) < 0)
                        {
                            return true;
                        }
                    }
                    break;

            }
            return false;
        }

        private bool isIntermediaryCumulativeAngularMotionValid(GESTURE gesture, GestureDatabase gestureDatabase)
        {
            Skeleton currentSkeleton;
            AppropriateJointInfo currentAptInfo;
            Skeleton firstValidSkeleton;
            AppropriateJointInfo firstValidAptInfo;
            int indexOfFirstValidMotion;

            indexOfFirstValidMotion =   (gesture == GESTURE.SWIPE_RIGHT) ?
                                        indexOfFirstValidSwipeRightFrame :
                                        indexOfFirstValidSwipeLeftFrame;

            gestureDatabase.getLastRecord(out currentSkeleton, out currentAptInfo);
            gestureDatabase.getRecord(indexOfFirstValidMotion,out firstValidSkeleton , out firstValidAptInfo);


            HCI580_Geometry.Vector2D currentElbowShoulder = new HCI580_Geometry.Vector2D((currentAptInfo.shoulderRightPos.X - currentAptInfo.elbowRightPos.X),
                                                                                         (currentAptInfo.shoulderRightPos.Y - currentAptInfo.elbowRightPos.Y));
            HCI580_Geometry.Vector2D currentElbowHand =     new HCI580_Geometry.Vector2D((currentAptInfo.handRightPos.X - currentAptInfo.elbowRightPos.X),
                                                                                         (currentAptInfo.handRightPos.Y - currentAptInfo.elbowRightPos.Y));

            HCI580_Geometry.Vector2D firstElbowShoulder = new HCI580_Geometry.Vector2D((firstValidAptInfo.shoulderRightPos.X - firstValidAptInfo.elbowRightPos.X),
                                                                                        (firstValidAptInfo.shoulderRightPos.Y - firstValidAptInfo.elbowRightPos.Y));
            HCI580_Geometry.Vector2D firstElbowHand = new HCI580_Geometry.Vector2D((firstValidAptInfo.handRightPos.X - firstValidAptInfo.elbowRightPos.X),
                                                                                         (firstValidAptInfo.handRightPos.Y - firstValidAptInfo.elbowRightPos.Y));

            double diffAngle =  currentElbowShoulder.angleTo(currentElbowHand) - firstElbowShoulder.angleTo(firstElbowHand);
            // Console.WriteLine("cumulative  diff " + diffAngle);
            return (gesture == GESTURE.SWIPE_RIGHT) ? (diffAngle > 0 ? true : false) : (diffAngle < 0 ? true : false);


        }

        // check for too fast and opposite movements
        private bool isHandAngularChangeWithInBounds(GESTURE gesture , GestureDatabase gestureDatabase)
        {

            Skeleton lastSkeleton;
            AppropriateJointInfo lastAptJointInfo;

            Skeleton lastValidSkeleton;
            AppropriateJointInfo lastValidAptJointInfo;

            int indexOfLastValidGesture = (gesture == GESTURE.SWIPE_RIGHT) ?
                                           indexOfLastValidSwipeRightFrame :
                                           indexOfLastValidSwipeLeftFrame;


            if (indexOfLastValidGesture < 0)
            {
                return true;
            }
            // compare the current frame with the last valid swipe right frame
            gestureDatabase.getLastRecord(out lastSkeleton , out lastAptJointInfo);
            gestureDatabase.getRecord(indexOfLastValidGesture , out lastValidSkeleton , out lastValidAptJointInfo);

            HCI580_Geometry.Vector2D lastElbowShoulder = new HCI580_Geometry.Vector2D(
                                                           (lastAptJointInfo.shoulderRightPos.X - lastAptJointInfo.elbowRightPos.X) ,
                                                           (lastAptJointInfo.shoulderRightPos.Y - lastAptJointInfo.elbowRightPos.Y));

            HCI580_Geometry.Vector2D lastElbowHand = new HCI580_Geometry.Vector2D(
                                                           (lastAptJointInfo.handRightPos.X - lastAptJointInfo.elbowRightPos.X),
                                                           (lastAptJointInfo.handRightPos.Y - lastAptJointInfo.elbowRightPos.Y));

            HCI580_Geometry.Vector2D lastValidElbowShoulder = new HCI580_Geometry.Vector2D(
                                                           (lastValidAptJointInfo.shoulderRightPos.X - lastValidAptJointInfo.elbowRightPos.X),
                                                           (lastValidAptJointInfo.shoulderRightPos.Y - lastValidAptJointInfo.elbowRightPos.Y));

            HCI580_Geometry.Vector2D lastValidElbowHand  = new HCI580_Geometry.Vector2D(
                                                           (lastValidAptJointInfo.handRightPos.X - lastValidAptJointInfo.elbowRightPos.X),
                                                           (lastValidAptJointInfo.handRightPos.Y - lastValidAptJointInfo.elbowRightPos.Y));

            if (Math.Abs(lastElbowShoulder.angleTo(lastElbowHand) - lastValidElbowShoulder.angleTo(lastValidElbowHand)) > MAX_RATE_OF_ANGLE_CHANGE)
            {
                // Console.WriteLine("angle change too fast");
                return false;       // moving too fast
            }

            // Console.WriteLine("last frame Angle " + lastElbowShoulder.angleTo(lastElbowHand) + " last Valid Frame Angle " + lastValidElbowShoulder.angleTo(lastValidElbowHand) +
            //                             " difference " + (lastElbowShoulder.angleTo(lastElbowHand) - lastValidElbowShoulder.angleTo(lastValidElbowHand)));
            switch (gesture)
            {
                case GESTURE.SWIPE_RIGHT:
                    {
                       

                        /*
                         *  we are getting only the magnitude of the angles here, so swipe right opposite is moving from larger angle to smaller angle
                         */

                        if (lastElbowShoulder.angleTo(lastElbowHand) - lastValidElbowShoulder.angleTo(lastValidElbowHand) <= -THRESHOLD_OPPOSITE_ANGLE)
                        {
                            // Console.WriteLine("Gesture : SWIPE RIGHT , OPPOSITE MOTION ....");
                            return false;
                        }
                            
                    }
                    break;
                case GESTURE.SWIPE_LEFT:
                    {
                        if (lastElbowShoulder.angleTo(lastElbowHand) - lastValidElbowShoulder.angleTo(lastValidElbowHand) >= THRESHOLD_OPPOSITE_ANGLE)
                        {
                            // Console.WriteLine("Gesture : SWIPE LEFT , OPPOSITE MOTION ....");
                            return false;
                        }
                            
                    }
                    break;
            }
                

            return true;
        }


        private double updateCurrentAngle(GESTURE gesture , GestureDatabase gestureDatabase)
        {
            double diffAngle = 0.0;


            Skeleton lastSkeleton;
            AppropriateJointInfo lastAptJointInfo;

            Skeleton lastValidSkeleton;
            AppropriateJointInfo lastValidAptJointInfo;

            int indexOfLastValidGesture = (gesture == GESTURE.SWIPE_RIGHT) ?
                                           indexOfLastValidSwipeRightFrame :
                                           indexOfLastValidSwipeLeftFrame;

            // compare the current frame with the last valid swipe right frame
            gestureDatabase.getLastRecord(out lastSkeleton, out lastAptJointInfo);
            gestureDatabase.getRecord(indexOfLastValidGesture, out lastValidSkeleton, out lastValidAptJointInfo);

            HCI580_Geometry.Vector2D lastElbowShoulder = new HCI580_Geometry.Vector2D(
                                                           (lastAptJointInfo.shoulderRightPos.X - lastAptJointInfo.elbowRightPos.X),
                                                           (lastAptJointInfo.shoulderRightPos.Y - lastAptJointInfo.elbowRightPos.Y));

            HCI580_Geometry.Vector2D lastElbowHand = new HCI580_Geometry.Vector2D(
                                                           (lastAptJointInfo.handRightPos.X - lastAptJointInfo.elbowRightPos.X),
                                                           (lastAptJointInfo.handRightPos.Y - lastAptJointInfo.elbowRightPos.Y));

            HCI580_Geometry.Vector2D lastValidElbowShoulder = new HCI580_Geometry.Vector2D(
                                                           (lastValidAptJointInfo.shoulderRightPos.X - lastValidAptJointInfo.elbowRightPos.X),
                                                           (lastValidAptJointInfo.shoulderRightPos.Y - lastValidAptJointInfo.elbowRightPos.Y));

            HCI580_Geometry.Vector2D lastValidElbowHand = new HCI580_Geometry.Vector2D(
                                                           (lastValidAptJointInfo.handRightPos.X - lastValidAptJointInfo.elbowRightPos.X),
                                                           (lastValidAptJointInfo.handRightPos.Y - lastValidAptJointInfo.elbowRightPos.Y));


            diffAngle = lastElbowShoulder.angleTo(lastElbowHand) - lastValidElbowShoulder.angleTo(lastValidElbowHand);

            // Console.WriteLine("diff Angle " +  diffAngle);
            diffAngle = (gesture == GESTURE.SWIPE_RIGHT) ? (diffAngle < 0 ? 0 : diffAngle) : (diffAngle > 0 ? 0 : diffAngle);

            // both the angles need to be added , as the diff angle will be negative for left swipes. 
            return ((gesture == GESTURE.SWIPE_RIGHT) ? this.currentSwipeRightAngle + diffAngle : this.currentSwipeLeftAngle + diffAngle);
        }

#endregion

#region SWIPE RIGHT
        public bool processSwipeRightGesture(GestureDatabase gestureDatabase)
        {

            // check for a valid start gesture , see if the hands are with in the angle range. Start the gesture if the frame is valid
            if (gestureDatabase.getTotalSize() > 0)
            {
                if(!isSwipeRightGestureStarted && indexOfFirstValidSwipeRightFrame < 0)
                {
                    bool isFrameValidSwipeRightStartFrame = isFirstValidGestureFrame(GESTURE.SWIPE_RIGHT, gestureDatabase);
                    if (!isFrameValidSwipeRightStartFrame)
                    {
                        reset(GESTURE.SWIPE_RIGHT);
                        return false;
                    }

                    indexOfFirstValidSwipeRightFrame = indexOfLastValidSwipeRightFrame = gestureDatabase.getTotalSize() - 1;
                    isSwipeRightGestureStarted = true;
                    startSwipeRightTime = currentSwipeRightTime = DateTime.Now;
                    return false;
                }
                
            }
            else
            {
                return false;
            }

            /*
             * <condition>
             *  See if body doesnt move 
             * </condition>
            */

            bool isBodyDeflectionValid = isBodyLinearDeflectionWithInBounds(GESTURE.SWIPE_RIGHT, gestureDatabase);
            if (!isBodyDeflectionValid)
            {
                // Console.WriteLine("gesture : SWIPE RIGHT , invalid isBodyDeflectionValid");
                reset(GESTURE.SWIPE_RIGHT);
                return false;
            }

            /*
             * <condition>
             *  See if the intermediary frames are valid for the SWIPE RIGHT gesture
             * </condition>
            */
            bool isIntermediaryFrameValid = isIntermediaryFrameValidGestureFrame(GESTURE.SWIPE_RIGHT, gestureDatabase);
            if (!isIntermediaryFrameValid)
            {
                // Console.WriteLine("gesture : SWIPE RIGHT , invalid isIntermediaryFrameValid");
                reset(GESTURE.SWIPE_RIGHT);
                return false;
            }

            /*
             * <condition>
             *  See if the movement of the hand is a valid SWIPE RIGHT
             * </condition>
            */

            bool isHandAngularMovementValid = isHandAngularChangeWithInBounds(GESTURE.SWIPE_RIGHT , gestureDatabase);
            if (!isHandAngularMovementValid)
            {
                // Console.WriteLine("gesture : SWIPE RIGHT , invalid isHandAngularMovementValid");
                reset(GESTURE.SWIPE_RIGHT);
                return false;
            }

            /*
            * <condition>
            *  See if the cumulative angle is valid SWIPE RIGHT Angle , ClockWise , only at an interval of multiple of 30 frames
            * </condition>
           */

            if (indexOfFirstValidSwipeRightFrame != indexOfLastValidSwipeRightFrame &&  indexOfLastValidSwipeRightFrame % 30 == 0)
            {
                bool isIntermediaryCumulativeAngleValid = isIntermediaryCumulativeAngularMotionValid(GESTURE.SWIPE_RIGHT, gestureDatabase);
                if (!isIntermediaryCumulativeAngleValid)
                {
                    // Console.WriteLine("gesture : SWIPE RIGHT , invalid isIntermediaryCumulativeAngleValid");
                    reset(GESTURE.SWIPE_RIGHT);
                    return false;
                }
            }

            /*
             * <Update>
             *  All conditions are satified, Update time and angle and check for gesture completion or expiration
             * </Update>
            */

            currentSwipeRightAngle =  updateCurrentAngle(GESTURE.SWIPE_RIGHT , gestureDatabase);
            currentSwipeRightTime = DateTime.Now;
            swipeRightTimeSpan = new TimeSpan(currentSwipeRightTime.Ticks - startSwipeRightTime.Ticks);
            indexOfLastValidSwipeRightFrame++;

            // Console.WriteLine("\ngesture : SWIPE RIGHT , current Angle " + currentSwipeRightAngle);
            // Console.WriteLine("\ngesture : SWIPE RIGHT , current TIme "  + swipeRightTimeSpan.Seconds);
            if((currentSwipeRightAngle >= SWIPE_RIGHT_MIN_FINISH_ANGLE - SWIPE_RIGHT_MIN_START_ANGLE) &&
                swipeRightTimeSpan.Seconds >= THRESHOLD_TIME_FOR_VALID_GESTURE)
            {
                // gesture completed
                reset(GESTURE.SWIPE_RIGHT);
                return true;
            }else if(swipeRightTimeSpan.Seconds >= THRESHOLD_TIME_FOR_GESTURE_TO_EXPIRE)
            {
                // time over, gesture not completed
                reset(GESTURE.SWIPE_RIGHT);
                return false;
            }

            // gesture frames are valid , just that the gesture isn't complete

            return false;
        }
#endregion

#region SWIPE LEFT

        public bool processSwipeLeftGesture(GestureDatabase gestureDatabase)
        {
            // check for a valid start gesture , see if the hands are with in the angle range. Start the gesture if the frame is valid
            if (gestureDatabase.getTotalSize() > 0)
            {
                if (!isSwipeLeftGestureStarted && indexOfFirstValidSwipeLeftFrame < 0)
                {
                    bool isFrameValidSwipeLeftStartFrame = isFirstValidGestureFrame(GESTURE.SWIPE_LEFT, gestureDatabase);
                    if (!isFrameValidSwipeLeftStartFrame)
                    {
                        reset(GESTURE.SWIPE_LEFT);
                        return false;
                    }

                    indexOfFirstValidSwipeLeftFrame = indexOfLastValidSwipeLeftFrame = gestureDatabase.getTotalSize() - 1;
                    isSwipeLeftGestureStarted = true;
                    startSwipeLeftTime = currentSwipeLeftTime = DateTime.Now;
                    return false;
                }

            }
            else
            {
                return false;
            }

            /*
             * <condition>
             *  See if body doesnt move 
             * </condition>
            */

            bool isBodyDeflectionValid = isBodyLinearDeflectionWithInBounds(GESTURE.SWIPE_LEFT, gestureDatabase);
            if (!isBodyDeflectionValid)
            {
                // Console.WriteLine("gesture : SWIPE LEFt , invalid isBodyDeflectionValid");
                reset(GESTURE.SWIPE_LEFT);
                return false;
            }

            /*
             * <condition>
             *  See if the intermediary frames are valid for the SWIPE LEFT gesture
             * </condition>
            */
            bool isIntermediaryFrameValid = isIntermediaryFrameValidGestureFrame(GESTURE.SWIPE_LEFT, gestureDatabase);
            if (!isIntermediaryFrameValid)
            {
                // Console.WriteLine("gesture : SWIPE LEFt , invalid isIntermediaryFrameValid");
                reset(GESTURE.SWIPE_LEFT);
                return false;
            }

            /*
             * <condition>
             *  See if the movement of the hand is a valid SWIPE LEFT
             * </condition>
            */

            bool isHandAngularMovementValid = isHandAngularChangeWithInBounds(GESTURE.SWIPE_LEFT, gestureDatabase);
            if (!isHandAngularMovementValid)
            {
                // Console.WriteLine("gesture : SWIPE LEFt , invalid isHandAngularMovementValid");
                reset(GESTURE.SWIPE_LEFT);
                return false;
            }

            /*
            * <condition>
            *  See if the cumulative angle is valid SWIPE LEfT Angle , Counter ClockWise , only at an interval of multiple of 30 frames
            * </condition>
           */

            if (indexOfFirstValidSwipeLeftFrame != indexOfLastValidSwipeLeftFrame && indexOfLastValidSwipeLeftFrame % 30 == 0)
            {
                bool isIntermediaryCumulativeAngleValid = isIntermediaryCumulativeAngularMotionValid(GESTURE.SWIPE_LEFT, gestureDatabase);
                if (!isIntermediaryCumulativeAngleValid)
                {
                    // Console.WriteLine("gesture : SWIPE LEFt , invalid isIntermediaryCumulativeAngleValid");
                    reset(GESTURE.SWIPE_LEFT);
                    return false;
                }
            }
            
            /*
             * <Update>
             *  All conditions are satified, Update time and angle and check for gesture completion or expiration
             * </Update>
            */
            // Console.WriteLine("\ngesture : SWIPE LEFT , current Angle " + currentSwipeLeftAngle);
            // Console.WriteLine("\ngesture : SWIPE LEFT , current TIme " + swipeLeftTimeSpan.Seconds);
            currentSwipeLeftAngle = updateCurrentAngle(GESTURE.SWIPE_LEFT, gestureDatabase);
            currentSwipeLeftTime = DateTime.Now;
            swipeLeftTimeSpan = new TimeSpan(currentSwipeLeftTime.Ticks - startSwipeLeftTime.Ticks);
            indexOfLastValidSwipeLeftFrame++;


            if ((currentSwipeLeftAngle <=  SWIPE_LEFT_MIN_FINISH_ANGLE - SWIPE_LEFT_MIN_START_ANGLE) &&
                swipeLeftTimeSpan.Seconds >= THRESHOLD_TIME_FOR_VALID_GESTURE)
            {
                // gesture completed
                reset(GESTURE.SWIPE_LEFT);
                return true;
            }
            else if (swipeLeftTimeSpan.Seconds >= THRESHOLD_TIME_FOR_GESTURE_TO_EXPIRE)
            {
                // time over, gesture not completed
                reset(GESTURE.SWIPE_LEFT);
                return false;
            }

            // gesture frames are valid , just that the gesture isn't complete

            return false;
        }

#endregion

    }
}
