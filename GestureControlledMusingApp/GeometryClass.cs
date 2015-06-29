using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCI580_Geometry
{
    public class Vector2D
    {
        public float X
        {
            get;
            set;
        }
        public float Y
        {
            get;
            set;
        }

        public Vector2D()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Vector2D(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2D(Vector2D vec)
        {
            this.X = vec.X;
            this.Y = vec.Y;
        }

        public static Vector2D operator -(Vector2D vec0 , Vector2D vec1)
        {
            return new Vector2D((vec0.X - vec1.X), (vec0.Y - vec1.Y));
        }

        public double angleTo(Vector2D vec)
        {
            double length = vectorLength() * vec.vectorLength();
            if (length == 0)
                return 0;
            return (180 / Math.PI) * Math.Acos(dotp(vec) / length);
        }

        public double dotp(Vector2D vec)
        {
            return X * vec.X + Y * vec.Y;
        }

        public int cpsign(Vector2D vec)
        {
            return ((X * vec.Y - Y * vec.X) > 0) ? 1 : ((X * vec.Y - Y * vec.X) < 0) ? -1 : 0;
        }

        public double vectorLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }


       
    }
}
