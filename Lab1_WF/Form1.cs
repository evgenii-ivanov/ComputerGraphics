using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab1_WF.Models;
using Lab1_WF.Extensions;
using System.Threading;
using System.Drawing.Drawing2D;
using Lab1_WF.Models.Enums;

namespace Lab1_WF
{

    public partial class Form : System.Windows.Forms.Form
    {

        protected static Triangle triangle;
        protected static Graphics g;
        protected int frameCount = 0;
        protected Image backgroundImage;
        protected float currentDx = 0.0f;
        protected bool isVerticalEdgeCollided = false;
        private int buttonPressCounter = 0;
        protected Brush[] brushes = {
            Brushes.Black,
            Brushes.Yellow,
            Brushes.Blue,
            Brushes.Cyan,
            Brushes.Red,
            Brushes.Gray,
            Brushes.Green
        };
        protected int brushIndex = 0;
        private int framesPerBrush = 20;

        public Form()
        {
            InitializeComponent();
            triangle = new Triangle(0, 0, -50, 50, 100, 100, 111);
            backgroundImage = Image.FromFile(@"E:\Projects\CG_Sem5\Lab1_WF\s400.jpg");
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.Clear(Color.White);
            switch (buttonPressCounter)
            {
                case 0:
                    //DELETE THIS
                    framesPerBrush = 20;
                    //DELETE THIS
                    timer.Interval = 50;
                    var transformMatrix = new Matrix(1, 0, 0, -1, this.Width / 2, this.Height / 2);
                    g.Transform = transformMatrix;
                    MoveTriangle();
                    frameCount++;
                    frameCount %= framesPerBrush;
                    if (frameCount == 0)
                    {
                        brushIndex++;
                        brushIndex %= brushes.Length;
                    }
                    g.FillTriangle(brushes[brushIndex], triangle);
                    break;
                case 2:
                    //DELETE THIS
                    framesPerBrush = 3;
                    //DELETE THIS
                    timer.Interval = 10;
                    g.DrawImage(backgroundImage, new PointF { X = 0, Y = 0 });
                    break;
                default:
                    break;
            }
            //DELETE THIS
            if(buttonPressCounter == 2)
            {
                var transformMatrix = new Matrix(1, 0, 0, -1, this.Width / 2, this.Height / 2);
                g.Transform = transformMatrix;
                MoveTriangle();
                frameCount++;
                frameCount %= framesPerBrush;
                if (frameCount == 0)
                {
                    brushIndex++;
                    brushIndex %= brushes.Length;
                }
                g.FillTriangle(brushes[brushIndex], triangle);
            }
            button1.Enabled = true;
        }

        private void MoveTriangle()
        {
            int signX = -1;
            int signY = -1;
            if (triangle.Angle < 90 || triangle.Angle > 270)
            {
                signX = 1;
            }
            if (triangle.Angle < 180)
            {
                signY = 1;
            }
            triangle.Move(Axis.Y, signY);
            if (triangle.UpperY >= this.Height / 2 || triangle.LowerY <= -this.Height / 2)
            {
                triangle.Reflect(Axis.X);
                currentDx = 0.0f;
            }
            var ctg = Math.Abs(1 / Math.Tan(DegreesToRadians(triangle.Angle)));
            currentDx += (float) ctg;
            float dx = 0.0f;
            if (currentDx > 0.5f)
            {
                dx += 1.0f;
                currentDx -= 1.0f;
            }
            triangle.Move(Axis.X, signX * dx);
            if ((triangle.RightX >= this.Width / 2 || triangle.LeftX <= - this.Width / 2) && !isVerticalEdgeCollided)
            {
                triangle.Reflect(Axis.Y);
                currentDx = 0.0f;
                isVerticalEdgeCollided = true;
            }
            else
            {
                isVerticalEdgeCollided = false;
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            g.Dispose();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();

        }

        private static double DegreesToRadians(double deg)
        {
            return deg / 180.0f * Math.PI;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonPressCounter++;
            buttonPressCounter %= 3;
            button1.Enabled = false;
        }
    }
}
