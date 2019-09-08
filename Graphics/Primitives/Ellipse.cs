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
    /// Ellipse Primitive
    /// </summary>
    public struct Ellipse : IPrimitive
    {
        short x;
        short y;
        short radiusX;
        short radiusY;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="positionX">X coordinate of center</param>
        /// <param name="positionY">Y coordinate of center</param>
        /// <param name="radiusX">Radius on X axis</param>
        /// <param name="radiusY">Radius on Y axis</param>
        public Ellipse(short positionX, short positionY, short radiusX, short radiusY)
        {
            this.x = positionX;
            this.y = positionY;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point">Center</param>
        /// <param name="size">Width and height of ellipse</param>
        public Ellipse(Point point, Size size)
        {
            this.x = (short)point.X;
            this.y = (short)point.Y;
            this.radiusX = (short)size.Width;
            this.radiusY = (short)size.Height;
        }

        /// <summary>
        /// Center of ellipse
        /// </summary>
        public Point Center
        {
            get
            {
                return new Point(this.x, this.y);
            }
            set
            {
                this.x = (short)value.X;
                this.y = (short)value.Y;
            }
        }

        /// <summary>
        /// Size struct representing width and height of ellipse
        /// </summary>
        public Size Size
        {
            get
            {
                return new Size(this.radiusX, this.radiusY);
            }
            set
            {
                this.radiusX = (short)value.Width;
                this.radiusY = (short)value.Height;
            }
        }

        /// <summary>
        /// X position of center of ellipse
        /// </summary>
        public short PositionX
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
        /// Y position of center of ellipse
        /// </summary>
        public short PositionY
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
        /// Width of ellipse
        /// </summary>
        public short RadiusX
        {
            get
            {
                return this.radiusX;
            }
            set
            {
                this.radiusX = value;
            }
        }

        /// <summary>
        /// Height of ellipse
        /// </summary>
        public short RadiusY
        {
            get
            {
                return this.radiusY;
            }
            set
            {
                this.radiusY = value;
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
                int result = SdlGfx.filledEllipseRGBA(surface.Handle, this.PositionX, this.PositionY, this.RadiusX, this.RadiusY, color.R, color.G, color.B,
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
                    result = SdlGfx.aaellipseRGBA(
                    surface.Handle, this.PositionX, this.PositionY,
                    this.RadiusX, this.RadiusY,
                    color.R, color.G, color.B,
                    color.A);
                    GC.KeepAlive(this);
                }
                else
                {
                    result = SdlGfx.ellipseRGBA(
                    surface.Handle, this.PositionX, this.PositionY,
                    this.RadiusX, this.RadiusY,
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
        /// String representation of ellipse
        /// </summary>
        /// <returns>string representation of ellipse</returns>
        public override string ToString()
        {
            return String.Format(
                CultureInfo.CurrentCulture,
                "({0}, {1}, {2}, {3})", x, y, radiusX, radiusY);
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="obj">Ellipse to compare</param>
        /// <returns>True if ellipses are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(Ellipse))
                return false;

            Ellipse e = (Ellipse)obj;
            return (
                (this.x == e.x) &&
                (this.y == e.y) &&
                (this.radiusX == e.radiusX) &&
                (this.radiusY == e.radiusY)
                );
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="e1">Ellipse to compare</param>
        /// <param name="e2">Ellipse to compare</param>
        /// <returns>True if ellipses are equal</returns>
        public static bool operator ==(Ellipse e1, Ellipse e2)
        {
            return (
                (e1.x == e2.x) &&
                (e1.y == e2.y) &&
                (e1.radiusX == e2.radiusX) &&
                (e1.radiusY == e2.radiusY)
                );
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="e1">Ellipse to compare</param>
        /// <param name="e2">Ellipse to compare</param>
        /// <returns>True if ellipses are not equal</returns>
        public static bool operator !=(Ellipse e1, Ellipse e2)
        {
            return !(e1 == e2);
        }

        /// <summary>
        /// Hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return x ^ y ^ radiusX ^ radiusY;

        }
    }
}
