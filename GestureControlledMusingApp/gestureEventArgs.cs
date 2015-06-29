using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class gestureEventArgs : EventArgs
    {
        public GESTURES gesture
        {
            get;
            set;
        }

        public gestureEventArgs(GESTURES gesture)
        {
            this.gesture = gesture;
        }

    }
}
