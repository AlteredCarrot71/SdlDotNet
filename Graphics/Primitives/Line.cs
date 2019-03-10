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
using Tao.Sdl;

namespace SdlDotNet.Graphics.Primitives
{
    /// <summary>
    /// Line primitive
    /// </summary>
    public struct Line : IPrimitive
    {
        short x1;
        short y1;
        short x2;
        short y2;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x1">X coordinate of first point</param>
        /// <param name="y1">Y coordinate of first point</param>
        /// <param name="x2">X coordinate of second point</param>
        /// <param name="y2">Y coordinate of second point</param>
        public Line(short x1, short y1, short x2, short y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point1">First point</param>
        /// <param name="point2">Second point</param>
        public Line(Point point1, Point point2)
        {
            this.x1 = (short)point1.X;
            this.y1 = (short)point1.Y;
            this.x2 = (short)point2.X;
            this.y2 = (short)point2.Y;
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
        /// Set to vertical line
        /// </summary>
        public void Vertical()
        {
            XPosition2 = XPosition1;
        }

        /// <summary>
        /// Set to horizontal line
        /// </summary>
        public void Horizontal()
        {
            YPosition2 = YPosition1;
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
            if (antiAlias)
            {
                int result = SdlGfx.aalineRGBA(
                   surface.Handle, this.XPosition1, this.YPosition1,
                   this.XPosition2, this.YPosition2,
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
                int result = SdlGfx.lineRGBA(
                    surface.Handle, this.XPosition1, this.YPosition1,
                    this.XPosition2, this.YPosition2,
                    color.R, color.G, color.B,
                    color.A);
                GC.KeepAlive(this);

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
        /// String representation of line
        /// </summary>
        /// <returns>String represenation of line</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "({0},{1}, {2}, {3})", x1, y1, x2, y2);
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="obj">line to compare</param>
        /// <returns>True if lines are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(Line))
                return false;

            Line line = (Line)obj;
            return (
                (this.x1 == line.x1) &&
                (this.y1 == line.y1) &&
                (this.x2 == line.x2) &&
                (this.y2 == line.y2)
                );
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="line1">Line to compare</param>
        /// <param name="line2">Line to compare</param>
        /// <returns>True if lines are equal</returns>
        public static bool operator ==(Line line1, Line line2)
        {
            return (
                (line1.x1 == line2.x1) &&
                (line1.y1 == line2.y1) &&
                (line1.x2 == line2.x2) &&
                (line1.y2 == line2.y2)
                );
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="line1">Line to compare</param>
        /// <param name="line2">Line to compare</param>
        /// <returns>True if lines are equal</returns>
        public static bool operator !=(Line line1, Line line2)
        {
            return !(line1 == line2);
        }

        /// <summary>
        /// hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return x1 ^ y1 ^ x2 ^ y2;

        }
        #region IPrimitive Members

        /// <summary>
        /// Center of line
        /// </summary>
        public Point Center
        {
            get
            {
                return new Point((x1 + x2) / 2, (y1 + y2) / 2);
            }
            set
            {
                short xDelta = (short)(value.X - (x2 + x1) / 2);
                short yDelta = (short)(value.Y - (y2 + y1) / 2);

                this.x1 += xDelta;
                this.x2 += xDelta;
                this.y1 += yDelta;
                this.y2 += yDelta;
            }
        }

        #endregion
    }
}
