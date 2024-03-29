﻿/**************************************************************************
 *                                                                        *
 *  File:        HeartStrategy.cs                                         *
 *  Copyright:   (c) 2021, Nistor Paula-Alina                             *
 *  E-mail:      paula-alina.nistor@student.tuiasi.ro                     *
 *  Description: Strategy class for heart shape                           *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/


using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Strategy
{
    /// <summary>
    /// Implements the heart drawing strategy
    /// </summary>
    public class HeartStrategy : TwoPointStrategy
    {
        #region Protected Methods
        protected override void Draw(object sender, PaintEventArgs e)
        {
            if (_points != null)
            {
                var graphics = e.Graphics;
                Pen pen = new Pen(_color, _thickness);

                int width = Math.Abs(_points[1].X - _points[0].X);
                int height = Math.Abs(_points[1].Y - _points[0].Y);

                int startX = Math.Min(_points[0].X, _points[1].X);
                int startY = Math.Min(_points[0].Y, _points[1].Y);

                var rect1 = new Rectangle(startX, startY, width / 2 + 1, height / 2 + 1);
                var rect2 = new Rectangle((startX + width / 2), startY, width / 2 + 1, height / 2 + 1);

                float startAngle = -180F;
                float sweepAngle = 180F;

                Point p = new Point(startX, startY);

                Point[] trianglePoints = { p, p, p };
                trianglePoints[0].Y += height / 4;
                trianglePoints[1].X += width;
                trianglePoints[1].Y += height / 4;
                trianglePoints[2].X += width / 2;
                trianglePoints[2].Y += height;

                GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
                graphicsPath.StartFigure();
                graphicsPath.AddArc(rect1, startAngle, sweepAngle);
                graphicsPath.AddArc(rect2, startAngle, sweepAngle);
                graphicsPath.StartFigure();
                graphicsPath.AddLine(new Point(startX, startY + height / 4), new Point(width / 2 + startX, startY + height));
                graphicsPath.AddLine(new Point(startX + width, startY + height / 4), new Point(width / 2 + startX, startY + height));
                graphicsPath.CloseFigure();

                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.FillPath(new SolidBrush(_fillColor), graphicsPath);
                graphics.FillPolygon(new SolidBrush(_fillColor), trianglePoints);
                graphics.DrawPath(pen, graphicsPath);
            }
        }
        #endregion

        #region Public Methods
        public override string GetDescription()
        {
            if (!_hasDrawn)
            {
                return "Nothing drawn";
            }

            if (_points != null)
            {
                return $"Draw hearth with corner ({_points[0].X}, {_points[0].Y}) and ({_points[1].X}, {_points[1].Y})";
            }
            return "Something wrong";
        }
        #endregion
    }
}
