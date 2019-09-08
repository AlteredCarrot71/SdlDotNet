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
    /// Pie-shaped primitive
    /// </summary>
    public struct Pie : IPrimitive
    {
        short x;
        short y;
        short r;
        short startingAngle;
        short endingAngle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="positionX">X position of vertex</param>
        /// <param name="positionY">Y position of vertex</param>
        /// <param name="radius">Radius</param>
        /// <param name="startingAngle">Starting angle in degrees</param>
        /// <param name="endingAngle">Ending angle in degrees</param>
        public Pie(short positionX, short positionY, short radius, short startingAngle, short endingAngle)
        {
            this.x = positionX;
            this.y = positionY;
            this.r = radius;
            this.startingAngle = startingAngle;
            this.endingAngle = endingAngle;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point">Position of vertex</param>
        /// <param name="radius">Radius</param>
        /// <param name="startingAngle">Starting angle in degrees</param>
        /// <param name="endingAngle">Ending angle in degrees</param>
        public Pie(Point point, short radius, short startingAngle, short endingAngle)
        {
            this.x = (short)point.X;
            this.y = (short)point.Y;
            this.r = radius;
            this.startingAngle = startingAngle;
            this.endingAngle = endingAngle;
        }

        /// <summary>
        /// X position of vertex
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
        /// Y position of vertex
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
        /// Vertex
        /// </summary>
        public Point Point
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
        /// Radius
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
        /// Starting angle in degrees
        /// </summary>
        public short StartingAngle
        {
            get
            {
                return this.startingAngle;
            }
            set
            {
                this.startingAngle = value;
            }
        }

        /// <summary>
        /// Ending angle in degrees
        /// </summary>
        public short EndingAngle
        {
            get
            {
                return this.endingAngle;
            }
            set
            {
                this.endingAngle = value;
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
                int result = SdlGfx.filledPieRGBA(surface.Handle, this.PositionX, this.PositionY, this.Radius, this.StartingAngle, this.EndingAngle, color.R, color.G, color.B,
                color.A);
                GC.KeepAlive(this);
                if (result != (int)SdlFlag.Success)
                {
                    throw SdlException.Generate();
                }
            }
            else
            {
                int result = SdlGfx.pieRGBA(
                surface.Handle, this.PositionX, this.PositionY,
                this.Radius,
                this.StartingAngle, this.EndingAngle,
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
        /// String representation of pie
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return String.Format(
                CultureInfo.CurrentCulture,
                "({0}, {1}, {2}, {3}, {4})",
                x, y, r, startingAngle, endingAngle);
        }
        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="obj">pie to compare</param>
        /// <returns>true if pies are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj.GetType() != typeof(Pie))
                return false;

            Pie pie = (Pie)obj;
            return (
                (this.x == pie.x) &&
                (this.y == pie.y) &&
                (this.r == pie.r) &&
                (this.startingAngle == pie.startingAngle) &&
                (this.endingAngle == pie.endingAngle)
                );
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="pie1">pie to compare</param>
        /// <param name="pie2">pie to compare</param>
        /// <returns>true if pies are equal</returns>
        public static bool operator ==(Pie pie1, Pie pie2)
        {
            return (
                (pie1.x == pie2.x) &&
                (pie1.y == pie2.y) &&
                (pie1.r == pie2.r) &&
                (pie1.startingAngle == pie2.startingAngle) &&
                (pie1.endingAngle == pie2.endingAngle)
                );
        }

        /// <summary>
        /// not equals operator
        /// </summary>
        /// <param name="pie1">pie to compare</param>
        /// <param name="pie2">pie to comapre</param>
        /// <returns>true if pies are not equal</returns>
        public static bool operator !=(Pie pie1, Pie pie2)
        {
            return !(pie1 == pie2);
        }

        /// <summary>
        /// hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return x ^ y ^ r ^ startingAngle ^ endingAngle;

        }
        #region IPrimitive Members

        /// <summary>
        /// Center of pie
        /// </summary>
        public Point Center
        {
            get
            {
                return new Point((int)(this.r * Math.Sin(((this.startingAngle - this.endingAngle) * 2 * Math.PI / 360) / (this.startingAngle - this.endingAngle) * 2 * Math.PI / 360)), (int)(this.r * Math.Cos(1 - (this.startingAngle - this.endingAngle) * 2 * Math.PI / 360) / (this.startingAngle - this.endingAngle) * 2 * Math.PI / 360));
            }
            set
            {
                Point centerTemp = new Point(this.Center.X, this.Center.Y);
                this.Point.Offset(value.X - centerTemp.X, value.Y - centerTemp.Y);
            }
        }

        #endregion
    }
}
