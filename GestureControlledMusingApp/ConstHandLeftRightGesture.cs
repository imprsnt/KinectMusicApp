using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HCI580_Geometry;
using Microsoft.Kinect;

namespace WindowsFormsApplication1
{
    class ConstHandLeftRightGesture
    {

#region MEMBER VARIABLES

        // common variables for both the gestures
        private readonly int THRESHOLD_ANGLE_DEFLECTION = 10;
        private readonly int THRESHOLD_X_Y_Z_DEFLECTIONS = 200;
        private readonly int THRESHOLD_TIME_FOR_VALID_GESTURE = 3;

        // Const Left Gesture parmaters
        private TimeSpan timeElapsedConstLeftHand;
        private DateTime startTimerConstLeftHand;
        private DateTime currentTimerConstLeftHand;
        private int accelerationConstLeftHand;
        private bool isConstLeftHandGestureStarted;
        private int indexOfFirstValidConstLeftGestureFrame;
        private int indexOfLastValidConstLeftGestureFrame;
        private readonly int CONST_LEFT_GESTURE_MIN_THETA = 40;
        private readonly int CONST_LEFT_GESTURE_MAX_THETA = 50;

        // Const Right Gesture parmaters
        private TimeSpan timeElapsedConstRightHand;
        private DateTime startTimerConstRightHand;
        private DateTime currentTimerConstRightHand;
        private bool isConstRightHandGestureStarted;
        private int accelerationConstRightHand;
        private int indexOfFirstValidConstRightGestureFrame;
        private int indexOfLastValidConstRightGestureFrame;
        private readonly int CONST_RIGHT_GESTURE_MIN_THETA = 130;
        private readonly int CONST_RIGHT_GESTURE_MAX_THETA = 140;

        private enum GESTURE { CONST_LEFT, CONST_RIGHT }
        
#endregion     

#region UTILITY FUNCTIONS

        public ConstHandLeftRightGesture()
        {
            reset(GESTURE.CONST_LEFT);
            reset(GESTURE.CONST_RIGHT);
        }

        public void reset()
        {
            reset(GESTURE.CONST_LEFT);
            reset(GESTURE.CONST_RIGHT);
        }

        private void reset(GESTURE gesture)
        {
            switch (gesture)
            {
                case GESTURE.CONST_LEFT:
                    accelerationConstLeftHand = 1;
                    indexOfFirstValidConstLeftGestureFrame = -1;
                    indexOfLastValidConstLeftGestureFrame = -1;
                    isConstLeftHandGestureStarted = false;
                    startTimerConstLeftHand = new DateTime(1, 1, 1);
                    currentTimerConstLeftHand = new DateTime(1, 1, 1);
                    timeElapsedConstLeftHand = new TimeSpan(currentTimerConstLeftHand.Ticks - startTimerConstLeftHand.Ticks);
                    break;

                case GESTURE.CONST_RIGHT:
                    accelerationConstRightHand = 1;
                    indexOfFirstValidConstRightGestureFrame = -1;
                    indexOfLastValidConstRightGestureFrame = -1;
                    isConstRightHandGestureStarted = false;
                    startTimerConstRightHand = new DateTime(1, 1, 1);
                    currentTimerConstRightHand = new DateTime(1, 1, 1);
                    timeElapsedConstRightHand = new TimeSpan(currentTimerConstRightHand.Ticks - startTimerConstRightHand.Ticks);
                    break;
            }

        }

