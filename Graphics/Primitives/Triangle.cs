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
using System.Drawing;
using System.Globalization;

using SdlDotNet.Core;
//using Tao.Sdl;

namespace SdlDotNet.Graphics.Primitives
{
    /// <summary>
    /// Triangle primitive
    /// </summary>
    public struct Triangle : IPrimitive
    {
        short x1;
        short y1;
        short x2;
        short y2;
        short x3;
        short y3;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x1">X position of first point</param>
        /// <param name="y1">Y position of first point</param>
        /// <param name="x2">X position of second point</param>
        /// <param name="y2">Y position of second point</param>
        /// <param name="x3">X position of third point</param>
        /// <param name="y3">Y position of third point</param>
        public Triangle(short x1, short y1, short x2, short y2, short x3, short y3)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.x3 = x3;
            this.y3 = y3;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point1">First point</param>
        /// <param name="point2">Second point</param>
        /// <param name="point3">Third point</param>
        public Triangle(Point point1, Point point2, Point point3)
        {
            this.x1 = (short)point1.X;
            this.y1 = (short)point1.Y;
            this.x2 = (short)point2.X;
            this.y2 = (short)point2.Y;
            this.x3 = (short)point3.X;
            this.y3 = (short)point3.Y;
        }

        /// <summary>
        /// First point
        /// </summary>
        public Point Point1
        {
            get
            {
                return new Point(this.x1, this.y1);
            }
            set
            {
                this.x1 = (short)value.X;
                this.y1 = (short)value.Y;
            }
        }

        /// <summary>
        /// Second point
        /// </summary>
        public Point Point2
        {
            get
            {
                return new Point(this.x2, this.y2);
            }
            set
            {
                this.x2 = (short)value.X;
                this.y2 = (short)value.Y;
            }
        }

        /// <summary>
        /// Third point
        /// </summary>
        public Point Point3
        {
            get
            {
                return new Point(this.x3, this.y3);
            }
            set
            {
                this.x3 = (short)value.X;
                this.y3 = (short)value.Y;
            }
        }

        /// <summary>
        /// X position of first point
        /// </summary>
        public short XPosition1
        {
            get
            {
                return this.x1;
            }
            set
            {
                this.x1 = value;
            }
        }

        /// <summary>
        /// Y position of first point
        /// </summary>
        public short YPosition1
        {
            get
            {
                return this.y1;
            }
            set
            {
                this.y1 = value;
            }
        }
        /// <summary>
        /// X position of second point
        /// </summary>
        public short XPosition2
        {
            get
            {
                return this.x2;
            }
            set
            {
                this.x2 = value;
            }
        }

        /// <summary>
        /// Y position of second point
        /// </summary>
        public short YPosition2
        {
            get
            {
                return this.y2;
            }
            set
            {
                this.y2 = value;
            }
        }
        /// <summary>
        /// X position of third point
        /// </summary>
        public short XPosition3
        {
            get
            {
                return this.x3;
            }
            set
            {
                this.x3 = value;
            }
        }

        /// <summary>
        /// Y position of third point
        /// </summary>
        public short YPosition3
        {
            get
            {
                return this.y3;
            }
            set
            {
                this.y3 = value;
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
                int result = SdlGfx.filledTrigonRGBA(
                surface.Handle, this.XPosition1, this.YPosition1,
                this.XPosition2, this.YPosition2,
                this.XPosition3, this.YPosition3,
                color.R, color.G, color.B,
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
                    result = SdlGfx.aatrigonRGBA(
                    surface.Handle, this.XPosition1, this.YPosition1,
                    this.XPosition2, this.YPosition2,
                    this.XPosition3, this.YPosition3,
                    color.R, color.G, color.B,
                    color.A);
                    GC.KeepAlive(this);
                }
                else
                {
                    result = SdlGfx.trigonRGBA(
                    surface.Handle, this.XPosition1, this.YPosition1,
                    this.XPosition2, this.YPosition2,
                    this.XPosition3, this.YPosition3,
                    color.R, color.G, color.B,
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
        /// String representation of triangle
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return String.Format(
                CultureInfo.CurrentCulture,
                "({0}, {1}, {2}, {3}, {4}, {5})", x1, y1, x2, y2, x3, y3);
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="obj">triangle to compare</param>
        /// <returns>true if triangles are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(Triangle))
                return false;

            Triangle triangle = (Triangle)obj;
            return (
                (this.x1 == triangle.x1) &&
                (this.y1 == triangle.y1) &&
                (this.x2 == triangle.x2) &&
                (this.y2 == triangle.y2) &&
                (this.x2 == triangle.x3) &&
                (this.y2 == triangle.y3)
                );
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="triangle1">triangle to compare</param>
        /// <param name="triangle2">triangle to compare</param>
        /// <returns>true if triangles are equal</returns>
        public static bool operator ==(Triangle triangle1, Triangle triangle2)
        {
            return (
                (triangle1.x1 == triangle2.x1) &&
                (triangle1.y1 == triangle2.y1) &&
                (triangle1.x2 == triangle2.x2) &&
                (triangle1.y2 == triangle2.y2) &&
                (triangle1.x3 == triangle2.x3) &&
                (triangle1.y3 == triangle2.y3)
                );
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="triangle1">triangle to compare</param>
        /// <param name="triangle2">triangle to compare</param>
        /// <returns>true if triangles are not equal</returns>
        public static bool operator !=(Triangle triangle1, Triangle triangle2)
        {
            return !(triangle1 == triangle2);
        }

        /// <summary>
        /// Hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return x1 ^ y1 ^ x2 ^ y2 ^ x3 ^ y3;

        }
        #region IPrimitive Members

        /// <summary>
        /// Center of triangle
        /// </summary>
        public Point Center
        {
            get
            {
                return new Point((this.x1 + this.x2 + this.x3) / 3, (this.y1 + this.y2 + this.y3) / 3);
            }
            set
            {
                short xDelta = (short)(value.X - (x3 + x2 + x1) / 3);
                short yDelta = (short)(value.Y - (y3 + y2 + y1) / 3);

                this.x1 += xDelta;
                this.x2 += xDelta;
                this.x3 += xDelta;
                this.y1 += yDelta;
                this.y2 += yDelta;
                this.y3 += yDelta;
            }
        }

        #endregion
    }
}
