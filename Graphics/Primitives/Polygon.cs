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
    /// Polygon primitive
    /// </summary>
    public struct Polygon : IPrimitive
    {
        short[] x;
        short[] y;
        int n;
        ArrayList list;
        short xTotal;
        short yTotal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="positionsX">array of x positions of points</param>
        /// <param name="positionsY">array of y positions of points</param>
        public Polygon(short[] positionsX, short[] positionsY)
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
            this.list = new ArrayList();
            this.xTotal = 0;
            this.yTotal = 0;

            if (x.Length != y.Length)
            {
                throw SdlException.Generate();
            }
            else
            {
                this.n = x.Length;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="points">ArrayList of points</param>
        public Polygon(ArrayList points)
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
            for (int i = 0; i < points.Count; i++)
            {
                x[i] = (short)((Point)points[i]).X;
                y[i] = (short)((Point)points[i]).Y;
            }
        }

        /// <summary>
        /// Get Array of all X positions
        /// </summary>
        public short[] PositionsX()
        {
            return this.x;
        }
        /// <summary>
        /// Set array of X positions
        /// </summary>
        /// <param name="arrayX">x positions</param>
        public void PositionsX(short[] arrayX)
        {
            if (arrayX == null)
            {
                throw new ArgumentNullException("arrayX");
            }
            if (arrayX.Length != this.y.Length)
            {
                throw SdlException.Generate();
            }
            else
            {
                this.x = arrayX;
                this.n = arrayX.Length;
            }
        }

        /// <summary>
        /// Get array of Y positions
        /// </summary>
        public short[] PositionsY()
        {
            return this.y;
        }
        /// <summary>
        /// Set array of Y positions
        /// </summary>
        /// <param name="arrayY">array of Y positions</param>
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
        /// Number of sides of the polygon
        /// </summary>
        public int NumberOfSides
        {
            get
            {
                return this.n;
            }
        }

        /// <summary>
        /// Arraylist of all the points of the polygon
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
            if (fill)
            {
                int result = SdlGfx.filledPolygonRGBA(surface.Handle, this.PositionsX(), this.PositionsY(), this.NumberOfSides, color.R, color.G, color.G,
                color.A);
                GC.KeepAlive(this);
                if (result != (int)SdlFlag.Success)
                {
                    throw SdlException.Generate();
                }
            }
            else
            {
                int result = 0;
                if (antiAlias)
                {
                    result = SdlGfx.aapolygonRGBA(surface.Handle, this.PositionsX(), this.PositionsY(), this.NumberOfSides, color.R, color.G, color.B,
                    color.A);
                    GC.KeepAlive(this);
                }
                else
                {
                    result = SdlGfx.polygonRGBA(surface.Handle, this.PositionsX(), this.PositionsY(), this.NumberOfSides, color.R, color.G, color.B,
                    color.A);
                    GC.KeepAlive(this);
                }
                if (result != (int)SdlFlag.Success)
                {
                    throw SdlException.Generate();
                }
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
        /// String representation of polygon
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return String.Format(
                CultureInfo.CurrentCulture,
                "({0}, {1}, {2})",
                x, y, n);
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="obj">polygon to compare</param>
        /// <returns>true if the polygons are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(Polygon))
                return false;

            Polygon polygon = (Polygon)obj;
            return (
                (this.x == polygon.x) &&
                (this.y == polygon.y) &&
                (this.n == polygon.n)
                );
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="polygon1">polygon to compare</param>
        /// <param name="polygon2">polygon to compare</param>
        /// <returns>true if the polygons are equal</returns>
        public static bool operator ==(Polygon polygon1, Polygon polygon2)
        {
            return (
                (polygon1.x == polygon2.x) &&
                (polygon1.y == polygon2.y) &&
                (polygon1.n == polygon2.n)
                );
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="polygon1">polygon to compare</param>
        /// <param name="polygon2">polygon to compare</param>
        /// <returns>true if the polygons are equal</returns>
        public static bool operator !=(Polygon polygon1, Polygon polygon2)
        {
            return !(polygon1 == polygon2);
        }

        /// <summary>
        /// Hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ n;

        }
        #region IPrimitive Members

        /// <summary>
        /// Center of polygon
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
