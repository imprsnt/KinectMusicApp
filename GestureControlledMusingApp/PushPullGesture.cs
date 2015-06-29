using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.IO;

namespace WindowsFormsApplication1
{

    /*
     *  Ideally for a Valid Push Gesture : The Straight Joints of the head , spine and Hip Center , shouldn't move.
     *  The Left or Right hand should produce significant Z displacement and very less X or Y deviation for a certain no of frames
     */

    class PushPullGesture
    {

#region MEMBER VARIABLES

        private readonly int THRESHOLD_X_Y_Z_DEFLECTIONS = 200;
        private readonly int THRESHOLD_TIME_FOR_VALID_GESTURE = 3;
        private readonly int THRESHOLD_TIME_FOR_GESTURE_TO_EXPIRE = 10;
        private readonly int THRESHOLD_Z_DEFLECTION_BETWEEN_FRAMES = 50;
        private readonly int THRESHOLD_OPPOSITE_Z_DEFLECTION = 20;
        private readonly int THRESHOLD_MIN_Z_MOVEMENT = 10;


        private TimeSpan timeElapsedPush;
        private DateTime startTimerPush;
        private DateTime currentTimerPush;
        private int indexOfFirstValidPushGestureFrame;
        private int indexOfLastValidPushGestureFrame;
        private float currentPushDistance;
        private bool isPushGestureStarted;
        private readonly int THRESHOLD_PUSH_LENGTH = 300;
        private readonly int INITIAL_MAX_DIST_BETWEEN_HAND_SHOULDER_FOR_PULL = 100;


        private TimeSpan timeElapsedPull;
        private DateTime startTimerPull;
        private DateTime currentTimerPull;
        private int indexOfFirstValidPullGestureFrame;
        private int indexOfLastValidPullGestureFrame;
        private float currentPullDistance;
        private bool isPullGestureStarted;
        private readonly int THRESHOLD_PULL_LENGTH = 300;
        private readonly int INITIAL_MIN_DIST_BETWEEN_HAND_SHOULDER_FOR_PULL = 320;

        private enum GESTURE { PUSH, PULL }

#endregion

#region UTILITY FUNCTIONS

        public PushPullGesture()
        {
            reset(GESTURE.PUSH);
            reset(GESTURE.PULL);
        }

        public void reset()
        {
            reset(GESTURE.PUSH);
            reset(GESTURE.PULL);          
        }

        private void reset(GESTURE gesture)
        {
            switch (gesture)
            {
                case GESTURE.PUSH:

                    currentPushDistance = 0;                    // treat push length as negative , since push is towards decreasing Z
                    indexOfFirstValidPushGestureFrame = -1;
                    indexOfLastValidPushGestureFrame = -1;
                    isPushGestureStarted = false;
                    startTimerPush = new DateTime(1, 1, 1);
                    currentTimerPush = new DateTime(1, 1, 1);
                    timeElapsedPush = new TimeSpan(currentTimerPush.Ticks - currentTimerPush.Ticks);
                    break;

                case GESTURE.PULL:

                    currentPullDistance = 0;                    // treat pull length as positive , since pull is towards positive Z
                    indexOfFirstValidPullGestureFrame = -1;
                    indexOfLastValidPullGestureFrame = -1;
                    isPullGestureStarted = false;
                    startTimerPull = new DateTime(1, 1, 1);
                    currentTimerPull = new DateTime(1, 1, 1);
                    timeElapsedPull = new TimeSpan(currentTimerPull.Ticks - currentTimerPull.Ticks);
                    break;
            }

        }

