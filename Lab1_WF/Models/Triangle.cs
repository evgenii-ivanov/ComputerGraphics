using Lab1_WF.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_WF.Models
{
    public class Triangle : MovableObject
    {
        public Triangle(float x1, float y1, float x2, float y2, float x3, float y3, float angle)
        {
            Points = new PointF[3];
            Points[0] = new PointF(x1, y1);
            Points[1] = new PointF(x2, y2);
            Points[2] = new PointF(x3, y3);
            if (angle < 0)
            {
                throw new ArgumentException("Move angle can't be a negative number");
            }
            Angle = angle;
        }
    }
}
