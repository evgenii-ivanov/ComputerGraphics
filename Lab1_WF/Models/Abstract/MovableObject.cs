using Lab1_WF.Models.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_WF.Models.Abstract
{
    public abstract class MovableObject
    {
        public PointF[] Points
        {
            get;
            protected set;
        }

        public float UpperY
        {
            get
            {
                return Points.Max(x => x.Y);
            }
        }

        public float LowerY
        {
            get
            {
                return Points.Min(x => x.Y);
            }
        }

        public float RightX
        {
            get
            {
                return Points.Max(x => x.X);
            }
        }

        public float LeftX
        {
            get
            {
                return Points.Min(x => x.X);
            }
        }

        public float Angle
        {
            get;
            protected set;
        }

        public void Reflect(Axis axis)
        {
            if (axis == Axis.X)
            {
                Angle = 360 - Angle; 
            }
            else
            {
                Angle = (540 - Angle) % 360;
            }
        }

        public void Move(Axis axis, float delta)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                if (axis == Axis.X)
                {
                    Points[i].X += delta;
                }
                else
                {
                    Points[i].Y += delta;
                }
            }
        }
    }
}