        private float updateCurrentLength(GESTURE gesture , GestureDatabase gestureDatabase)
        {
            float diffLength = 0;
            int indexOfLastValidGestureFrame = (gesture == GESTURE.PUSH) ?
                                               indexOfLastValidPushGestureFrame :
                                               indexOfLastValidPullGestureFrame;

            Skeleton currentSkeleton;
            AppropriateJointInfo currentAptJointInfo;
            Skeleton lastValidSkeleton;
            AppropriateJointInfo lastValidAptInfo;

            gestureDatabase.getLastRecord(out currentSkeleton, out currentAptJointInfo);
            gestureDatabase.getRecord(indexOfLastValidGestureFrame, out lastValidSkeleton, out lastValidAptInfo);

            diffLength = currentAptJointInfo.handRightPos.Z - lastValidAptInfo.handRightPos.Z;
            diffLength = (gesture == GESTURE.PUSH) ? (diffLength > 0 ? 0 : diffLength) : (diffLength > 0 ? diffLength : 0);

            // since the value of diff lenth is  positive for pull and negative for push so we just add the values to both .
            return (gesture == GESTURE.PUSH) ? currentPushDistance + diffLength : currentPullDistance + diffLength;

        }

        private bool isHand_X_Y_DeflectionWithInBounds(GESTURE gesture , GestureDatabase gestureDatabase)
        {

            if (gesture == GESTURE.PUSH)
                if (indexOfFirstValidPushGestureFrame < 0)
                    return true;                        // this is the start of the new Gesture, first frame of the new gesture
            if (gesture == GESTURE.PULL)
                if (indexOfFirstValidPullGestureFrame < 0)
                    return true;

            int indexOfFirstValidGestureFrame = (gesture == GESTURE.PUSH) ? indexOfFirstValidPushGestureFrame :
                                                                            indexOfFirstValidPullGestureFrame;

            Skeleton currentSkeleton;
            AppropriateJointInfo currentAptJointInfo;

            Skeleton firstValidSkeleton;
            AppropriateJointInfo firstValidAptJointInfo;

            gestureDatabase.getLastRecord(out currentSkeleton, out currentAptJointInfo);
            gestureDatabase.getRecord(indexOfFirstValidGestureFrame, out firstValidSkeleton, out firstValidAptJointInfo);

            if (Math.Abs(currentAptJointInfo.handRightPos.X - firstValidAptJointInfo.handRightPos.X) >= THRESHOLD_X_Y_Z_DEFLECTIONS ||
                Math.Abs(currentAptJointInfo.handRightPos.Y - firstValidAptJointInfo.handRightPos.Y) >= THRESHOLD_X_Y_Z_DEFLECTIONS)
            {
                return false;
            }
           
            return true;
        }

        private bool isValidStartGesture(GESTURE gesture, GestureDatabase gestureDatabase)
        {
            Skeleton skeleton;
            AppropriateJointInfo aptJoint;
            switch (gesture)
            {

                case GESTURE.PUSH:
                    gestureDatabase.getLastRecord(out skeleton, out aptJoint);
                    if (aptJoint.handRightPos.Y < aptJoint.shoulderCenterPos.Y &&
                        aptJoint.handRightPos.Y > aptJoint.elbowRightPos.Y)
                        return true;
                    break;
                case GESTURE.PULL:
                    gestureDatabase.getLastRecord(out skeleton, out aptJoint);
                    if (aptJoint.handRightPos.Y < aptJoint.shoulderCenterPos.Y && 
                       (aptJoint.shoulderCenterPos.Z - aptJoint.handRightPos.Z) > INITIAL_MIN_DIST_BETWEEN_HAND_SHOULDER_FOR_PULL)
                        return true;
                    break;
            }
            return false;
        }

