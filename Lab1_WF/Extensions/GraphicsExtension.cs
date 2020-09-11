using Lab1_WF.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_WF.Extensions
{
    public static class GraphicsExtension
    {
        public static void FillTriangle(this Graphics g, Brush brush, Triangle triangle)
        {
            g.FillPolygon(brush, triangle.Points);
        }
    }
}
