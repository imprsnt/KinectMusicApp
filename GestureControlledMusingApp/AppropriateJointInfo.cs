using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace WindowsFormsApplication1
{

    public enum PRIMARY_HAND
    {
        HAND_LEFT = 1,
        HAND_RIGHT = 2
    }

    public class AppropriateJointInfo
    {
        public SkeletonPoint handLeftPos;
        public SkeletonPoint elbowLeftPos;
        public SkeletonPoint shoulderLeftPos;


        public SkeletonPoint handRightPos;
        public SkeletonPoint elbowRightPos;
        public SkeletonPoint shoulderRightPos;

        public SkeletonPoint headPos;
        public SkeletonPoint shoulderCenterPos;
        public SkeletonPoint spinePos;
        public SkeletonPoint hipCenterPos;

        public PRIMARY_HAND primaryHand;
        public int totalTimeElaspedSinceFirstValidFrame;
        public int skeletonTrackingID
        {
            get;
            set;
        }

        public AppropriateJointInfo()
        {
            primaryHand = PRIMARY_HAND.HAND_RIGHT;
        }
    }
}
