﻿/**************************************************************************
 *                                                                        *
 *  File:        LineStrategy.cs                                          *
 *  Copyright:   (c) 2021, Beldiman Vladislav                             *
 *  E-mail:      vladislav.beldiman@student.tuiasi.ro                     *
 *  Description: Strategy class for line shape                            *
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
    /// Implements the line drawing strategy
    /// </summary>
    public class LineStrategy : TwoPointStrategy
    {
        #region Public Member Functions
        public override string GetDescription()
        {
            if (!_hasDrawn)
            {
                return "Nothing drawn";
            }

            if (_points != null)
            {
                return $"Draw line from ({_points[0].X}, {_points[0].Y}) to ({_points[1].X}, {_points[1].Y})";
            }
            return "Something wrong";
        }
        #endregion
        #region Protected Member Functions
        protected override void Draw(object sender, PaintEventArgs e)
        {
            if (_points != null)
            {
                var graphics = e.Graphics;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.DrawLine(new Pen(_color, _thickness), _points[0], _points[1]);
            }
        }
        #endregion
  }
}