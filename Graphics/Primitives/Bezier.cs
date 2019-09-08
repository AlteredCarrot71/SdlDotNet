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
using System.Collections;
using System.Drawing;
using System.Globalization;

using SdlDotNet.Core;
//using Tao.Sdl;

namespace SdlDotNet.Graphics.Primitives
{
    /// <summary>
    /// Bezier curve
    /// </summary>
    public struct Bezier : IPrimitive
    {
        const int MINIMUMSTEPS = 2;
        short[] x;
        short[] y;
        int n;
        int steps;
        ArrayList list;
        short xTotal;
        short yTotal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="positionsX">array of x positions</param>
        /// <param name="positionsY">array of y positions</param>
        /// <param name="steps">number of steps in curve</param>
        public Bezier(short[] positionsX, short[] positionsY, int steps)
        {
            if (positionsX == null)
            {
                throw new ArgumentNullException("positionsX");
            }
            if (positionsY == null)
            {
                throw new ArgumentNullException("positionsY");
            }
            this.x = positionsX;
            this.y = positionsY;
            this.n = 0;
            this.xTotal = 0;
            this.yTotal = 0;
            this.steps = steps;
            this.list = new ArrayList();

            if (steps < MINIMUMSTEPS)
            {
                this.steps = MINIMUMSTEPS;
            }
            else
            {
                this.steps = steps;
            }

            if (positionsX.Length != positionsY.Length)
            {
                throw SdlException.Generate();
            }
            else
            {
                this.n = positionsX.Length;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="points">Points of curve</param>
        /// <param name="steps">number of steps in curve</param>
        public Bezier(ArrayList points, int steps)
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            this.x = new short[points.Count];
            this.y = new short[points.Count];
            this.n = points.Count;
            this.list = new ArrayList();
            this.xTotal = 0;
            this.yTotal = 0;

            if (steps < MINIMUMSTEPS)
            {
                this.steps = MINIMUMSTEPS;
            }
            else
            {
                this.steps = steps;
            }

            for (int i = 0; i < points.Count; i++)
            {
                x[i] = (short)((Point)points[i]).X;
                y[i] = (short)((Point)points[i]).Y;
            }
        }

        /// <summary>
        /// Get array of x positions of point
        /// </summary>
        public short[] PositionsX()
        {
            return this.x;
        }
        /// <summary>
        /// Set array of x positions of points
        /// </summary>
        /// <param name="arrayX">array of positions</param>
        public void PositionsX(short[] arrayX)
        {
            if (x.Length != y.Length)
            {
                throw SdlException.Generate();
            }
            else
            {
                if (arrayX == null)
                {
                    throw new ArgumentNullException("arrayX");
                }
                this.x = arrayX;
                this.n = arrayX.Length;
            }
        }

        /// <summary>
        /// Get array of y positions of points
        /// </summary>
        public short[] PositionsY()
        {
            return this.y;
        }
        /// <summary>
        /// Set array of y positions of points
        /// </summary>
        /// <param name="arrayY">array of positions</param>
        public void PositionsY(short[] arrayY)
        {
            if (arrayY == null)
            {
                throw new ArgumentNullException("arrayY");
            }
            if (this.x.Length != arrayY.Length)
            {
                throw SdlException.Generate();
            }
            else
            {
                this.y = arrayY;
                this.n = arrayY.Length;
            }
        }

        /// <summary>
        /// Number of points in curve
        /// </summary>
        public int NumberOfPoints
        {
            get
            {
                return this.n;
            }
        }

        /// <summary>
        /// Number of steps in curve
        /// </summary>
        public int Steps
        {
            get
            {
                return this.steps;
            }
            set
            {
                if (value < 2)
                {
                    steps = 2;
                }
                else
                {
                    steps = value;
                }
            }
        }

        /// <summary>
        /// Draw filled primitive onto surface
        /// </summary>
        /// <param name="surface">Surface to draw to</param>
        /// <param name="color">Color to fill primitive</param>
        /// <param name="antiAlias">antialias primitive</param>
        /// <param name="fill">fill primitive with color</param>
        public void Draw(Surface surface, System.Drawing.Color color, bool antiAlias, bool fill)
        {
            if (surface == null)
            {
                throw new ArgumentNullException("surface");
            }
            int result = SdlGfx.bezierRGBA(
                surface.Handle, this.PositionsX(), this.PositionsY(),
                this.NumberOfPoints, this.Steps,
                color.R, color.G, color.B,
                color.A);
            GC.KeepAlive(this);

            if (result != (int)SdlFlag.Success)
            {
                throw SdlException.Generate();
            }
        }

        /// <summary>
        /// Draw primitive onto surface
        /// </summary>
        /// <param name="surface">surface to draw to</param>
        /// <param name="color">Color of primitive</param>
        /// <param name="antiAlias">Antialias primitive</param>
        public void Draw(Surface surface, System.Drawing.Color color, bool antiAlias)
        {
            this.Draw(surface, color, antiAlias, false);
        }

        /// <summary>
        /// Draw primitive onto surface
        /// </summary>
        /// <param name="surface">surface to draw to</param>
        /// <param name="color">Color of primitive</param>
        public void Draw(Surface surface, System.Drawing.Color color)
        {
            Draw(surface, color, true, false);
        }


        /// <summary>
        /// String representation of bezier curve
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return String.Format(
                CultureInfo.CurrentCulture,
                "({0}, {1}, {2}, {3})",
                x, y, n, steps);
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="obj">bezier to compare</param>
        /// <returns>true if bezier curves are equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(Bezier))
            {
                return false;
            }

            Bezier bezier = (Bezier)obj;
            return (
                (this.x == bezier.x) &&
                (this.y == bezier.y) &&
                (this.n == bezier.n) &&
                (this.steps == bezier.steps)
                );
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="bezier1">curve to compare</param>
        /// <param name="bezier2">curve to compare</param>
        /// <returns>true if curves are equal</returns>
        public static bool operator ==(Bezier bezier1, Bezier bezier2)
        {
            return (
                (bezier1.x == bezier2.x) &&
                (bezier1.y == bezier2.y) &&
                (bezier1.n == bezier2.n) &&
                (bezier1.steps == bezier2.steps)
                );
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="bezier1">curve to compare</param>
        /// <param name="bezier2">curve to compare</param>
        /// <returns>true if curves are not equal</returns>
        public static bool operator !=(Bezier bezier1, Bezier bezier2)
        {
            return !(bezier1 == bezier2);
        }

        /// <summary>
        /// Hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ n ^ steps;

        }
        /// <summary>
        /// List of point that make up curve
        /// </summary>
        public ArrayList Points
        {
            get
            {
                list.Clear();
                for (int i = 0; i < this.n; i++)
                {
                    list.Add(new Point(x[i], y[i]));
                }
                return list;
            }
        }
        #region IPrimitive Members

        /// <summary>
        /// Center of curve
        /// </summary>
        public Point Center
        {
            get
            {
                xTotal = 0;
                yTotal = 0;

                for (int i = 0; i < list.Count; i++)
                {
                    xTotal += (short)((Point)list[i]).X;
                    yTotal += (short)((Point)list[i]).Y;
                }
                return new Point((xTotal / this.n), (yTotal / this.n));
            }
            set
            {
                Point centerTemp = new Point(this.Center.X, this.Center.Y);
                foreach (Point i in this.Points)
                {
                    i.Offset(value.X - centerTemp.X, value.Y - centerTemp.Y);
                }
            }
        }

        #endregion
    }
}
