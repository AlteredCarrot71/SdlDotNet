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
    /// Circle Primitive
    /// </summary>
    public struct Circle : IPrimitive
    {
        short x;
        short y;
        short r;

        /// <summary>
        /// Constructor for Circle
        /// </summary>
        /// <param name="positionX">X coordinate of Center</param>
        /// <param name="positionY">Y coordinate of Center</param>
        /// <param name="radius">Radius</param>
        public Circle(short positionX, short positionY, short radius)
        {
            this.x = positionX;
            this.y = positionY;
            this.r = radius;
        }

        /// <summary>
        /// Constructor for Circle
        /// </summary>
        /// <param name="center">Center point</param>
        /// <param name="radius">Radius</param>
        public Circle(Point center, short radius)
        {
            this.x = (short)center.X;
            this.y = (short)center.Y;
            this.r = radius;
        }

        /// <summary>
        /// Center of circle
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
        /// X position of circle
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
        /// Y position of circle
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
        /// Radius of circle
        /// </summary>
        public short Radius
        {
            get
            {
                return this.r;
            }
            set
            {
                this.r = value;
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
                int result = SdlGfx.filledCircleRGBA(surface.Handle, this.PositionX, this.PositionY, this.Radius, color.R, color.G, color.B,
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
                    result = SdlGfx.aacircleRGBA(surface.Handle, this.PositionX, this.PositionY, this.Radius, color.R, color.G, color.B,
                        color.A);
                    GC.KeepAlive(this);
                }
                else
                {
                    result = SdlGfx.circleRGBA(surface.Handle, this.PositionX, this.PositionY, this.Radius, color.R, color.G, color.B,
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
        /// String representation of circle
        /// </summary>
        /// <returns>string representation of circle</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "({0},{1}, {2})", x, y, r);
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="obj">Circle to compare</param>
        /// <returns>true if circles are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(Circle))
                return false;

            Circle c = (Circle)obj;
            return ((this.x == c.x) && (this.y == c.y) && (this.r == c.r));
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="c1">Circle to compare</param>
        /// <param name="c2">Circle to compare</param>
        /// <returns>True if circles are equal</returns>
        public static bool operator ==(Circle c1, Circle c2)
        {
            return ((c1.x == c2.x) && (c1.y == c2.y) && (c1.r == c2.r));
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="c1">Circle to compare</param>
        /// <param name="c2">Circle to compare</param>
        /// <returns>True if circles are not equal</returns>
        public static bool operator !=(Circle c1, Circle c2)
        {
            return !(c1 == c2);
        }

        /// <summary>
        /// Hash Code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return x ^ y ^ r;
        }
    }
}
