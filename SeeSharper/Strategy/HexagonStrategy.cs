﻿/**************************************************************************
 *                                                                        *
 *  File:        HexagonStrategy.cs                                       *
 *  Copyright:   (c) 2021, Nistor Paula-Alina                             *
 *  E-mail:      paula-alina.nistor@student.tuiasi.ro                     *
 *  Description: Strategy class for hexagon shape                         *
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
using System.Windows.Forms;

namespace Strategy
{
    /// <summary>
    /// Implements the hexagon drawing strategy
    /// </summary>
    public class HexagonStrategy : TwoPointStrategy
    {
        #region Protected Methods
        protected override void Draw(object sender, PaintEventArgs e)
        {
            if (_points != null)
            {
                var graphics = e.Graphics;
                Point[] hexagonPoints = new Point[6];

                int width = _points[1].X - _points[0].X;
                int height = _points[1].Y - _points[0].Y;

                hexagonPoints[0] = new Point(_points[0].X + width / 2, _points[0].Y);
                hexagonPoints[1] = new Point(_points[0].X + width, _points[0].Y + height / 3);
                hexagonPoints[2] = new Point(_points[0].X + width, _points[0].Y + 2 * height / 3);
                hexagonPoints[3] = new Point(_points[0].X + width / 2, _points[0].Y + height);
                hexagonPoints[4] = new Point(_points[0].X, _points[0].Y + 2 * height / 3);
                hexagonPoints[5] = new Point(_points[0].X, _points[0].Y + height / 3);

                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.FillPolygon(new SolidBrush(_fillColor), hexagonPoints);
                graphics.DrawPolygon(new Pen(_color, _thickness), hexagonPoints);
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
                return $"Draw hexagon with corner ({_points[0].X}, {_points[0].Y}) and ({_points[1].X}, {_points[1].Y})";
            }
            return "Something wrong";
        }
        #endregion
    }
}
