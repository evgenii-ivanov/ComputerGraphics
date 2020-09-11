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

        private static Triangle triangle;
        private int frameCount = 0;
        private Image backgroundImage;
        private float currentDx = 0.0f;
        private bool isVerticalEdgeCollided = false;
        private int buttonPressCounter = 0;
        private readonly Brush[] brushes = {
            Brushes.Black,
            Brushes.Yellow,
            Brushes.Blue,
            Brushes.Cyan,
            Brushes.Red,
            Brushes.Gray,
            Brushes.Green
        };
        private int brushIndex = 0;
        private int framesPerBrush = 20;
        private Graphics g;

        public Form()
        {
            InitializeComponent();
            triangle = new Triangle(0, 0, -50, 50, 100, 100, 111);
            backgroundImage = Image.FromFile(@"E:\Projects\CG_Sem5\Lab1_WF\sprite.jpg");
            g = pictureBox.CreateGraphics();
            var transformMatrix = new Matrix(1, 0, 0, -1, this.pictureBox.Width / 2, this.pictureBox.Height / 2);
            g.Transform = transformMatrix;
            DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true
            );
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
            if (triangle.UpperY >= this.pictureBox.Height / 2 || triangle.LowerY <= -this.pictureBox.Height / 2)
            {
                triangle.Reflect(Axis.X);
                currentDx = 0.0f;
            }
            var ctg = Math.Abs(1.0 / Math.Tan(DegreesToRadians(triangle.Angle)));
            currentDx += (float) ctg;
            float dx = 0.0f;
            if (currentDx > 0.5f)
            {
                dx += 1.0f;
                currentDx -= 1.0f;
            }
            triangle.Move(Axis.X, signX * dx);
            if ((triangle.RightX >= this.pictureBox.Width / 2 || triangle.LeftX <= - this.pictureBox.Width / 2) && !isVerticalEdgeCollided)
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

        private void PaintImage()
        {
            var transformMatrix = new Matrix(1, 0, 0, 1, this.pictureBox.Width / 2, this.pictureBox.Height / 2);
            g.Transform = transformMatrix;
            switch (buttonPressCounter)
            {
                case 0:
                    g.Clear(Color.White);
                    g.DrawImage(backgroundImage, new PointF { X = triangle.LeftX, Y = -triangle.UpperY });
                    break;
                case 1:
                    g.Clear(Color.White);
                    break;
                case 2:
                    g.DrawImage(backgroundImage, new PointF { X = triangle.LeftX, Y = -triangle.UpperY });
                    break;
            }
            transformMatrix = new Matrix(1, 0, 0, -1, this.pictureBox.Width / 2, this.pictureBox.Height / 2);
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
            button.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            PaintImage();
        }

        private static double DegreesToRadians(double deg)
        {
            return deg / 180.0f * Math.PI;
        }

        private void crossLabel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, EventArgs e)
        {
            buttonPressCounter++;
            buttonPressCounter %= 3;
            button.Enabled = false;
        }
    }
}