        private bool isHandAngleDeflectionWithInBounds(GESTURE gesture, GestureDatabase gestureDatabase)
        {
            Skeleton skeleton;
            AppropriateJointInfo aptJoint;

            switch (gesture)
            {
                case GESTURE.CONST_LEFT:
                    {
                        gestureDatabase.getLastRecord(out skeleton, out aptJoint);
                        Vector2D elbowShoulder = new Vector2D(aptJoint.shoulderRightPos.X - aptJoint.elbowRightPos.X, aptJoint.shoulderRightPos.Y - aptJoint.elbowRightPos.Y);
                        Vector2D elbowHand = new Vector2D(aptJoint.handRightPos.X - aptJoint.elbowRightPos.X, aptJoint.handRightPos.Y - aptJoint.elbowRightPos.Y);
                       
                        if (elbowShoulder.cpsign(elbowHand) > 0 &&
                            elbowShoulder.angleTo(elbowHand) >= CONST_LEFT_GESTURE_MIN_THETA - THRESHOLD_ANGLE_DEFLECTION &&
                            elbowShoulder.angleTo(elbowHand) <= CONST_LEFT_GESTURE_MAX_THETA + THRESHOLD_ANGLE_DEFLECTION)
                        {
                            return true;
                        }
                        break;
                    }
                case GESTURE.CONST_RIGHT:
                    { 
                        gestureDatabase.getLastRecord(out skeleton, out aptJoint);
                        Vector2D elbowShoulder = new Vector2D(aptJoint.shoulderRightPos.X - aptJoint.elbowRightPos.X, aptJoint.shoulderRightPos.Y - aptJoint.elbowRightPos.Y);
                        Vector2D elbowHand = new Vector2D(aptJoint.handRightPos.X - aptJoint.elbowRightPos.X, aptJoint.handRightPos.Y - aptJoint.elbowRightPos.Y);
                        
                        if (elbowShoulder.cpsign(elbowHand) < 0 &&
                            elbowShoulder.angleTo(elbowHand) >= CONST_RIGHT_GESTURE_MIN_THETA - THRESHOLD_ANGLE_DEFLECTION &&
                            elbowShoulder.angleTo(elbowHand) <= CONST_RIGHT_GESTURE_MAX_THETA + THRESHOLD_ANGLE_DEFLECTION)
                        {
                            return true;
                        }
                        break;
                    }
            }
            return false;
        }

        private bool isFirstValidGestureFrame(GESTURE gesture, GestureDatabase gestureDatabase)
        {
            return isIntermediaryFrameValidGestureFrame(gesture , gestureDatabase);


        }

        private bool isIntermediaryFrameValidGestureFrame(GESTURE gesture, GestureDatabase gestureDatabase)
        {
            Skeleton currentSkeleton;
            AppropriateJointInfo currentAptJointInfo;

            gestureDatabase.getLastRecord(out currentSkeleton, out currentAptJointInfo);

            Vector2D currentElbowShoulder = new Vector2D((currentAptJointInfo.shoulderRightPos.X - currentAptJointInfo.elbowRightPos.X),
                                                                     (currentAptJointInfo.shoulderRightPos.Y - currentAptJointInfo.elbowRightPos.Y));

            Vector2D currentElbowHand = new Vector2D((currentAptJointInfo.handRightPos.X - currentAptJointInfo.elbowRightPos.X),
                                                         (currentAptJointInfo.handRightPos.Y - currentAptJointInfo.elbowRightPos.Y));

            double angle = currentElbowShoulder.angleTo(currentElbowHand);

            switch (gesture)
            {
                case GESTURE.CONST_LEFT:
                    {


                        if (currentAptJointInfo.shoulderCenterPos.Y > currentAptJointInfo.handRightPos.Y &&
                           currentAptJointInfo.shoulderCenterPos.Y > currentAptJointInfo.elbowRightPos.Y &&
                           currentAptJointInfo.handRightPos.X < currentAptJointInfo.elbowRightPos.X &&
                           currentElbowShoulder.cpsign(currentElbowHand) > 0 &&
                           angle >= CONST_LEFT_GESTURE_MIN_THETA - THRESHOLD_ANGLE_DEFLECTION &&
                           angle <= CONST_LEFT_GESTURE_MAX_THETA + THRESHOLD_ANGLE_DEFLECTION)
                        {
                            return true;
                        }

                    }
                    break;

                case GESTURE.CONST_RIGHT:
                    {
                        if (
                          currentAptJointInfo.shoulderCenterPos.Y > currentAptJointInfo.elbowRightPos.Y &&
                          currentAptJointInfo.elbowRightPos.Y > currentAptJointInfo.handRightPos.Y &&
                          currentAptJointInfo.handRightPos.X > currentAptJointInfo.elbowRightPos.X &&
                          currentElbowShoulder.cpsign(currentElbowHand) < 0 &&
                          angle >= CONST_RIGHT_GESTURE_MIN_THETA - THRESHOLD_ANGLE_DEFLECTION &&
                          angle <= CONST_RIGHT_GESTURE_MAX_THETA + THRESHOLD_ANGLE_DEFLECTION)
                        {
                            return true;
                        }
                    }
                    break;
            }

            return false;
        }

