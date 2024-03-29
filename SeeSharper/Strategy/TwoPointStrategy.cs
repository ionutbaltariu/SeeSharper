﻿/**************************************************************************
 *                                                                        *
 *  File:        TwoPointStrategy.cs                                      *
 *  Copyright:   (c) 2021, Rusu Iulian                                    *
 *  E-mail:      iulian.rusu2@student.tuiasi.ro                           *
 *  Description: Class for strategies that require two clicks             *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Strategy
{
    /// <summary>
    /// Base class for all shapes that requires 2 points to be drawn
    /// </summary>
    public abstract class TwoPointStrategy : Strategy
    {
        #region Protected Fields
        protected Point[] _points;
        #endregion

        #region Public Methods
        public override void MouseStateChanged(int x, int y)
        {
            if (_points == null)
            {
                _points = new Point[2];
                _points[0].X = _points[1].X = x;
                _points[0].Y = _points[1].Y = y;
            }
            else
            {
                _done = true;
            }
        }

        public override void MouseMoved(int x, int y)
        {
            if (_points != null && !_done)
            {
                _points[1].X = x;
                _points[1].Y = y;
                _hasDrawn = true;
            }
        }

        public override void Reset()
        {
            _done = false;
            _points = null;
            _hasDrawn = false;
        }
        #endregion
    }
}
