using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;


namespace WindowsFormsApplication1
{
    class GestureDatabase
    {
        public readonly int MAX_DATABASE_SIZE = 6000;


        List<Skeleton> skeletons
        {
            get;
            set;
        }

        private List<AppropriateJointInfo> aptJoints
        {
            get;
            set;
        }

        public GestureDatabase()
        {
            skeletons = new List<Skeleton>();
            aptJoints = new List<AppropriateJointInfo>();
        }

        public void ClearDatabase()
        {
            skeletons.Clear();
            aptJoints.Clear();
        }

        public bool getRecords(int index, int length, out List<Skeleton> paramSkeletons, out List<AppropriateJointInfo> paramAptJoints)
        {
            paramSkeletons = new List<Skeleton>();
            paramAptJoints = new List<AppropriateJointInfo>();

            if (index < 0 || index + length > skeletons.Count)
            {
                System.Windows.Forms.MessageBox.Show("Message: Error! Invalid Index | Method : getRecords | Class: GestureDatabase");
                return false;
            }

            for (int i = index; i < index + length; i++)
            {
                paramSkeletons.Add(skeletons.ElementAt(i));
                paramAptJoints.Add(aptJoints.ElementAt(i));
            }

            return true;
        }

        public bool getLastRecord(out Skeleton paramSkeleton , out AppropriateJointInfo paramAptJoint)
        {
            paramSkeleton = skeletons.ElementAt(skeletons.Count - 1);
            paramAptJoint = aptJoints.ElementAt(skeletons.Count - 1);

            return true;
        }

        public bool getRecord(int index , out Skeleton paramSkeleton, out AppropriateJointInfo paramAptJoint)
        {
            paramAptJoint = new AppropriateJointInfo();
            paramSkeleton = new Skeleton();

            if (index < 0 || index > skeletons.Count)
            {
                System.Windows.Forms.MessageBox.Show("Message: Error! Invalid Index "+ index +"| Method : getRecord | Class: GestureDatabase");
                return false;
            }
            paramSkeleton = skeletons.ElementAt(index);
            paramAptJoint = aptJoints.ElementAt(index);

            return true;
            
        }

        public void removeInvalidData(int index , int count)
        {
            for(int i = 0 ; i < index ; i++)
            {
                skeletons.RemoveRange(index , count) ;
                aptJoints.RemoveRange(index , count);            
            }

        }

        public int getTotalSize()
        {
            return skeletons.Count;
        }

        public int addToDatabase(Skeleton skeleton , AppropriateJointInfo aptJoint)
        {
            skeletons.Add(skeleton);
            aptJoints.Add(aptJoint);

            return skeletons.Count;
        }

        public void addToDatabase(Skeleton skeleton)
        {
             skeletons.Add(skeleton);
        }
        public void addToDatabase(AppropriateJointInfo aptJoint)
        {
             aptJoints.Add(aptJoint);
        }
    }
}