        private bool isPointWithInThreshold(SkeletonPoint final, SkeletonPoint initial)
        {
            if (Math.Abs(final.X - initial.X) >= THRESHOLD_X_Y_Z_DEFLECTIONS ||
                Math.Abs(final.Y - initial.Y) >= THRESHOLD_X_Y_Z_DEFLECTIONS ||
                Math.Abs(final.Z - initial.Z) >= THRESHOLD_X_Y_Z_DEFLECTIONS)
                return false;
            return true;
        }

        private bool isIntermediaryCumulativeAngleDeflectionValid(GESTURE gesture ,  GestureDatabase gestureDatabase)
        {

            Skeleton firstValidSkeleton;
            AppropriateJointInfo firstValidAptJointInfo;
            Skeleton currentSkeleton;
            AppropriateJointInfo currentAptJointInfo;

            int indexOfFirstValidGestureFrame = (gesture == GESTURE.CONST_LEFT) ?
                                                 indexOfFirstValidConstLeftGestureFrame :
                                                 indexOfFirstValidConstRightGestureFrame;

            gestureDatabase.getLastRecord(out currentSkeleton , out currentAptJointInfo);
            gestureDatabase.getRecord(indexOfFirstValidGestureFrame, out firstValidSkeleton , out firstValidAptJointInfo);

            Vector2D currentElbowShoulder = new Vector2D((currentAptJointInfo.shoulderRightPos.X - currentAptJointInfo.elbowRightPos.X),
                                                         (currentAptJointInfo.shoulderRightPos.Y - currentAptJointInfo.elbowRightPos.Y));
            Vector2D currentElbowHand = new Vector2D((currentAptJointInfo.handRightPos.X - currentAptJointInfo.elbowRightPos.X),
                                                         (currentAptJointInfo.handRightPos.Y - currentAptJointInfo.elbowRightPos.Y));

            Vector2D firstElbowShoulder = new Vector2D((firstValidAptJointInfo.shoulderRightPos.X - firstValidAptJointInfo.elbowRightPos.X),
                                                         (firstValidAptJointInfo.shoulderRightPos.Y - firstValidAptJointInfo.elbowRightPos.Y));
            Vector2D firstElbowHand = new Vector2D((firstValidAptJointInfo.handRightPos.X - firstValidAptJointInfo.elbowRightPos.X),
                                                         (firstValidAptJointInfo.handRightPos.Y - firstValidAptJointInfo.elbowRightPos.Y));


            double angularDeflection = Math.Abs(currentElbowShoulder.angleTo(currentElbowHand) - firstElbowShoulder.angleTo(firstElbowHand));

            if (angularDeflection <= THRESHOLD_ANGLE_DEFLECTION)
                return true;

            return false;
        }

        private bool isBodyLinearDeflectionsWithInBounds(GESTURE gesture , GestureDatabase gestureDatabase)
        {
            if(gesture == GESTURE.CONST_LEFT)
                if (indexOfFirstValidConstLeftGestureFrame < 0)
                    return true;                        // this is the start of the new Gesture, first frame of the new gesture

            if (gesture == GESTURE.CONST_RIGHT)
                if (indexOfFirstValidConstRightGestureFrame < 0)
                    return true;                        // this is the start of the new Gesture, first frame of the new gesture
            int indexOfFirstValidGestureFrame = (gesture == GESTURE.CONST_LEFT) ? indexOfFirstValidConstLeftGestureFrame :
                                                                                  indexOfFirstValidConstRightGestureFrame;

            Skeleton lastValidSkeleton;
            AppropriateJointInfo lastAptInfo;

            Skeleton firstValidSkeleton;
            AppropriateJointInfo firstAptInfo;

            gestureDatabase.getRecord(gestureDatabase.getTotalSize() - 1, out lastValidSkeleton, out lastAptInfo);
            gestureDatabase.getRecord(indexOfFirstValidGestureFrame, out firstValidSkeleton, out firstAptInfo);

            return (isPointWithInThreshold(lastAptInfo.shoulderCenterPos, firstAptInfo.shoulderCenterPos));

        }

#endregion

#region CONST LEFT HAND GESTURE