        private bool isHand_Z_DeflectionWithInBounds(GESTURE gesture, GestureDatabase gestureDatabase)
        {
            
            switch (gesture)
            {
                case GESTURE.PUSH:
                    {
                        if (indexOfFirstValidPushGestureFrame < 0)
                        {
                            return true;
                        }
                        // check if there is no movement in the opposite direction between consecute frames
                        Skeleton lastValidSkeleton;
                        AppropriateJointInfo lastValidAptJointInfo;
                        Skeleton lastSkeleton;
                        AppropriateJointInfo lastAptJointInfo;
                        gestureDatabase.getRecord(indexOfLastValidPushGestureFrame, out lastValidSkeleton, out lastValidAptJointInfo);
                        gestureDatabase.getRecord(gestureDatabase.getTotalSize() - 1, out lastSkeleton, out lastAptJointInfo);

                        // checks if the movement in Z dir is invalid positive rather than the valid negative
                        if ((lastAptJointInfo.handRightPos.Z - lastValidAptJointInfo.handRightPos.Z) >= THRESHOLD_OPPOSITE_Z_DEFLECTION)
                        {
                            return false;
                        }
                            

                        // check if the movement is not too fast , i.e it is with in the Z threshold
                        if ((Math.Abs(lastAptJointInfo.handRightPos.Z - lastValidAptJointInfo.handRightPos.Z) >= THRESHOLD_Z_DEFLECTION_BETWEEN_FRAMES))
                        {
                            return false;
                        }

                        break;
                    }
                case GESTURE.PULL:
                    {
                        if (indexOfFirstValidPullGestureFrame < 0)
                        {
                            return true;
                        }
                        // check if there is no movement in the opposite direction between consecute frames
                        Skeleton lastValidSkeleton;
                        AppropriateJointInfo lastValidAptJointInfo;
                        Skeleton lastSkeleton;
                        AppropriateJointInfo lastAptJointInfo;
                        gestureDatabase.getRecord(indexOfLastValidPullGestureFrame, out lastValidSkeleton, out lastValidAptJointInfo);
                        gestureDatabase.getRecord(gestureDatabase.getTotalSize() - 1, out lastSkeleton, out lastAptJointInfo);

                        // checks if the movement in Z dir is invalid negative rather than the valid positive
                        if ((lastAptJointInfo.handRightPos.Z - lastValidAptJointInfo.handRightPos.Z) <= -THRESHOLD_OPPOSITE_Z_DEFLECTION)
                            return false;

                        // check if the movement is not too fast , i.e it is with in the Z threshold
                        if ((Math.Abs(lastAptJointInfo.handRightPos.Z - lastValidAptJointInfo.handRightPos.Z) >= THRESHOLD_Z_DEFLECTION_BETWEEN_FRAMES))
                        {
                            return false;
                        }
                        break;
                    }
            }

            return true;
        }

        private bool isPointWithInThreshold(SkeletonPoint final, SkeletonPoint initial)
        {
            if (Math.Abs(final.X - initial.X) >= THRESHOLD_X_Y_Z_DEFLECTIONS ||
                Math.Abs(final.Y - initial.Y) >= THRESHOLD_X_Y_Z_DEFLECTIONS ||
                Math.Abs(final.Z - initial.Z) >= THRESHOLD_X_Y_Z_DEFLECTIONS)
                return false;
            return true;
        }

        private bool isIntermediaryCumulative_Z_DeflectionValid(GESTURE gesture, GestureDatabase gestureDatabase)
        {
            Skeleton currentSkeleton;
            AppropriateJointInfo currentAptJointInfo;
            Skeleton firstSkeleton;
            AppropriateJointInfo firstAptJointInfo;

            int indexOfFirstGestureFrame = (gesture == GESTURE.PUSH) ?
                                           indexOfFirstValidPushGestureFrame :
                                           indexOfLastValidPullGestureFrame;



            gestureDatabase.getLastRecord(out currentSkeleton , out currentAptJointInfo);
            gestureDatabase.getRecord(indexOfFirstGestureFrame, out firstSkeleton, out firstAptJointInfo);

            double diffZ = currentAptJointInfo.handRightPos.Z - firstAptJointInfo.handRightPos.Z;


            return (gesture == GESTURE.PUSH) ? (diffZ < 0 ? true : false) : (diffZ < 0 ? false : true);

            

        }
        private bool isBodyLinearDeflectionsWithInBounds(GESTURE gesture , GestureDatabase gestureDatabase)
        {
            if(gesture == GESTURE.PUSH)
                if (indexOfFirstValidPushGestureFrame < 0)
                    return true;                        // this is the start of the new Gesture, first frame of the new gesture
            if (gesture == GESTURE.PULL)
                if (indexOfFirstValidPullGestureFrame < 0)
                    return true;

            int indexOfFirstValidGestureFrame = (gesture == GESTURE.PUSH) ? indexOfFirstValidPushGestureFrame :
                                                                            indexOfFirstValidPullGestureFrame;

            Skeleton lastValidSkeleton;
            AppropriateJointInfo lastAptInfo;

            Skeleton firstValidSkeleton;
            AppropriateJointInfo firstAptInfo;

            gestureDatabase.getRecord(gestureDatabase.getTotalSize() - 1, out lastValidSkeleton, out lastAptInfo);
            gestureDatabase.getRecord(indexOfFirstValidGestureFrame, out firstValidSkeleton, out firstAptInfo);

            return (isPointWithInThreshold(lastAptInfo.shoulderCenterPos, firstAptInfo.shoulderCenterPos));

        }
#endregion

#region PUSH GESTURE
       

