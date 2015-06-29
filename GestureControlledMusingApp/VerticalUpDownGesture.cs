using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace WindowsFormsApplication1
{
    class VerticalUpDownGesture
    {
        public List<SkeletonFrame> skeletonFrames
        {
            set;
            get;
        }

        public bool processVerticalUpGesture(GestureDatabase gestureDatabase)
        {
            return false;
        }
        public bool processVerticalDownGesture(GestureDatabase gestureDatabase)
        {
            return false;
        }
    }
}
