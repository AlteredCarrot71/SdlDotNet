#region LICENSE
/*
 * Copyright (C) 2004 - 2007 David Hudson (jendave@yahoo.com)
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
#endregion LICENSE

using System;
using System.Globalization;

namespace SdlDotNet.Input
{
    /// <summary>
    /// Struct for trackball motion
    /// </summary>
    public struct BallMotion
    {
        #region Private fields

        int x;
        int y;

        #endregion

        #region Constructors

        /// <summary>
        /// Ball motion
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        public BallMotion(int positionX, int positionY)
        {
            this.x = positionX;
            this.y = positionY;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// motion in X-axis
        /// </summary>
        public int MotionX
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Motion in Y-axis
        /// </summary>
        public int MotionY
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// String output
        /// </summary>
        /// <returns>String repesentation.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "({0},{1})", x, y);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">Object for comparison</param>
        /// <returns>If objects are equal, this returns true.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(BallMotion))
                return false;

            BallMotion c = (BallMotion)obj;
            return ((this.x == c.x) && (this.y == c.y));
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="c1">object to compare</param>
        /// <param name="c2">object to compare</param>
        /// <returns>If objects are equal, this returns true.</returns>
        public static bool operator ==(BallMotion c1, BallMotion c2)
        {
            return ((c1.x == c2.x) && (c1.y == c2.y));
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="c1">object to compare</param>
        /// <param name="c2">object to compare</param>
        /// <returns>If objects are not equal, this returns true.</returns>
        public static bool operator !=(BallMotion c1, BallMotion c2)
        {
            return !(c1 == c2);
        }

        /// <summary>
        /// GetHashCode openrator
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return x ^ y;

        }

        #endregion
    }
}