        public bool processPushForwardGesture(GestureDatabase gestureDatabase)
        {

            

            // first frame is always a valid frame , No initial check on joints is needed ...
            if (gestureDatabase.getTotalSize() > 0)
            {
                // check if the current frame is a valid gesture start frame
                if (!isPushGestureStarted && indexOfFirstValidPushGestureFrame < 0)
                {
                    bool isCurrentFrameValidPushGesture = isValidStartGesture(GESTURE.PUSH, gestureDatabase);
                    if (!isCurrentFrameValidPushGesture)
                    {
                        Console.WriteLine("GESTURE : PUSH | invalid isCurrentFrameValidPushGesture ");
                        reset(GESTURE.PUSH);
                        return false;
                    }
                    indexOfFirstValidPushGestureFrame = indexOfLastValidPushGestureFrame = gestureDatabase.getTotalSize() - 1;
                    startTimerPush = currentTimerPush = DateTime.Now;       // start the timer 
                    isPushGestureStarted = true;
                    currentPushDistance = 0;                                // set  the initial push distance
                    return false;                                           // this is a valid frame but the gesture is incomplete
                }
            }
            else
            {
                return false;
            }


            // check to see if there is any body deflection
            bool isBodyDefectionValid = isBodyLinearDeflectionsWithInBounds(GESTURE.PUSH, gestureDatabase);

            if (!isBodyDefectionValid)
            {
                Console.WriteLine("GESTURE : PUSH | invalid isBodyDefectionValid ");
                reset(GESTURE.PUSH);
                return false;
            }

            // check to see if there is hand deflection except in the positive Z-dir

            bool isHand_X_Y_DeflectionValid = isHand_X_Y_DeflectionWithInBounds(GESTURE.PUSH, gestureDatabase);
            if (!isHand_X_Y_DeflectionValid)
            {
                Console.WriteLine("GESTURE : PUSH | invalid isHand_X_Y_DeflectionValid ");
                reset(GESTURE.PUSH);
                return false;
            }

            //  check if the Z deflection of the hand is valid, i.e no PULL movement , also no fast movement

            bool isHand_Z_DeflectionValid = isHand_Z_DeflectionWithInBounds(GESTURE.PUSH, gestureDatabase);
            if (!isHand_Z_DeflectionValid)
            {
                Console.WriteLine("GESTURE : PUSH | invalid isHand_Z_DeflectionValid ");
                reset(GESTURE.PUSH);
                return false;
            }


            // This is a point where the frame has valid gesture..
            currentPushDistance = updateCurrentLength(GESTURE.PUSH, gestureDatabase);
            currentTimerPush = DateTime.Now;
            this.indexOfLastValidPushGestureFrame++;
            // check the duration , update the current psuh length , check the threshold length , 
            timeElapsedPush = new TimeSpan(currentTimerPush.Ticks - startTimerPush.Ticks);
            // condition of time and length satified
            Console.WriteLine("GESTURE : PUSH | timeElapsed  " + timeElapsedPush.Seconds + " dist: " + currentPushDistance);
            if (timeElapsedPush.Seconds >= THRESHOLD_TIME_FOR_VALID_GESTURE &&
                timeElapsedPush.Seconds <= THRESHOLD_TIME_FOR_GESTURE_TO_EXPIRE && 
                currentPushDistance <= -THRESHOLD_PUSH_LENGTH)
            {
                // valid gesture
                reset(GESTURE.PUSH);
                return true;
            }
            else if (timeElapsedPush.Seconds >= THRESHOLD_TIME_FOR_GESTURE_TO_EXPIRE)
            {
                // gesture expired
                reset(GESTURE.PUSH);
                return false;
            }
            // the current frame has been marked as valid frame , but gesture is incomplete so return false...
            return false;

        }


#endregion

#region PULL GESTURE