        public bool processConstHandLeftGesture(GestureDatabase gestureDatabase)
        {
           
            

            if (gestureDatabase.getTotalSize() > 0)
            {
                if (!isConstLeftHandGestureStarted && indexOfFirstValidConstLeftGestureFrame < 0)
                {
                    bool isCurrentValidStartFrame = isFirstValidGestureFrame(GESTURE.CONST_LEFT, gestureDatabase);
                    if (!isCurrentValidStartFrame)
                    {
                        reset(GESTURE.CONST_LEFT);
                        return false;
                    }
                    isConstLeftHandGestureStarted = true;
                    indexOfFirstValidConstLeftGestureFrame = indexOfLastValidConstLeftGestureFrame = gestureDatabase.getTotalSize() - 1;
                    startTimerConstLeftHand = DateTime.Now;
                    return false;                   // is valid gesture frame , but gesture is still incomplete , so return false.
                }
            }
            else
                return false;


            // check to see if the current frame is a valid frame
            bool isCurrentFrameValid = isIntermediaryFrameValidGestureFrame(GESTURE.CONST_LEFT, gestureDatabase);
            if (!isCurrentFrameValid)
            {
                reset(GESTURE.CONST_LEFT);
                return false;
            }


            // check for nobody movement by comparing frames first and lastvalid frames
            bool isBodyDeflectionValid = isBodyLinearDeflectionsWithInBounds(GESTURE.CONST_LEFT,  gestureDatabase);
            if (!isBodyDeflectionValid)
            {
                reset(GESTURE.CONST_LEFT);
                return false;
            }

           // all conditions satisfied , now check if the gesture has completed , update the indexOfLastValidFrame and Update the time

            indexOfLastValidConstLeftGestureFrame++;
            currentTimerConstLeftHand = DateTime.Now;
            timeElapsedConstLeftHand = new TimeSpan(currentTimerConstLeftHand.Ticks - startTimerConstLeftHand.Ticks);

            if (timeElapsedConstLeftHand.Seconds >= THRESHOLD_TIME_FOR_VALID_GESTURE)
            {
                // valid gesture 
                reset(GESTURE.CONST_LEFT);
                return true;
            }
            else
            {
                // gesture in progress and not complete but not an Invalid gesture
                return false;

            }

        }
#endregion

#region CONST RIGHT HAND GESTURE

        public bool processConstHandRightGesture(GestureDatabase gestureDatabase)
        {
            if (gestureDatabase.getTotalSize() > 0)
            {
                if (!isConstRightHandGestureStarted && indexOfFirstValidConstRightGestureFrame < 0)
                {
                    bool isCurrentValidStartFrame = isFirstValidGestureFrame(GESTURE.CONST_RIGHT, gestureDatabase);
                    if (!isCurrentValidStartFrame)
                    {
                        reset(GESTURE.CONST_RIGHT);
                        return false;
                    }
                    isConstRightHandGestureStarted = true;
                    indexOfFirstValidConstRightGestureFrame = indexOfLastValidConstRightGestureFrame = gestureDatabase.getTotalSize() - 1;
                    startTimerConstRightHand = DateTime.Now;
                    return false;                   // is valid gesture frame , but gesture is still incomplete , so return false.
                }
            }
            else
                return false;


            // check to see if the current frame is a valid frame
            bool isCurrentFrameValid = isIntermediaryFrameValidGestureFrame(GESTURE.CONST_RIGHT , gestureDatabase);
            if (!isCurrentFrameValid)
            {
                reset(GESTURE.CONST_RIGHT);
                return false;
            }


            // check for nobody movement by comparing frames first and lastvalid frames
            bool isBodyDeflectionValid = isBodyLinearDeflectionsWithInBounds(GESTURE.CONST_RIGHT, gestureDatabase);
            if (!isBodyDeflectionValid)
            {
                reset(GESTURE.CONST_RIGHT);
                return false;
            }

            // all conditions satisfied , now check if the gesture has completed , update the indexOfLastValidFrame and Update the time

            indexOfLastValidConstRightGestureFrame++;
            currentTimerConstRightHand = DateTime.Now;
            timeElapsedConstRightHand = new TimeSpan(currentTimerConstRightHand.Ticks - startTimerConstRightHand.Ticks);

            if (timeElapsedConstRightHand.Seconds >= THRESHOLD_TIME_FOR_VALID_GESTURE)
            {
                // valid gesture 
                reset(GESTURE.CONST_RIGHT);
                return true;
            }
            else
            {
                // gesture in progress and not complete but not an Invalid gesture
                return false;

            }
        }

#endregion
    }
}