        public bool processPullBackwardsGesture(GestureDatabase gestureDatabase)
        {

            // first frame is always a valid frame , No initial check on joints is needed ...
            if (gestureDatabase.getTotalSize() > 0)
            {
                // check if the current frame is a valid gesture start frame
                if (!isPullGestureStarted && indexOfFirstValidPullGestureFrame < 0)
                {
                    bool isCurrentFrameValidPullGesture = isValidStartGesture(GESTURE.PULL, gestureDatabase);
                    if (!isCurrentFrameValidPullGesture)
                    {
                        Console.WriteLine("GESTURE : PULL | invalid isCurrentFrameValidPullGesture ");
                        reset(GESTURE.PULL);
                        return false;
                    }
                    indexOfFirstValidPullGestureFrame = indexOfLastValidPullGestureFrame = gestureDatabase.getTotalSize() - 1;
                    startTimerPull = currentTimerPull = DateTime.Now;       // start the timer 
                    isPullGestureStarted = true;
                    currentPullDistance = 0;                                // set  the initial pull distance
                    return false;                                           // this is a valid frame but the gesture is incomplete
                }
            }
            else
            {
                return false;
            }


            // check to see if there is any body deflection
            bool isBodyDefectionValid = isBodyLinearDeflectionsWithInBounds(GESTURE.PULL, gestureDatabase);

            if (!isBodyDefectionValid)
            {
                Console.WriteLine("GESTURE : PULL | invalid isBodyDefectionValid ");
                reset(GESTURE.PULL);
                return false;
            }

            // check to see if there is hand deflection except in the positive Z-dir

            bool isHand_X_Y_DeflectionValid = isHand_X_Y_DeflectionWithInBounds(GESTURE.PULL, gestureDatabase);
            if (!isHand_X_Y_DeflectionValid)
            {
                Console.WriteLine("GESTURE : PULL | invalid isHand_X_Y_DeflectionValid ");
                reset(GESTURE.PULL);
                return false;
            }

            //  check if the Z deflection of the hand is valid, i.e no PUSH movement , also no fast movement

            bool isHand_Z_DeflectionValid = isHand_Z_DeflectionWithInBounds(GESTURE.PULL, gestureDatabase);
            if (!isHand_Z_DeflectionValid)
            {
                Console.WriteLine("GESTURE : PULL | invalid isHand_Z_DeflectionValid ");
                reset(GESTURE.PULL);
                return false;
            }


            // This is a point where the frame has valid gesture..
            currentPullDistance = updateCurrentLength(GESTURE.PULL, gestureDatabase);
            currentTimerPull = DateTime.Now;
            this.indexOfLastValidPullGestureFrame++;
            // check the duration , update the current psuh length , check the threshold length , 
            timeElapsedPull = new TimeSpan(currentTimerPull.Ticks - startTimerPull.Ticks);
            // condition of time and length satified
            Console.WriteLine("GESTURE : PULL | timeElapsed  " + timeElapsedPull.Seconds + " dist: " + currentPullDistance);
            if (timeElapsedPull.Seconds >= THRESHOLD_TIME_FOR_VALID_GESTURE &&
                timeElapsedPull.Seconds <= THRESHOLD_TIME_FOR_GESTURE_TO_EXPIRE &&
                currentPullDistance >= THRESHOLD_PULL_LENGTH)
            {
                // valid gesture
                reset(GESTURE.PULL);
                return true;
            }
            else if (timeElapsedPull.Seconds >= THRESHOLD_TIME_FOR_GESTURE_TO_EXPIRE)
            {
                // gesture expired
                reset(GESTURE.PULL);
                return false;
            }
            // the current frame has been marked as valid frame , but gesture is incomplete so return false...
            return false;


        }

#endregion
    }
}
